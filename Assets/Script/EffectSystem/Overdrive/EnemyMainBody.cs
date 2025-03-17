using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMainBody : MainBody
{

    public bool isBoss;
    // SAD
    public override void EnterOverdriveMode()
    {
        if (isBoss)
        {
            GetComponentInParent<RandomMoveBehaviour>().SpeedUp(speedUpFactor);
            GetComponentInParent<LookAtPlayerWithSpeed>().RotateSpeed *= speedUpFactor;
            return;
        }
        else
        {
            print("speed up enemy");
            GetComponent<RandomMoveBehaviour>().SpeedUp(speedUpFactor);
        }
    }

    public override void ExitOverdriveMode()
    {
        if (isBoss)
        {
            GetComponentInParent<RandomMoveBehaviour>().ResumeSpeed(speedUpFactor);
            GetComponentInParent<LookAtPlayerWithSpeed>().RotateSpeed /= speedUpFactor;
            return;
        }
        else
        {
            print("resume enemy");
            GetComponent<RandomMoveBehaviour>().ResumeSpeed(speedUpFactor);
        }
    }

    public override Dictionary<string, string> GetPartDetail()
    {
        throw new System.NotImplementedException();
    }

   
}
