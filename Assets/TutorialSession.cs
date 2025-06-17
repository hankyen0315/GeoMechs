using System;
using UnityEngine;

[Serializable]
public class TutorialSession
{
    public string SessionName;
    private TutorialStep[] steps;

}

[Serializable]
public class TutorialStep
{
    private Action stepAction;
}