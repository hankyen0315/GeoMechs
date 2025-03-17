using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class GearOverdrive : Overdrive
{
    [Tooltip("the sprite renderer's gameobject must not have other visiable child")]
    [SerializeField]
    private SpriteRenderer spriteRenderer;
    //[SerializeField]
    private float rotatedSpeed = 60f;
    public override void UseOverdriveAbility()
    {
        StartCoroutine(RotateSelf());
        int childCount=transform.childCount;
        if(gameObject.transform.parent.GetComponent<Overdrive>() && gameObject.transform.parent.GetComponent<Overdrive>().Mode == OD_Mode.Normal)
            gameObject.transform.parent.GetComponent<Overdrive>().EnterOverdrivenMode();
        for (int i = 0;i< childCount; i++)
        {
            if(transform.GetChild(i).GetComponent<Overdrive>() && transform.GetChild(i).GetComponent<Overdrive>().Mode == OD_Mode.Normal)
                transform.GetChild(i).GetComponent<Overdrive>().EnterOverdrivenMode();
        }
    }
    private IEnumerator RotateSelf()
    {
        while (this.Mode == OD_Mode.Overdriven)
        {
            spriteRenderer.gameObject.transform.Rotate(Vector3.forward * rotatedSpeed * Time.deltaTime, Space.Self);
            yield return null;
        }
    }

    public override void CancelOverdriveAbility()
    {
        int childCount = transform.childCount;
        if (gameObject.transform.parent.GetComponent<Overdrive>() && gameObject.transform.parent.GetComponent<Overdrive>().Mode == OD_Mode.Overdriven)
            gameObject.transform.parent.GetComponent<Overdrive>().ResumeNormalMode();
        for (int i = 0; i < childCount; i++)
        {
            if (transform.GetChild(i).GetComponent<Overdrive>() && transform.GetChild(i).GetComponent<Overdrive>().Mode == OD_Mode.Overdriven)
                transform.GetChild(i).GetComponent<Overdrive>().ResumeNormalMode();
        }
    }

}
