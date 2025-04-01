using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddOverdriveEvent : GameEvent
{
    public AddOverdriveEvent(int _delay)
    {
        DelayTime = _delay;
        EventAction = IncreaseMaxOverdrive;
        Condition = null;
    }

    private void IncreaseMaxOverdrive()
    {
        AudioManager.Instance.PlayPlayerSounds("Get Item");
        PlayerStatsManager.Instance.IncreaseMaxOverdrive();
        UIManager.Instance.ChangeODText((PlayerStatsManager.Instance.GetMaxOverdrive() - PlayerStatsManager.Instance.gameObject.GetComponent<OverdriveManager>().AvailableOverdrives.Count).ToString());
    }
}
