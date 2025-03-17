using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircleRedLayer : MonoBehaviour
{
    SpriteRenderer spriteRenderer;
    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer=GetComponent<SpriteRenderer>();
        if(gameObject.transform.parent.GetComponent<Part>() != null)
        {
            spriteRenderer.sortingOrder = gameObject.transform.parent.GetComponent<Part>().SpriteRenderer.sortingOrder + 1;
        }
        else
        {
            spriteRenderer.sortingOrder = 1;
        }
    }
}
