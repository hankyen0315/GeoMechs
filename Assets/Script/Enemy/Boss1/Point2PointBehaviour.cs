using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Point2PointBehaviour : StateMachineBehaviour
{
    [SerializeField]
    private float MinMoveDistance;
    [SerializeField]
    private float MaxMoveDistance;
    [Range(0,1)]
    [SerializeField]
    private float xEdgeOffset;
    [Range(0,1)]
    [SerializeField]
    private float yEdgeOffset;


    [SerializeField]
    private int tryTimes;

    //consider making this value control by other script?
    [SerializeField]
    private float moveSpeed;

    private Vector2 nextPoint;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Debug.Log("enter moving state");
        animator.SetBool("moving", true);
        DecideNextPoint(animator.gameObject);
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (animator.GetBool("moving") == false) return;
        Vector2 moveVector = nextPoint - To2D(animator.gameObject.transform.position);
        if (moveVector.magnitude < 0.1f)
        {
            animator.SetBool("moving", false);
            int action = ChooseAction(animator);
            animator.SetInteger("action", action);
            return;
        }
        animator.transform.Translate(moveVector.normalized * moveSpeed * Time.deltaTime, Space.World);
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

    }

    private Vector2 To2D(Vector3 position)
    {
        return new Vector2(position.x, position.y);
    }
    private Vector3 To3D(Vector2 position)
    {
        return new Vector3(position.x, position.y, 0f);
    }
    private void DecideNextPoint(GameObject boss)
    {
        nextPoint = Vector2.zero;
        for (int i = 0; i < tryTimes; i++)
        {
            float x = Random.Range(0 + xEdgeOffset, 1 - xEdgeOffset);
            float y = Random.Range(0 + yEdgeOffset, 1 - yEdgeOffset);
            nextPoint = Camera.main.ViewportToWorldPoint(new Vector2(x, y));
            if (Vector2.Distance(To2D(boss.transform.position), nextPoint) < MinMoveDistance && 
                Vector2.Distance(To2D(boss.transform.position), nextPoint) > MaxMoveDistance)
            {
                continue;
            }
            break;
        }
    }
    private int ChooseAction(Animator animator)
    {
        if (animator.GetInteger("stage") == 1)
        {
            return 1;
        }
        else
        {
            if (animator.GetBool("startHeal"))
            {
                return Random.Range(2, 7);
            }
            return 2;
        }
    }

}
