using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MakeMeInvincible : MonoBehaviour
{
    public string Usage = "Dying all the time while testing? Check this out";

    public bool InvincibleMode;



    private void Start()
    {
        if (InvincibleMode)
        {
            PlayerStatsManager.Instance.MakePlayerInvincible();
        }
    }

}
