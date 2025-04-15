using UnityEngine;

public class UnlockOverdriveEvent : GameEvent
{
    public UnlockOverdriveEvent(int _delay)
    {
        DelayTime = _delay;
        EventAction = UnlockOverdrive;
        Condition = null;
    }

    public void UnlockOverdrive()
    {
        UIManager.Instance.ShowOverdriveUI();
    }


}
