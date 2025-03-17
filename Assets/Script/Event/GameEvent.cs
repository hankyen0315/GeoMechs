using System;
using System.Collections.Generic;
using UnityEngine;

public abstract class GameEvent
{
    public float DelayTime;
    public Action EventAction;
    public Func<bool> Condition;

    public static GameEvent CreateEventFromContainer(EventObjectContainer container)
    {
        GameEvent e;
        switch (container.EventType)
        {
            case "UnlockPartEvent":
                e = new UnlockPartEvent(container.integerA, container.integerB);
                break;
            case "BonusCoinEvent":
                e = new BonusCoinEvent(container.integerA, container.integerB);
                break;
            case "AddOverdriveEvent":
                e = new AddOverdriveEvent(container.integerA);
                break;
            default:
                Debug.Log("no such event");
                return null;
        }
        return e;
    }


}

[System.Serializable]
public class EventObjectContainer
{
    public string EventType;
    public int integerA;
    public int integerB;
    public int integerC;
    public string str;
    //QQ only the dumbest way
    //// Add additional fields for constructor parameters
    //public string constructorParameters;
}