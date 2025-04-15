using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class PartPurchased: Selectable
{
    [SerializeField]
    private Sprite SelectedImg;
    private Sprite originalSprite;
    [SerializeField]
    private Part partInfo;

    protected override void Awake()
    {
        base.Awake();
        availableButtonActions = new AvailableButtonAction[] { 
                                     AvailableButtonAction.Rotate, 
                                     AvailableButtonAction.Remove, 
                                     AvailableButtonAction.SetOD, 
                                     AvailableButtonAction.CancelOD };
        originalSprite = partInfo.SpriteRenderer.sprite;
    }
    protected override void Highlight()
    {
        partInfo.SpriteRenderer.sprite = SelectedImg;
    }

    protected override void Unhighlight()
    {
        partInfo.SpriteRenderer.sprite = originalSprite;
    }

    protected override Dictionary<string, string> GetPartDetail()
    {
        return partInfo.GetPartDetail();
    }

    protected override VideoClip GetPreviewVideo()
    {
        return partInfo.PreviewVideo;
    }

    // Start is called before the first frame update
    //void Awake()
    //{
    //    partInfo = GetComponent<Part>();
    //    OriginalSprite = partInfo.SpriteRenderer.sprite;
    //}

    //private void OnMouseDown()
    //{
    //    Select();
    //}

    //public void Select()
    //{
    //    print(gameObject.name + " is selected");
    //    if (gameObject.transform.CompareTag("Untagged"))
    //    {
    //        GameObject another = GameObject.FindGameObjectWithTag("SelectPart");
    //        if (another != null)
    //        {
    //            another.transform.tag = "Untagged";
    //            another.GetComponent<Part>().SpriteRenderer.sprite = another.GetComponent<PartPurchased>().OriginalSprite;
    //        }
    //        gameObject.transform.tag = "SelectPart";
    //        partInfo.SpriteRenderer.sprite = SelectedImg;

    //    }
    //    else
    //    {
    //        Unselect();
    //    }
    //}

    //public void Unselect()
    //{
    //    gameObject.transform.tag = "Untagged";
    //    partInfo.SpriteRenderer.sprite = OriginalSprite;
    //}

}
