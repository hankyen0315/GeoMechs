using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public abstract class Part : MonoBehaviour
{
    public SpriteRenderer SpriteRenderer;
    public int Price;
    [TextArea()]
    public string supplement = "";

    public VideoClip PreviewVideo;


    public Material OverdriveMaterial;
    public Material OriginalMaterial;
    [HideInInspector]
    public GameObject ConnectTo;
    //private bool _active = true;
    public bool Active = true;
    //{
    //    get
    //    {
    //        return _active;
    //    }
    //    set
    //    {
    //        if (value != _active) OnActiveStateChange(value);
    //        _active = value;
    //    }
    //}
    //protected abstract void OnActiveStateChange(bool changeTo);

    public abstract Dictionary<string,string> GetPartDetail();


    private int initialChildCount = 0;
    public bool IsPartAtTail
    {
        get
        {
            return gameObject.transform.childCount == initialChildCount;
        }
    }
    protected void Start()
    {
        initialChildCount = transform.childCount;
    }

    public void ToOverdriveMaterial()
    {
        if (OriginalMaterial == null)
        {
            OriginalMaterial = SpriteRenderer.material;
        }
        Material odMaterialInstance = new Material(OverdriveMaterial);
        SpriteRenderer.material = odMaterialInstance;
    }
    public void ToOriginalMaterial()
    {
        Material odMaterial = SpriteRenderer.material;
        Destroy(odMaterial);
        SpriteRenderer.material = OriginalMaterial;
    }


}
