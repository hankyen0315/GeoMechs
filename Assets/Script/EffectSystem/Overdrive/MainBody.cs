using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MainBody : Part
{
    [SerializeField]
    protected float speedUpFactor = 1.25f;
    public abstract void EnterOverdriveMode();
    public abstract void ExitOverdriveMode();
}
