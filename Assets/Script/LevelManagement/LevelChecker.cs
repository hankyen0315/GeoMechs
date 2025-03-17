using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelChecker : MonoBehaviour
{
    public List<Button> LevelButtons;
    private void Awake()
    {
        for(int i = 0; i < LevelManager.LevelProgress; i++)
        {
            LevelButtons[i].interactable = true;
        }
    }
}
