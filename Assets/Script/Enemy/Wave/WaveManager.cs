using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;
using UnityEngine.SceneManagement;
using System.Reflection;
public class WaveManager : MonoBehaviour
{
    public static WaveManager Instance { private set; get; }
    [SerializeField]
    private int currentLevel;

    [Header("You must set the first WaveData's WaveType to Prepare")]
    public WaveData[] waveData;

    // for debugger
    public List<GameObject> Enemys;

    private int _enemyCount = 0;
    public int EnemyCount 
    {
        get 
        {
            return _enemyCount;
        }
        set 
        {
            _enemyCount = value;
            if (_enemyCount == 0 && ongoingGenerateTask == 0 && clearedByPlayer)
            {
                NextWave();
            }
        }
    }

    public bool LastWaveBeforePrepare => waveData.Length == WaveCount+1 || waveData[WaveCount + 1].Type == WaveType.Prepare;
    public bool FinishGenereateEnemy => ongoingGenerateTask == 0;
    public bool OnlyOneEnemyLeft => EnemyCount == 1 && ongoingGenerateTask == 0;

    public int WaveCount = 0;
    [SerializeField]
    private GameObject Warning;
    [SerializeField]
    private List<GameObject> warnings;
    [SerializeField]
    private float flashingTime;
    [SerializeField]
    private float playerExitSpeed = 20f;
    [SerializeField]
    private float adjustFactor;

    [Tooltip("set to true if player use debugger's wave cleaner, so the next wave would start")]
    private bool clearedByPlayer = true;
    private int ongoingGenerateTask = 0;
    private IEnumerator generateRoutine;
    AttackSwitch attackSwitch;

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
    void Start()
    {
        attackSwitch = GameObject.FindAnyObjectByType<AttackSwitch>();
    }
    private void OnEnable()
    {
        print(LevelManager.LastCheckPoint);
        NextWave();
    }

    private void NextWave()
    {
        Debug.Log("Next Wave");
        TriggerEvents(waveData[WaveCount].events);
        WaveCount++;
        UIManager.Instance.UpdateProgressBar(WaveCount);
        if (WaveCount < waveData.Length)
        {
            print("WaveCount = " + WaveCount);
            if (waveData[WaveCount].Type == WaveType.Prepare) 
            {
                print("prepare");
                SaveCheckPoint();
                Prepare();
                return;
            }

            StartCoroutine(generateRoutine = GenerateEnemy());
        }
        else
        {
            StartCoroutine(TransitionAnimation(OnLevelClear));
            return;
        }

    }

    private void OnLevelClear()
    {
        LevelManager.State = LevelState.Prepare;
        LevelManager.LastCheckPoint = 0;
        print(currentLevel);
        print(LevelManager.TotalLevel);
        if (currentLevel == LevelManager.TotalLevel)
        {
            ChangeScene.ToEndScene();
            return;
        }
        LevelManager.LevelProgress = Mathf.Max(LevelManager.LevelProgress, currentLevel + 1);
        ChangeScene.ToSelectionScene();
        
        return;
    }


    private IEnumerator GenerateEnemy()
    {
        

        yield return new WaitForSeconds(waveData[WaveCount].StartWaitTime);
        print("generating enemy...");
        ongoingGenerateTask = waveData[WaveCount].EnemyInfos.Length;
        foreach (var enemyInfo in waveData[WaveCount].EnemyInfos)
        {
            StartCoroutine(GenerateEnemyOfOneType(enemyInfo));
            yield return new WaitForSeconds(enemyInfo.timeToNextInfo);
        }
    }

    private IEnumerator GenerateEnemyOfOneType(EnemyInfo enemyInfo)
    {
        Vector3 originalPosition = SpawnPositionManager.Instance.GetTransformByName(enemyInfo.spawnPosition.ToString()).position;
        GameObject warning = Instantiate(Warning, originalPosition, Quaternion.identity);


        if (waveData[WaveCount].Type == WaveType.Boss)
        {
            AudioManager.Instance.PlaySFX("Boss Warning");
        }
        else
        {
            AudioManager.Instance.PlaySFX("Enemy Warning");
        }
        
        warnings.Add(warning);
        MoveTowardCenter(warning.transform);
        yield return new WaitForSeconds(flashingTime);
        warnings.Remove(warning);
        Destroy(warning);
        for (int i = 0; i < enemyInfo.number; i++)
        {
            var pos = RandomPosition(originalPosition, enemyInfo.MaxXOffset, enemyInfo.MaxYOffset);
            var enemy = Instantiate(enemyInfo.prefab, pos, Quaternion.identity);
            Enemys.Add(enemy);
            EnemyCount += 1;
            if (i != enemyInfo.number - 1)
            {
                yield return new WaitForSeconds(enemyInfo.spawnTimeInterval);
            }
        }
        ongoingGenerateTask--;
        print("finish generate task, " + ongoingGenerateTask.ToString() + " tasks left");
    }

