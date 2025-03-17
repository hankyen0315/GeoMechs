using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PartChanger : MonoBehaviour
{
    //public GameObject CanonSprite;
    //public GameObject Canon;
    //public GameObject ShieldSprite;
    //public GameObject Shield;

    public List<ChangeSet> ChangeSets;


    public void ChangePart(int id)
    {
        print("start change part with set: " + id.ToString());
        foreach (GameObject part in ChangeSets[id].NeedOpen) 
        {
            print("open part name: " + part.name);
            part.SetActive(true);
        } 
        foreach (GameObject part in ChangeSets[id].NeedClose) part.SetActive(false);
    }




    //public void Stage1CanonOut()
    //{
    //    CanonSprite.SetActive(false);
    //    ShieldSprite.SetActive(false);
    //    Canon.SetActive(true);
    //    Shield.SetActive(false);
    //}

    //public void Stage1CanonIn()
    //{
    //    CanonSprite.SetActive(false);
    //    ShieldSprite.SetActive(false);

    //}






}

[Serializable]
public struct ChangeSet
{
    [TextArea]
    [SerializeField]
    private string Description;
    public List<GameObject> NeedOpen;
    public List<GameObject> NeedClose;
}


