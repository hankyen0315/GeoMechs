using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
public class UnlockLevelCheat : MonoBehaviour
{
    public Button Level2;
    public Button Level3;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void Update()
    {
        if (Keyboard.current.uKey.wasPressedThisFrame)
        {
            Level2.interactable = true;
            Level3.interactable = true;
            //print("clear current wave");
            //WaveManager.Instance?.ClearCurrentWave(true);

        }
    }
}
