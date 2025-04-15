using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddPart : MonoBehaviour
{

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
