using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootTripleBulletBehaviour : StateMachineBehaviour
{

    private float timer = 0f;
    private float timeout;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        CalculateEffect calculator = GameObject.Find("NonRotated/CanonCollection/canon_switch").GetComponent<CalculateEffect>();
        calculator.TriggerMultipleTimes(3);
        timeout = calculator.triggerInterval * 3;
        timer = 0f;
        animator.SetBool("attackEnd", false);
    }

    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        timer += Time.deltaTime;
        if (timer > timeout)
        {
            animator.SetBool("attackEnd", true);
        }
    }
}
