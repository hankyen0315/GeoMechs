using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BladeOverdrive : Overdrive
{
    [SerializeField]
    private Animator anim;


    public override void CancelOverdriveAbility()
    {
        anim.SetBool("overdriven", false);
    }

    public override void UseOverdriveAbility()
    {
        anim.SetBool("overdriven", true);
    }
}
