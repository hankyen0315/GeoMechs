using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//TODO: to be implemented in the future
public class LaserOverdrive : Overdrive
{
    private List<GameObject> activeLasers = new List<GameObject>();
    private bool effectEnd = true;
    public override void UseOverdriveAbility()
    {
        AfterBulletInitCallback = EndlessSustainTime;
        effectEnd = false;
    }

    public void EndlessSustainTime(GameObject bullet)
    {
        if (bullet.GetComponentInChildren<Laser>() == null) return;
        if (effectEnd) return;
        gameObject.GetComponent<Attack>().Active = false;
        activeLasers.Add(bullet);
        bullet.GetComponentInChildren<Animator>().SetBool("DecayNormally", false);
        bullet.GetComponentInChildren<Laser>().Overdriven = true;
    }



    public override void CancelOverdriveAbility()
    {
        effectEnd = true;
        foreach (GameObject laser in activeLasers)
        {
            laser.GetComponentInChildren<Animator>().SetBool("DecayNormally", true);
            laser.GetComponentInChildren<Laser>().Overdriven = false;
        }
        activeLasers.Clear();
        AfterBulletInitCallback = null;
        gameObject.GetComponent<Attack>().Active = true;
    }
}
