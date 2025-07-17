using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Effect : MonoBehaviour
{
    Attack attackData;
    Buff buffData;
    [SerializeField]
    private float scatterAngle = 5f;

    // Start is called before the first frame update
    void Start()
    {
        attackData = GetComponentInChildren<Attack>(false);
    }

    //public EffectResult CalculateEffect(GameObject childObject,float AttackModifier, float TotalScatter, GameObject BulletType, Action<GameObject> overdriveCallback = null)
    //{
    //    bool atEndPoint = childObject.transform.CompareTag("RedCircle") || childObject.transform.name == "EnemyGunGO" || childObject.transform.name == "PlayerGunGO";
    //    if (!atEndPoint)
    //    {

    //        if (childObject.GetComponent<Attack>())
    //        {
    //            //if (BulletType == null && childObject.GetComponent<Attack>().Active)
    //            //{
    //            //    BulletType = attackData.Bullet;
    //            //}


    //            for (int i = 0; i < childObject.transform.childCount; i++)
    //            {
    //                CalculateEffect(childObject.transform.GetChild(i).gameObject, AttackModifier, TotalScatter, BulletType, overdriveCallback);
    //            }
    //        }
    //        else if(childObject.GetComponent<Buff>())
    //        {
    //            buffData = childObject.GetComponent<Buff>();
    //            if (buffData.Active)
    //            {
    //                AttackModifier *= buffData.AttackModifier;
    //                TotalScatter += buffData.Scatter;
    //            }
    //            for (int i = 0; i < childObject.transform.childCount; i++)
    //            {
    //                CalculateEffect(childObject.transform.GetChild(i).gameObject, AttackModifier,TotalScatter, BulletType, overdriveCallback);
    //            }
    //        }
    //    }
    //    else
    //    {
    //        if (BulletType == null)
    //        {
    //            print("BulletType is null");
    //            return new EffectResult(0, 0, null);
    //        }

    //        float negValue = -scatterAngle;
    //        //the bullet should be face upward
    //        float forward = childObject.transform.rotation.eulerAngles.z;
    //        float initialAngle = (TotalScatter % 2 == 1) ? forward : forward - negValue / 2f;
    //        for (int i = 0;i< TotalScatter; i++)
    //        {
    //            negValue = -negValue;
    //            initialAngle += i * negValue;
    //            Quaternion newRotation = Quaternion.Euler(0, 0, initialAngle);
    //            Transform parent = null;
    //            if (attackData != null)
    //            {
    //                parent = (attackData.StickWithInstantiatePoint) ? childObject.transform : null;
    //            }
    //            GameObject instantiatedObject = BulletSpawner.Instance.SpawnBullet(BulletType, childObject.transform.position, newRotation, parent, AttackModifier, overdriveCallback);
    //            if (parent != null)
    //            {
    //                instantiatedObject.transform.localScale = ScaleBack.GetOriginalScale(childObject.transform.localScale);
    //            }
                    
    //            instantiatedObject.name = "Bullet";
    //        }
            
    //    }
    //}


}
