using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//just for testing, need more general solution for the certain enemy behaviour
public class ShieldEnemyBehaviour : MonoBehaviour
{
    [SerializeField]
    private float cdTime;
    [SerializeField]
    private Effect shieldEffect;


    private void Start()
    {
        StartCoroutine(CreateShield(0));
    }
    public void StartCreateShield()
    {
        StartCoroutine(CreateShield(cdTime));
    }
    private IEnumerator CreateShield(float time)
    {
        yield return new WaitForSeconds(time);
        shieldEffect.UpdateEffect(shieldEffect.gameObject, 1, 1);
    }
}
