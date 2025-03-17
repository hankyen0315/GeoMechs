using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchPartBehaviour : StateMachineBehaviour
{
    [SerializeField]
    private int enterChangeSetID;
    [SerializeField]
    private int exitChangeSetID;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        //if (stateInfo.)
        Debug.Log("animation begin");
        if (enterChangeSetID == -1) return;
        animator.gameObject.GetComponent<PartChanger>().ChangePart(enterChangeSetID);
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Debug.Log("after animation");
        if (exitChangeSetID == -1) return;
        animator.gameObject.GetComponent<PartChanger>().ChangePart(exitChangeSetID);
    }

}
