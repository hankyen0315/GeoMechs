using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AttackSwitch : MonoBehaviour
{
    public List<CalculateEffect> FirstLayerConnectPoints;

    public GameObject[] StartNeedClose;
    public GameObject[] StartNeedOpen;

    GameObject BulletSpawner;
    SpriteRenderer SpriteRenderer;
    CircleCollider2D circleCollider;
    PlayerControl playerControl;
    PlayerDamageDetector damageDetector;
    Vector3 initialPosition;
    Quaternion initialRotation;
    public float AttackSizeModifier;
    public float PrepareSizeModifier;
    public Image BlackImage;
    private Camera cam;
    private void Start()
    {
        cam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        initialPosition = gameObject.transform.position;
        initialRotation = gameObject.transform.rotation;
        playerControl = gameObject.GetComponent<PlayerControl>();
        damageDetector = gameObject.GetComponent<PlayerDamageDetector>();
        BulletSpawner = GameObject.Find("BulletSpawner");
    }

    public void StartAttack()
    {
        playerControl.CanMove = true;
        damageDetector.Active = true;
        HideAllRedCircle();
        UnselectAll();
        gameObject.transform.localScale = new Vector3(1f, 1f, 1f)*AttackSizeModifier;

        foreach (var item in StartNeedClose)
        {
            item.SetActive(false);
        }
        foreach (var item in StartNeedOpen)
        {
            item.SetActive(true);
        }

        AudioManager.Instance.PlayMusic("BGM(main)");

        LevelManager.State = LevelState.Fight;
        for (int i = 0; i < FirstLayerConnectPoints.Count; i++)
        {
            FirstLayerConnectPoints[i].ExecuteAllTasks();
        }
    }

    public void StopAttack()
    {
        BlackImage.gameObject.SetActive(true);
        var tempColor = BlackImage.color;
        tempColor.a = 255f;
        BlackImage.color = tempColor;

        playerControl.CanMove = false;
        damageDetector.Active = false;

        cam.orthographicSize = 10;
        cam.transform.position = new Vector3(0, 0, cam.transform.position.z);

        WaveManager.Instance.ClearCurrentWave(false);
        int BulletSpawnerChildCount = BulletSpawner.transform.childCount;
        for (int i = 0; i < BulletSpawnerChildCount; i++)
        {
            BulletSpawner.transform.GetChild(i).gameObject.SetActive(false);
        }
        gameObject.transform.position = initialPosition;
        gameObject.transform.rotation = initialRotation;
        gameObject.transform.localScale = new Vector3(1f, 1f, 1f) * PrepareSizeModifier;
        ShowLastRedCircle();
        foreach (var item in StartNeedClose)
        {
            item.SetActive(true);
        }
        foreach (var item in StartNeedOpen)
        {
            item.SetActive(false);
        }

        AudioManager.Instance.PlayMusic("BGM(Prepare)");
        StartCoroutine(CleanUpEmptyParent());
        LevelManager.State = LevelState.Prepare;
        StopEveryPointTask();
        StartCoroutine(BlackActiveFalse());
    }
    private IEnumerator CleanUpEmptyParent()
    {
        while (GameObject.Find("Empty_parent"))
        {
            GameObject Empty_parent = GameObject.Find("Empty_parent");
            Destroy(Empty_parent);
            yield return null;
        }
    }
    public void StopEveryPointTask()
    {
        for (int i = 0; i < FirstLayerConnectPoints.Count; i++)
        {
            FirstLayerConnectPoints[i].StopAllTasks();
        }
    }

    private IEnumerator BlackActiveFalse()
    {
        yield return new WaitForSeconds(1.0f);
        BlackImage.gameObject.SetActive(false);
    }

    private void HideAllRedCircle()
    {
        GameObject[] gameObjects = GameObject.FindGameObjectsWithTag("RedCircle");

        foreach (GameObject gameObject in gameObjects)
        {
            try
            {
                SpriteRenderer = gameObject.GetComponent<SpriteRenderer>();
                SpriteRenderer.enabled = false;

                circleCollider = gameObject.GetComponent<CircleCollider2D>();
                circleCollider.enabled = false;
            }
            catch
            {

            }
        }
    }

    private void UnselectAll()
    {
        Selectable.Unselect();
    }

    private void ShowLastRedCircle()
    {
        GameObject[] gameObjects = GameObject.FindGameObjectsWithTag("RedCircle");

        foreach (GameObject gameObject in gameObjects)
        {
            try
            {
                SpriteRenderer = gameObject.GetComponent<SpriteRenderer>();
                SpriteRenderer.enabled = true;

                circleCollider = gameObject.GetComponent<CircleCollider2D>();
                circleCollider.enabled = true;
            }
            catch
            {

            }
        }
    }
}
