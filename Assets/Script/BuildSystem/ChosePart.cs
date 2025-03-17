using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;



//todo: move the functionality to AssemebleManager.cs, and let UImanager.cs to handle the ui.
public class ChosePart : MonoBehaviour
{
    //    [HideInInspector]
    //    public GameObject ChosenPart;
    //    GameObject[] frames;
    //    public TextMeshProUGUI PartDetailText;
    //    //public TextMeshProUGUI PriceText;
    //    private Dictionary<string, string> chosenPartDetail;

    //    public static ChosePart Instance { get; private set; }
    //    private void Awake()
    //    {
    //        if (Instance != null && Instance != this)
    //        {
    //            Destroy(this);
    //        }
    //        else
    //        {
    //            Instance = this;
    //        }
    //    }



    //    // call by pointer event
    //    public void SetChosen(GameObject part)
    //    {
    //        print(part.name + "lmage clicked ");
    //        ChosenPart = part;
    //        PartDetailText.text = "";
    //        chosenPartDetail=part.GetComponent<Part>().GetPartDetail();

    //        foreach (var (key,value) in chosenPartDetail)
    //        {
    //            if(value!="")
    //                PartDetailText.text += key + " : " + value + "\n";
    //        }
    //    }

    //    //call by pointer event
    //    public void HighlightChosen(RawImage chosenFrame)
    //    {
    //        frames = GameObject.FindGameObjectsWithTag("Frame");
    //        foreach (var frame in frames)
    //        {
    //            frame.GetComponent<RawImage>().enabled = false;
    //        }
    //        chosenFrame.enabled = true;
    //    }
}
