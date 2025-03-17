using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

//consider making a bullet pool for efficiency
public class BulletSpawner : MonoBehaviour
{
    public static BulletSpawner Instance { get; private set; }
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }


    /// <summary>
    /// If null is passed in the parent param, the parent would be set to SpawnBullet.Instance.transform by default
    /// </summary>
    /// <param name="bullet"></param>
    /// <param name="position"></param>
    /// <param name="rotation"></param>
    /// <param name="parent"></param>
    /// <returns></returns>
    public GameObject SpawnBullet(GameObject bullet, Vector3 position, Quaternion rotation, Transform parent, float AttackModifier,Action<GameObject> overdriveCallback = null)
    {
        if (bullet.name == "Laser" || bullet.name == "EnemyLaser_v2")
        {
            StartCoroutine(WaitForLaserBuildup());
        }

        //if (bullet.name == "NormalBullet" || bullet.name == "FastBullet")
        //{
        //    AudioManager.Instance.PlayPlayerSounds("Normal attack");
        //}


        if (parent == null)
        {
            parent = transform;
        }
        GameObject newBullet = Instantiate(bullet, position, rotation, parent);
        newBullet.GetComponentInChildren<Bullet>().AttackPower *= AttackModifier;
        if (overdriveCallback == null) return newBullet;
        overdriveCallback(newBullet);
        return newBullet;
    }

    //temp solution
    private IEnumerator WaitForLaserBuildup()
    {
        yield return new WaitForSeconds(0.33f);
        AudioManager.Instance.PlayPlayerSounds("Laser");
    }



}
