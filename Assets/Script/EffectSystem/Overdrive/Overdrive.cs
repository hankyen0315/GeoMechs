using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Part))]
public abstract class Overdrive : MonoBehaviour
{
    [SerializeField]
    public float duration = 5f;
    public OD_Mode Mode { protected set; get; }
    public OverdriveManager Manager;
    private Part part;
    [SerializeField]
    private float glowIntensity = 8f;
    private void Awake()
    {
        part = GetComponent<Part>();
        Mode = OD_Mode.Normal;
    }
    public void EnterOverdrivenMode()
    {
        Mode = OD_Mode.Overdriven;
        float r = part.SpriteRenderer.material.color.r;
        float g = part.SpriteRenderer.material.color.g;
        float b = part.SpriteRenderer.material.color.b;
        part.SpriteRenderer.material.color = new Color(r * glowIntensity, g * glowIntensity, b * glowIntensity);
        if (GetComponent<PartDamageDetector>() != null)
        {
            GetComponent<PartDamageDetector>().Active = false;
        }
        UseOverdriveAbility();
    }
    public void ResumeNormalMode()
    {
        Mode = OD_Mode.Normal;
        float r = part.SpriteRenderer.material.color.r;
        float g = part.SpriteRenderer.material.color.g;
        float b = part.SpriteRenderer.material.color.b;
        part.SpriteRenderer.material.color = new Color(r / glowIntensity, g / glowIntensity, b / glowIntensity);
        if (GetComponent<PartDamageDetector>() != null)
        {
            GetComponent<PartDamageDetector>().Active = true;
        }
        CancelOverdriveAbility();
    }
    public abstract void UseOverdriveAbility();
    public abstract void CancelOverdriveAbility();

    public Action<GameObject> AfterBulletInitCallback = null;


    private void OnDestroy()
    {
        if (Manager != null && Manager.AvailableOverdrives.Contains(this))
        {
            Manager.AvailableOverdrives.Remove(this);
            Manager.UpdateOverdriveUI();
            //UIManager.Instance.ChangeODText((PlayerStatsManager.Instance.GetMaxOverdrive() - Manager.AvailableOverdrives.Count).ToString());
        }
    }
    public enum OD_Mode
    {
        Overdriven, Normal
    }

}
