using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImRich : MonoBehaviour
{
    [SerializeField]
    private bool unlimitedBudgetWork;


    private void Start()
    {
        if (unlimitedBudgetWork)
        {
            PlayerStatsManager.Instance.TryChangeCoin(9999999);
        }
    }
}
