using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using UnityEngine.UI;
public class OverdriveManager : MonoBehaviour
{
    public List<Overdrive> AvailableOverdrives = new List<Overdrive>();
    //[SerializeField]
    //private int MaxOverdrives;
    public bool CanUseOverdrive { get; private set; }
    [SerializeField]
    private float overdriveCD = 10f;
    private float passedTime = 0f;
    //public GameObject Mark;
    //public Volume GlobalVolume;
    private bool usingOD = false;
    private float usingDuration;
    private bool recover = false;
    private float recoverTime;

    [SerializeField]
    private GameObject overdriveIcon;

    [SerializeField]
    private bool isPlayer = false;

    //private Material originalMaterial;
    //private Bloom blooms;
    //[SerializeField]
    //private float glowIntensity = 10f;

    private void Start()
    {
        CanUseOverdrive = true;
    }
    private void Update()
    {
        if (!isPlayer) return;
        if (!CanUseOverdrive)
        {
            passedTime += Time.deltaTime;
            if (usingOD)
            {
                UIManager.Instance.UpdateOverdriveMeter(1-passedTime / usingDuration);
            }
            else if (recover)
            {
                UIManager.Instance.UpdateOverdriveMeter(passedTime / recoverTime);
            }
        }
    }


    public void ChangeCD(float cd)
    {
        overdriveCD = cd;
    }
    public void AddNewOverdrive()
    {
        if (!isPlayer) return;
        if (AvailableOverdrives.Count == PlayerStatsManager.Instance.GetMaxOverdrive())
        {
            UIManager.Instance.ShowMessage("Can't set more parts to overdriven!");
            return;
        }
        GameObject selected = Selectable.Selected.GetPartGameObject();
        if (selected == null) 
        {
            UIManager.Instance.ShowMessage("Please select one part first!");
            return;
        }
        selected.GetComponent<Overdrive>().Manager = this;
        AvailableOverdrives.Add(selected.GetComponent<Overdrive>());
        
        //selected.GetComponent<SelectPart>().Unselect();
        //originalMaterial = selected.GetComponent<Part>().SpriteRenderer.material;
        //selected.GetComponent<Part>().SpriteRenderer.material = selected.GetComponent<Part>().OverdriveMaterial;
        selected.GetComponent<Part>().ToOverdriveMaterial();

        UIManager.Instance.ChangeODText((PlayerStatsManager.Instance.GetMaxOverdrive() - AvailableOverdrives.Count).ToString());
        overdriveIcon.transform.rotation = Quaternion.Euler(0f, 180f, 0f);
        overdriveIcon.GetComponent<Image>().color = Color.red;
    }

    public void RemoveOverdrive()
    {
        if (!isPlayer) return;
        GameObject selected = Selectable.Selected.GetPartGameObject();
        if (selected == null) 
        {
            UIManager.Instance.ShowMessage("Please select one part first!");
            return;
        }

        if (selected.GetComponent<Overdrive>() != null && AvailableOverdrives.Contains(selected.GetComponent<Overdrive>()))
        {
            AvailableOverdrives.Remove(selected.GetComponent<Overdrive>());
            selected.GetComponent<Overdrive>().Manager = null;
            //selected.GetComponent<SelectPart>().Unselect();
            //selected.GetComponent<Part>().SpriteRenderer.material = originalMaterial;
            selected.GetComponent<Part>().ToOriginalMaterial();
            UpdateOverdriveUI();
        }
        else
        {
            UIManager.Instance.ShowMessage("Selected part is not overdriven!");
        }
    }

    public void UpdateOverdriveUI()
    {
        if (!isPlayer) return;
        UIManager.Instance.ChangeODText((PlayerStatsManager.Instance.GetMaxOverdrive() - AvailableOverdrives.Count).ToString());
        overdriveIcon.transform.rotation = Quaternion.Euler(0f, 0f, 0f);
        overdriveIcon.GetComponent<Image>().color = Color.white;
    }

    public void ActivateOverdrive()
    {
        if (AvailableOverdrives.Count == 0)
        {
            print("no overdrive");
            return;
        }
        if (!CanUseOverdrive) 
        {
            print("in cd");
            return;
        }
        usingOD = true;
        passedTime = 0f;
        usingDuration = AvailableOverdrives[0].duration;
        CanUseOverdrive = false;
        
        StartCoroutine(OverdriveTriggerCoolDown());
        foreach (Overdrive activatedOverdrive in AvailableOverdrives)
        {
            activatedOverdrive.EnterOverdrivenMode();
            StartCoroutine(EachOverdriveDurationCountdown(activatedOverdrive, activatedOverdrive.duration));
        }
    }


    private IEnumerator EachOverdriveDurationCountdown(Overdrive overdrive, float duration)
    {
        yield return new WaitForSeconds(duration);
        overdrive.ResumeNormalMode();
        if (isPlayer) UIManager.Instance.ChangeOverdriveMeterColor(Color.gray);
        recoverTime = overdriveCD - passedTime;
        passedTime = 0f;
        usingOD = false;
        recover = true;
    }

    private IEnumerator OverdriveTriggerCoolDown()
    {
        yield return new WaitForSeconds(overdriveCD);
        if (isPlayer) UIManager.Instance.ChangeOverdriveMeterColor(Color.blue);
        CanUseOverdrive = true;
    }


}
