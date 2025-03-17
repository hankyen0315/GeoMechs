using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootShieldBulletWithHealing : StateMachineBehaviour
{
    [SerializeField]
    private float rotateSpeedUp;
    [SerializeField]
    private float attackSpeedUp;
    [SerializeField]
    private float duration;
    [SerializeField]
    private float healAmountPerSecond;
    [SerializeField]
    private GameObject circleShield;
    private GameObject circleShieldInstance;
    [SerializeField]
    private float rewardDamage;
    [SerializeField]
    private GameObject explodeEffect;

    private float timer;



    private CirclePart circle;
    private GameObject shieldCollection;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        circleShieldInstance = Instantiate(circleShield, animator.gameObject.transform);
        animator.GetComponent<DamageDetector>().Active = false;
        timer = 0f;
        circle = animator.gameObject.GetComponentInChildren<CirclePart>();
        circle.rotateSpeed *= rotateSpeedUp;
        shieldCollection = circle.transform.Find("shield_rotated").gameObject;
        foreach (Attack shieldPart in shieldCollection.GetComponentsInChildren<Attack>())
        {
            shieldPart.AttackInterval /= attackSpeedUp;
        }
        shieldCollection.GetComponent<CalculateEffect>().Auto = true;
        //circle.gameObject.GetComponent<CalculateEffect>().AttackInterval /= attackSpeedUp;
        animator.SetBool("attackEnd", false);
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        timer += Time.deltaTime;
        animator.GetComponent<EnemyStatsManager>().ChangeHealth(healAmountPerSecond * Time.deltaTime);
        
        if ((timer >= duration  || circleShieldInstance == null) && animator.GetBool("attackEnd") == false)
        {
            circle.rotateSpeed /= rotateSpeedUp;
            foreach (Attack shieldPart in shieldCollection.GetComponentsInChildren<Attack>())
            {
                shieldPart.AttackInterval *= attackSpeedUp;
            }
            shieldCollection.GetComponent<CalculateEffect>().StopAllTasks();
            shieldCollection.GetComponent<CalculateEffect>().Auto = false;
            
            animator.GetComponent<DamageDetector>().Active = true;
            animator.SetBool("attackEnd", true);
        }
        
    }
    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (circleShieldInstance == null)
        {
            Debug.Log("interupted");
            //maybe play a sfx?
            AudioManager.Instance.PlaySFX("Heavy Hit");
            Transform me = animator.gameObject.transform;
            Instantiate(explodeEffect, new Vector3(me.position.x, me.position.y, 0f), Quaternion.identity);
            animator.GetComponent<EnemyStatsManager>().ChangeHealth(-rewardDamage);
        }
        else
        {
            Destroy(circleShieldInstance);
        }
    }
}
