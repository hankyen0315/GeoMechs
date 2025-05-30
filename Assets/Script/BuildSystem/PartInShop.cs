using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine.Video;
using Unity.VisualScripting;
using TMPro;


public class PartInShop : Selectable
{
    [SerializeField]
    private Part partInfo;
    [SerializeField]
    private RawImage selectedFrame;
    [SerializeField]
    private TextMeshProUGUI newText;
    protected override void Awake()
    {
        base.Awake();
        availableButtonActions = new AvailableButtonAction[0];
    }
    protected override void Highlight()
    {
        selectedFrame.enabled = true;
        if (newText != null)
        {
            newText.text = "";
        }
        
    }
    protected override void Unhighlight()
    {
        selectedFrame.enabled = false;
    }
    protected override Dictionary<string, string> GetPartDetail()
    {
        return partInfo.GetPartDetail();
    }
    protected override VideoClip GetPreviewVideo()
    {
        return partInfo.PreviewVideo;
    }
    public override GameObject GetPartGameObject()
    {
        return partInfo.gameObject;
    }

}
