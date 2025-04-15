using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// change to EnemyStatManager and EnemyStats maybe?
//this is a temporary solution for enemy health
public class EnemyStatsManager : MonoBehaviour, IGiveDamage
{
    [SerializeField]
    private float maxHealth;
    [SerializeField]
    private float health;
    
    public float CollisionDamage = 1f;
    public bool DestroyOnHit = false;

    [SerializeField]
    private int DropCoin;

    public bool IsBoss = false;
    public bool IsEnemyEntity = true;

    public Sprite BreakSprite;
    private bool breaking = false;
    public GameObject explode;
    //[SerializeField]
    //private float shakeIntensity = 1;
    private GameObject new_parent;

    public bool stopMove = false;
    private void Start()
    {
        if (!IsBoss) return;
        print("uimanager instance" + UIManager.Instance.name);
        UIManager.Instance.ShowBossHealth();
    }
    public void ChangeHealth(float amount, bool killByPlayer = true)
    {
        //print("enemy get hit, -" + amount.ToString() + " health");
        if (health <= 0f) return;
        health += amount;
        if (IsBoss)
        {
            UIManager.Instance.UpdateBossHealthBar(health / maxHealth);
        }

        if (health <= maxHealth / 2)
        {
            if (BreakSprite && !breaking)
            {
                GetComponent<SpriteRenderer>().sprite = BreakSprite;
            }
        }
        if (health <= 0f)
        {
            EnemyDeath(killByPlayer);
        }
    }

    private void OnDestroy()
    {
        if (IsBoss)
        {
            UIManager.Instance.CloseBossHealth();
        }
    }
    public float GetHealth()
    {
        return health;
    }
    public int GetDropCoin()
    {
        return DropCoin;
    }

    public void OnHit()
    {
        if (DestroyOnHit) Destroy(gameObject);
        return;
    }

    public string GetAttacker()
    {
        return gameObject.tag;
    }

    public float GetAttackPower()
    {
        return CollisionDamage;
    }

    public bool Active()
    {
        return true;
    }

    private void EnemyDeath(bool killByPlayer)
    {
        
        if (WaveManager.Instance.LastWaveBeforePrepare && WaveManager.Instance.OnlyOneEnemyLeft && IsEnemyEntity)
        {
            foreach (CalculateEffect ce in GetComponentsInChildren<CalculateEffect>())
            {
                ce.Auto = false;
                ce.StopAllTasks();
            }
            stopMove = true;
            FindAnyObjectByType<CutScene>().FightStageEnd(gameObject.transform.position);

            new_parent =new GameObject("Empty_parent");
            new_parent.transform.position = gameObject.transform.position;
            gameObject.transform.parent = new_parent.transform;
            Animation vib_animation = new Animation();

            if (IsBoss)
            {
                for (int i = 0; i < transform.childCount; i++)
                {
                    if (transform.GetChild(i).gameObject.name != "MainBody")
                    {
                        transform.GetChild(i).gameObject.SetActive(false);
                    }
                    else
                    {
                        vib_animation = transform.GetChild(i).gameObject.GetComponent<Animation>();
                        if (gameObject.GetComponent<Boss3StageManager>())
                        {
                            for (int j = 0; j < transform.GetChild(i).childCount; j++)
                            {
                                transform.GetChild(i).GetChild(j).gameObject.SetActive(false);
                            }
                        }
                    }
                }
            }
            else
            {
                vib_animation = GetComponent<Animation>();
            }
            vib_animation.Play();


            foreach (var eventContainer in WaveManager.Instance.waveData[WaveManager.Instance.WaveCount].events)
            {
                if (eventContainer.EventType == "UnlockPartEvent")
                {
                    UIManager.Instance.ShowNewPartObject(gameObject, eventContainer.PartID);
                }
            }
        }
        
        if (IsEnemyEntity)
        {
            AudioManager.Instance.PlaySFX("Enemy Death");
            WaveManager.Instance.Enemys.Remove(gameObject);
            WaveManager.Instance.EnemyCount -= 1;
        }
        if (killByPlayer && explode != null)
        {
            PlayerStatsManager.Instance.TryChangeCoin(DropCoin); // DropCoin should be positive
            GameObject particle = Instantiate(explode, new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, 0f), Quaternion.identity);
            ParticleSystem.MainModule main = particle.GetComponent<ParticleSystem>().main;
        }
        Destroy(gameObject);
    }
    
    
}