    private Vector3 RandomPosition(Vector3 position, float maxXOffset, float maxYOffset)
    {
        float xOffset = Random.Range(-maxXOffset, maxXOffset);
        float yOffset = Random.Range(-maxYOffset, maxYOffset);
        return new Vector3(position.x + xOffset, position.y + yOffset, 0f);
    }

    private void Prepare()
    {
        StartCoroutine(TransitionAnimation(attackSwitch.StopAttack));
    }
    private IEnumerator TransitionAnimation(System.Action onAnimationEnd)
    {
        yield return new WaitForSeconds(1f);
        bool finishZoom = false;
        while (!finishZoom)
        {
            finishZoom = FindAnyObjectByType<CameraMagnify>().GoToCenter();
            yield return null;
        }
        GameObject newPart = GameObject.Find("NewPart");
        bool partIsCollected = false;
        float movespeed = 10f;

        while (!partIsCollected && newPart != null)
        {
            newPart.transform.position =
                Vector3.MoveTowards(
                    newPart.transform.position,
                    new Vector3(UIManager.Instance.CollectedPosition.position.x, UIManager.Instance.CollectedPosition.position.y, UIManager.Instance.CollectedPosition.position.z),
                    movespeed * Time.deltaTime);
            newPart.transform.localScale =
                Vector3.MoveTowards(
                    newPart.transform.localScale,
                    new Vector3(0, 0, transform.position.z),
                    1 * Time.deltaTime);
            if (newPart.transform.position.x == UIManager.Instance.CollectedPosition.position.x)
            {
                partIsCollected = true;
                AudioManager.Instance.PlayPlayerSounds("Get Item");

                Destroy(newPart);
            }
            yield return null;
        }



        GameObject player = GameObject.FindGameObjectWithTag("Player");
        float progress = 0f;
        float rotateSpeed = 2f;
        Quaternion from = player.transform.rotation;
        Quaternion to = Quaternion.identity;
        while (progress <= 1f)
        {
            progress += rotateSpeed * Time.deltaTime;
            player.transform.rotation = Quaternion.Slerp(from, to, progress);
            yield return null;
        }

        bool playerIsOutside = false;
        while (!playerIsOutside)
        {
            player.transform.Translate(Vector3.up * playerExitSpeed * Time.deltaTime);
            Vector3 viewport = Camera.main.WorldToViewportPoint(player.transform.position);
            if (viewport.y >= 1.1) playerIsOutside = true;
            yield return null;

        }

        onAnimationEnd();

    }
    private void SaveCheckPoint()
    {
        LevelManager.LastCheckPoint = WaveCount;
    }

    public void ClearCurrentWave(bool _clearedByPlayer)
    {
        print(_clearedByPlayer);
        clearedByPlayer = _clearedByPlayer;
        if (ongoingGenerateTask>0)
        {
            StopCoroutine(generateRoutine);
            StopAllCoroutines();
            foreach (GameObject warning in warnings) Destroy(warning);
            warnings.Clear();
            ongoingGenerateTask = 0;
        }

        if (clearedByPlayer)
        {
            int coin = 0;
            foreach (EnemyInfo info in waveData[WaveCount].EnemyInfos)
            {
                coin += info.number*info.prefab.GetComponent<EnemyStatsManager>().GetDropCoin();
            }
            PlayerStatsManager.Instance.TryChangeCoin(coin);
        }


        foreach (GameObject enemy in Enemys)
        {
            Destroy(enemy);
        }
        Enemys.Clear();
        EnemyCount = 0;
        clearedByPlayer = true;
    }

    private void TriggerEvents(List<EventObjectContainer> events)
    {
        foreach (var eventContainer in events)
        {
            GameEventManager.Instance.HandleEvent(GameEvent.CreateEventFromContainer(eventContainer));
        }
    }


    private void MoveTowardCenter(Transform target)
    {
        target.position += (-target.position) * adjustFactor;
    }

}

[Serializable]
public class WaveData
{
    public EnemyInfo[] EnemyInfos;
    public float StartWaitTime = 1f;
    public List<EventObjectContainer> events;
    public WaveType Type = WaveType.Normal;
}

[Serializable]
public class EnemyInfo
{
    public GameObject prefab;
    public int number;
    public SpawnPosition spawnPosition;
    public Transform SpawnPosition;
    public float MaxXOffset;
    public float MaxYOffset;
    public float spawnTimeInterval;
    public float timeToNextInfo;
}

public enum SpawnPosition
{
    Top, Bottom, Left, Right, TopRight, TopLeft, BottomRight, BottomLeft
}

public enum WaveType
{
    Boss, MidBoss, Normal, Prepare
}

