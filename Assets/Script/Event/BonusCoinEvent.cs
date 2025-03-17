using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonusCoinEvent : GameEvent
{
    private int coinAmount;

    public BonusCoinEvent(int _delay, int _coinAmount)
    {
        DelayTime = _delay;
        coinAmount = _coinAmount;
        EventAction = GiveCoin;
        Condition = null;
    }

    private void GiveCoin()
    {
        PlayerStatsManager.Instance.TryChangeCoin(coinAmount);
    }
}
