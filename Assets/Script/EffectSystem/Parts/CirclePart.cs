using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CirclePart : Buff
{
    public float rotateSpeed;
    public int direction = 1;
    [SerializeField]
    private float atkIntervalModifier = 0.6f;   
    private Quaternion fixedRotation;
    private List<Attack> currentAttackChildren = new List<Attack>();
    private new void Start()
    {
        base.Start();
    }

    private void Update()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            Attack atk = transform.GetChild(i).gameObject.GetComponent<Attack>();
            if (atk == null || currentAttackChildren.Contains(atk)) continue;

            currentAttackChildren.Add(atk);
            atk.AttackInterval *= atkIntervalModifier;
        }
    }

    private void FixedUpdate()
    {
        if (LevelManager.State == LevelState.Fight && Active)
        {
            transform.Rotate(new Vector3(0f, 0f, direction * rotateSpeed * Time.deltaTime), Space.Self);
        }
    }

    public void UpdateAtkIntervalForChildren(float intervalModifier)
    {
        foreach (Attack atk in currentAttackChildren)
        {
            atk.AttackInterval *= intervalModifier;
        }
    }
    public void ResumeAtkIntervalForChildren(float intervalModifier)
    {
        foreach (Attack atk in currentAttackChildren)
        {
            atk.AttackInterval /= intervalModifier;
        }
    }

    //public void FromFightToPrepare()
    //{
    //    transform.rotation = fixedRotation;
    //}

    //public void FromPrepareToFight()
    //{
    //    fixedRotation = transform.rotation;
    //}


}
