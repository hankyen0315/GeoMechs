using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutScene : MonoBehaviour
{
    private AttackSwitch attackSwitch;
    private PlayerControl playerControl;
    private PlayerDamageDetector damageDetector;
    private void Awake()
    {
        attackSwitch = FindAnyObjectByType<AttackSwitch>();
        playerControl = FindAnyObjectByType<PlayerControl>();
        damageDetector = FindAnyObjectByType<PlayerDamageDetector>();
    }
    public void StopPlayer()
    {
        DisableAllBullet();
        print("cut scene: stop player");
        attackSwitch.StopEveryPointTask();
        playerControl.CanMove = false;
        damageDetector.Active = false;
    }

    private void DisableAllBullet()
    {
        foreach (var sp in FindObjectsByType<SpriteRenderer>(FindObjectsSortMode.None))
        {
            if (sp.sortingLayerName == "Bullet")
            {
                print("disable: " + sp.gameObject.name);
                sp.enabled = false;
            }
        }
    }

    public void RestartPlayer()
    {
        print("cut scene: restart player");
        for (int i = 0; i < attackSwitch.FirstLayerConnectPoints.Count; i++)
        {
            attackSwitch.FirstLayerConnectPoints[i].ExecuteAllTasks();
        }
        playerControl.CanMove = true;
        damageDetector.Active = true;
    }

    //public static void SlowMotion(float scale)
    //{
    //    Time.timeScale = scale;
    //}
    //public static void ResumeMotion()
    //{
    //    Time.timeScale = 1f;
    //}

    public void FightStageEnd(Vector3 diedEnemeyPosition)
    {
        StopPlayer();
        Time.timeScale = 0.15f;
        StartCoroutine(FightEndCutScene(diedEnemeyPosition));
    }
    private IEnumerator FightEndCutScene(Vector3 diedEnemyPosition)
    {
        bool finishZoom = false;
        while (!finishZoom)
        {
            finishZoom = FindAnyObjectByType<CameraMagnify>().GoToEnemy(diedEnemyPosition, 6);
            yield return null;
        }
        yield return new WaitForSeconds(0.25f);
        Time.timeScale = 1f;
    }





}
