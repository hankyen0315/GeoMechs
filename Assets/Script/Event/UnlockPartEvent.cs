using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnlockPartEvent : GameEvent
{
    private int partID;
    public UnlockPartEvent(float _delay, int _partID)
    {
        DelayTime = _delay;
        partID = _partID;
        EventAction = UnlockPart;
        Condition = () => LevelManager.State == LevelState.Prepare;
    }

    public void UnlockPart()
    {
        //AudioManager.Instance.PlayPlayerSounds("Get Item");
        UIManager.Instance.ShowNewPart(partID);
    }





}
