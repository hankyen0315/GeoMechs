using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CirclePart))]
public class CircleOverdrive : Overdrive
{
    [SerializeField]
    private float rotateSpeedUp;
    [SerializeField]
    private float attackSpeedUp;
    public override void UseOverdriveAbility()
    {
        CirclePart part = GetComponent<CirclePart>();
        part.rotateSpeed *= rotateSpeedUp;
        part.UpdateAtkIntervalForChildren(1/attackSpeedUp);
    }

    public override void CancelOverdriveAbility()
    {
        CirclePart part = GetComponent<CirclePart>();
        part.rotateSpeed /= rotateSpeedUp;
        part.ResumeAtkIntervalForChildren(1/attackSpeedUp);
    }
}
