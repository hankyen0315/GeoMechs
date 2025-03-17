using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonEffect : MonoBehaviour
{
    public GameObject ResetButton;

    private void Update()
    {
        GameObject SelectPart = GameObject.FindGameObjectWithTag("SelectPart");
        if (SelectPart && !ResetButton.activeSelf)
        {
            ResetButton.SetActive(true);
        }
        else if(!SelectPart && ResetButton.activeSelf)
        {
            ResetButton.SetActive(false);
        }
    }

}
