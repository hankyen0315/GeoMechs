using System;
using UnityEngine;

[Serializable]
public class TutorialStep
{
    public int stepID;
    public Func<bool> condition;
    public Action action;
}
