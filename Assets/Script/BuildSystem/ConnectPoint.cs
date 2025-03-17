using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;

public class ConnectPoint : MonoBehaviour
{
    SpriteRenderer SpriteRenderer;
    CircleCollider2D circleCollider;
    int layerOrder;


    // Start is called before the first frame update
    void Start()
    {
        SpriteRenderer = GetComponent<SpriteRenderer>();
        circleCollider = GetComponent<CircleCollider2D>();
    }

    public void OnMouseDown()
    {
        bool purchaseSucceed = AssembleManager.Instance.BuyPart();
        if (!purchaseSucceed) return;

        layerOrder = SpriteRenderer.sortingOrder;
        AssembleManager.Instance.AddPart(gameObject, gameObject.transform.parent, layerOrder);

        gameObject.transform.tag = "Untagged";
        SpriteRenderer.enabled = false;
        circleCollider.enabled = false;
    }
}
