using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AimBehaviour : StateMachineBehaviour
{
    private Transform player;
    [SerializeField]
    private float rotationOffset = 90f;
    [SerializeField]
    private float rotateSpeed;
    [SerializeField]
    private float threshold;
    [SerializeField]
    private float timeout;

    private float timer;
    private bool timeup = false;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        timer = 0f;
        timeup = false;
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Vector3 relativePos = player.position - animator.gameObject.transform.position;
        float angle = Mathf.Atan2(relativePos.y, relativePos.x) * Mathf.Rad2Deg - rotationOffset;
        Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        
        animator.gameObject.transform.rotation = Quaternion.Lerp(animator.gameObject.transform.rotation, rotation, Time.deltaTime * rotateSpeed);
        if (timer > timeout && !timeup)
        {
            Debug.Log("aimed");
            timeup = true;
            animator.SetTrigger("aimed");
        }
        timer += Time.deltaTime;
    }
}
