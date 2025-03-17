using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class WaveCleaner : MonoBehaviour
{
    [TextArea]
    public string Usage = "press k to kill the current wave enemys(enemys' bullet might still active, but i am too lazy to destroy them though)";
    private void Update()
    {
        if (Keyboard.current.kKey.wasPressedThisFrame)
        {
            print("clear current wave");
            WaveManager.Instance?.ClearCurrentWave(true);
        }
    }
}
