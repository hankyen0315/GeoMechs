using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectPart : MonoBehaviour
{
    [SerializeField]
    private Sprite SelectedImg;
    [HideInInspector]
    public Sprite OriginalSprite;
    private Part partInfo;
    [SerializeField]
    private float selectedScaleUp = 1.5f;

    // Start is called before the first frame update
    void Awake()
    {
        partInfo = GetComponent<Part>();
        OriginalSprite = partInfo.SpriteRenderer.sprite;
    }

    private void OnMouseDown()
    {
        Select();
    }

    public void Select()
    {
        print(gameObject.name + " is selected");
        if (gameObject.transform.CompareTag("Untagged"))
        {
            GameObject another = GameObject.FindGameObjectWithTag("SelectPart");
            if (another != null)
            {
                another.transform.tag = "Untagged";
                another.GetComponent<Part>().SpriteRenderer.sprite = another.GetComponent<SelectPart>().OriginalSprite;
                //another.GetComponent<Part>().SpriteRenderer.gameObject.transform.localScale /= selectedScaleUp;
            }
            gameObject.transform.tag = "SelectPart";
            partInfo.SpriteRenderer.sprite = SelectedImg;
            //partInfo.SpriteRenderer.gameObject.transform.localScale *= selectedScaleUp;

        }
        else
        {
            Unselect();
        }
    }

    public void Unselect()
    {
        gameObject.transform.tag = "Untagged";
        partInfo.SpriteRenderer.sprite = OriginalSprite;
        //partInfo.SpriteRenderer.gameObject.transform.localScale /= selectedScaleUp;
    }
}
