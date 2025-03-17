using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PartStatsManager : MonoBehaviour
{
    [SerializeField]
    private float maxHealth = 6;
    [SerializeField]
    private float health = 6;

    [SerializeField]
    private float repairTime = 2;

    public void ChangeHealth(float amount)
    {
        if (health <= 0f) return;
        health += amount;

        if (health <= 0f)
        {
            Shutdown();
        }
    }

    private void Shutdown()
    {
        AudioManager.Instance.PlayPlayerSounds("Shutdown");
        GetComponent<Part>().Active = false;
        //AttackOrBuff(false);
        //GetComponent<Part>().SpriteRenderer.color = Color.black;
        Color tmp = GetComponent<Part>().SpriteRenderer.color;
        tmp.a = 0.3f;
        GetComponent<Part>().SpriteRenderer.color = tmp;
        GetComponent<Collider2D>().enabled = false;
        StartCoroutine(WaitToRepair());
    }

    public float GetHealth()
    {
        return health;
    }

    IEnumerator WaitToRepair()
    {
        yield return new WaitForSeconds(repairTime);
        //GetComponent<Part>().SpriteRenderer.color = Color.white;
        Color tmp = GetComponent<Part>().SpriteRenderer.color;
        tmp.a = 1f;
        GetComponent<Part>().SpriteRenderer.color = tmp;
        GetComponent<Part>().Active = true;
        health = maxHealth;
        GetComponent<Collider2D>().enabled = true;
    }
}
