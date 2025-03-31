using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//[RequireComponent(typeof(Collider2D))]
//TODO: find a more general way of representing taking damage(currently force the derived class have a SpriteRenderer on it)
public abstract class DamageDetector : MonoBehaviour
{
    public bool Active = true;
    public GameObject OnHitParticle;
    [SerializeField]
    protected string onHitSound = "";
    public Color ParticleColor= Color.white;
    public Color HitColor = Color.white;
    public float HitColorDuration = 0.25f;
    private Color originalColor;
    private void Start()
    {
        if (gameObject.GetComponent<SpriteRenderer>())
            originalColor = gameObject.GetComponent<SpriteRenderer>().color;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //if (collision.GetComponent<Bullet>() == null)
        //{
        //    return;
        //}
        //print(gameObject.name + " collide with: " + collision.gameObject.name);
        if (!Active) return;
        if (collision.GetComponent<IGiveDamage>() == null) return;
        IGiveDamage attackObject = collision.GetComponent<IGiveDamage>();
        string Attacker = attackObject.GetAttacker();
        if (IsHitByOthers(Attacker) && attackObject.Active())
        {
            if (onHitSound != "")
            {
                if (gameObject.tag == "Player")
                {
                    AudioManager.Instance.PlayPlayerSounds(onHitSound);// todo: a better categorization
                }
                else
                {
                    AudioManager.Instance.PlaySFX(onHitSound);
                }
            }
            if (gameObject.GetComponent<SpriteRenderer>())
            {
                gameObject.GetComponent<SpriteRenderer>().color = HitColor;
                StartCoroutine(WaitSecond());
            }
                
            attackObject.OnHit();
            TakeDamage(collision);
        }
    }

    protected virtual bool IsHitByOthers(string tag)
    {
        return tag != gameObject.tag;
    }


    protected abstract void TakeDamage(Collider2D collision);

    IEnumerator WaitSecond()
    {

        yield return new WaitForSeconds(HitColorDuration);
        if (gameObject.GetComponent<SpriteRenderer>())
            gameObject.GetComponent<SpriteRenderer>().color = originalColor;
    }
}
