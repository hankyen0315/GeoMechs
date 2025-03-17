using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class PlayerStatsManager : MonoBehaviour
{



    [SerializeField]
    private PlayerStats stats;
    public static PlayerStatsManager Instance { get; private set; }
    public GameObject explode;
    private GameObject new_parent;
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



    public void ChangeHealth(float amount)
    {
        if (stats.Health <= 0f) return;
        stats.Health = Mathf.Max(0f, stats.Health+amount);
        UIManager.Instance.UpdatePlayerHealthBar(stats.Health / stats.MaxHealth);
        if (stats.Health == 0f)
        {
            StartCoroutine(PlayerDeath());
        }
    }
    IEnumerator PlayerDeath()
    {
        Time.timeScale = 0.2f;
        bool finishZoom = false;
        while (!finishZoom)
        {
            finishZoom = FindAnyObjectByType<CameraMagnify>().GoToEnemy(gameObject.transform.position, 5);
            yield return null;
        }
        new_parent = new GameObject("Player_parent");
        new_parent.transform.position = gameObject.transform.position;
        gameObject.transform.parent = new_parent.transform;
        Animation vib_animation = new Animation();
        vib_animation = GetComponent<Animation>();
        vib_animation.Play();
        foreach (AnimationState state in vib_animation)
        {
            state.wrapMode = WrapMode.Loop;
            state.speed = 2;
        }
        if (explode != null)
        {
            GameObject particle = Instantiate(explode, new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, 0f), Quaternion.identity);
            ParticleSystem.MainModule main = particle.GetComponent<ParticleSystem>().main;
        }
        yield return new WaitForSeconds(0.3f);
        Time.timeScale = 1f;
        //yield return new WaitForSeconds(0.5f);

        if (stats.Lives == 1)
        {
            print("GameOver");
            ChangeScene.ToGameOverScene();
            yield return null;
        }
        stats.Lives -= 1;
        stats.Health = stats.MaxHealth;
        AudioManager.Instance.PlayPlayerSounds("Player Death");
        UIManager.Instance.ChangeLivesText(stats.Lives.ToString());
        UIManager.Instance.UpdatePlayerHealthBar(1);
        print("last check point: " + LevelManager.LastCheckPoint.ToString() + "total wave count: " + WaveManager.Instance.waveData.Length.ToString());

        UIManager.Instance.UpdateProgressBar(LevelManager.LastCheckPoint);

        GetComponent<AttackSwitch>().StopAttack();
        LevelManager.ReturnToLastCheckPoint();
        vib_animation.Stop();
    }

    public bool TryChangeCoin(int amount)
    {

        if (stats.Coin < -amount)
        {
            return false;
        }

        stats.Coin += amount;
        //GameObject.Find("CoinText").GetComponent<TextMeshProUGUI>().text = stats.Coin.ToString();
        UIManager.Instance.ChangeCoinText(stats.Coin.ToString());
        return true;
    }

    //finish the other getter method in the near future(because i'm lazy)
    public float GetMoveSpeed()
    {
        return stats.MoveSpeed;
    }
    public float GetRotateSpeed()
    {
        return stats.RotateSpeed;
    }

    public void SpeedUp(float factor)
    {
        stats.MoveSpeed *= factor;
        stats.RotateSpeed *= factor;
    }
    public void ResumeSpeed(float factor)
    {
        stats.MoveSpeed /= factor;
        stats.RotateSpeed /= factor;
    }

    public int GetCoin()
    {
        return stats.Coin;
    }
    public int GetLives()
    {
        return stats.Lives;
    }

    public int GetMaxOverdrive()
    {
        return stats.MaxOverdrive;
    }
    public void IncreaseMaxOverdrive()
    {
        stats.MaxOverdrive++;
    }

    public int GetRadarChance()
    {
        return stats.RadarUseChance;
    }

    public void UseRadar()
    {
        stats.RadarUsedCount++;
    }
    public bool CanUseRadar => stats.RadarUsedCount < stats.RadarUseChance;
    public int LeftRadarChance => stats.RadarUseChance - stats.RadarUsedCount;

    #region evil tricks


    //good player don't use these evil trick to win
    public void MakePlayerInvincible()
    {
        stats.MaxHealth = 1000000;
        stats.Health = 1000000;
    }

    #endregion

}
