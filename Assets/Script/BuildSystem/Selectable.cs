using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.Video;


public enum AvailableButtonAction
{
    Rotate = 0,
    Remove = 1,
    SetOD = 2,
    CancelOD = 3,
    Start = 4
}



public abstract class Selectable : MonoBehaviour, IPointerClickHandler
{
    public static Selectable Selected { get; private set; }


    protected AvailableButtonAction[] availableButtonActions;
    private Button[] Buttons;
    protected virtual void Awake()
    {
        Buttons = GameObject.FindGameObjectWithTag("ButtonActionUI").GetComponentsInChildren<Button>(true);
    }

    protected virtual void OnMouseDown()
    {
        Click();
    }
    public void OnPointerClick(PointerEventData eventData)
    {
        Click();
    }

    private void Click()
    {
        if (LevelManager.State == LevelState.Fight) return;

        Debug.Log("Click on " + gameObject.name);
        Selectable previousSelected = Selected;
        if (Selected != null)
        {
            Unselect();
        }
        if (previousSelected != this)
        {
            Select();
        }
    }




    public void Select()
    {
        Selected = this;
        Highlight();
        ShowAvailableActions();
        Dictionary<string, string> detail = GetPartDetail();
        UIManager.Instance.ChangePartDetailText(detail);
        //todo: finish editing preview video, disable for now
        //VideoClip previewVideo = GetPreviewVideo();
        //UIManager.Instance.PlayPreviewVideo(previewVideo);
    }

    public static void Unselect()
    {
        if (Selected == null) return;
        Selected.Unhighlight();
        Selected.HideAvailableActions();
        UIManager.Instance.ChangePartDetailText(new Dictionary<string, string>()); // clear text
        UIManager.Instance.StopPreviewVideo();
        Selected = null;
    }



    protected abstract void Highlight();
    protected abstract void Unhighlight();
    protected abstract Dictionary<string, string> GetPartDetail();
    protected abstract VideoClip GetPreviewVideo();
    private void ShowAvailableActions()
    {
        foreach (AvailableButtonAction action in availableButtonActions)
        {
            Buttons[(int)action].interactable = true;
        }
    }
    private void HideAvailableActions()
    {
        foreach (AvailableButtonAction action in availableButtonActions)
        {
            Buttons[(int)action].interactable = false;
        }
    }

    public virtual GameObject GetPartGameObject()
    {
        return gameObject;
    }


}
