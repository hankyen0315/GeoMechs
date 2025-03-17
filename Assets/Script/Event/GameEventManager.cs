using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEventManager : MonoBehaviour
{
    public static GameEventManager Instance { get; private set; }
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }


    public void HandleEvent(GameEvent e)
    {
        StartCoroutine(DelayAndHandle(e));
    }
    private IEnumerator DelayAndHandle(GameEvent e)
    {
        yield return new WaitForSeconds(e.DelayTime);
        if (e.Condition != null)
        {
            yield return new WaitUntil(e.Condition);
        }
        e.EventAction();
    }



}
