using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddPart : MonoBehaviour
{
    //[SerializeField]
    //private GameObject part;
    //private GameObject newpart;
    //public void Part(Vector3 enemyPosition)
    //{
    //    newpart = new GameObject();

    //    newpart.AddComponent<SpriteRenderer>();
    //    SpriteRenderer spriteRenderer = part.GetComponent<Part>().SpriteRenderer;

    //    newpart.GetComponent<SpriteRenderer>().sprite = spriteRenderer.sprite;
    //    newpart.GetComponent<SpriteRenderer>().color = spriteRenderer.color;
    //    newpart.transform.position = enemyPosition;
    //    newpart.name = "NewPart";
    //}
    [SerializeField]
    private GameObject partPrefab;
    private GameObject part;
    public void Part(Vector3 enemyPosition)
    {
        part = new GameObject();

        part.AddComponent<SpriteRenderer>();
        SpriteRenderer spriteRenderer = partPrefab.GetComponent<Part>().SpriteRenderer;

        part.GetComponent<SpriteRenderer>().sprite = spriteRenderer.sprite;
        part.GetComponent<SpriteRenderer>().color = spriteRenderer.color;
        part.transform.position = enemyPosition;
        part.name = "NewPart";
    }
}
