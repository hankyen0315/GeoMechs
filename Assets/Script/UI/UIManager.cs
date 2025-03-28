using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
//using UnityEditor.Experimental.GraphView;
//using static TMPro.SpriteAssetUtilities.TexturePacker_JsonArray;
using UnityEngine.Video;

public class UIManager : MonoBehaviour
{

    [Header("Scene Independent")]
    [SerializeField]
    private TextMeshProUGUI coinTextMesh;
    [SerializeField]
    private TextMeshProUGUI livesTextMesh;
    [SerializeField]
    private TextMeshProUGUI ODTextMesh;
    [SerializeField]
    private TextMeshProUGUI RadarTextMesh;


    [SerializeField]
    private TextMeshProUGUI message;

    [SerializeField]
    private Image playerHealth;
    [SerializeField]
    private Image bossHealth;
    [SerializeField]
    private GameObject bossHealthBar;
    [SerializeField]
    private Image ODMeterImage;

    [SerializeField]
    private Radar radar;
    [SerializeField]
    private GameObject enemyListPanel;
    [SerializeField]
    private GameObject enemyImageTemplate;
    [SerializeField]
    private GameObject enemyImageLayout;

    [SerializeField]
    private GameObject partPanel;


    
    [SerializeField]
    private Image progressBar;
    private float waveLengthUnit;
    private int totalWaveCount;
    [SerializeField]
    private Texture BossIcon;
    [SerializeField]
    private Texture MidBossIcon;
    [SerializeField]
    private Texture PrepareIcon;
    [SerializeField]
    private GameObject iconObject;
    [SerializeField]
    private RawImage playerIcon;
    [SerializeField]
    private RectTransform startPoint;

    
    [SerializeField]
    private GameObject nextPartBtn;
    [SerializeField]
    private GameObject prevPartBtn;
    

    public delegate void UIAction();
    public static UIAction pendingAction = null;

    [SerializeField]
    private TextMeshProUGUI PartDetailText;
    [SerializeField]
    private GameObject VideoFrame;

    [SerializeField]
    private GameObject[] TutorialPages;
    private int currentPage = 1;
    private int PageAmount = 2;
    [SerializeField]
    private TextMeshProUGUI TutorialPageText;

    [Header("Scene Dependent")]
    [SerializeField]
    private WaveManager waveManager;
    public int UnlockedPartAmount;
    private int shownPartAmount = 6;
    private int currentFirstPartID = 0;
    public Transform CollectedPosition;

    public static UIManager Instance { get; private set; }
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

    private void OnEnable()
    {
        if (pendingAction != null)
        {
            pendingAction();
        }   
    }

    private void Start()
    {
        Initialize();
    }
    private void Initialize()
    {
        InitCoinText();
        InitLivesText();
        InitODText();
        //InitRadarText();
        InitProgressBar();
        //CountUnlock();
    }




    public void ChangePartDetailText(GameObject part)
    {
        PartDetailText.text = "";
        Dictionary<string, string> chosenPartDetail = part.GetComponent<Part>().GetPartDetail();
        foreach (var (key, value) in chosenPartDetail)
        {
            if (value != "")
            {
                PartDetailText.text += key + " : " + value + "\n";
            }
                
        }
    }
    public void HighlightedChosen(RawImage chosenFrame)
    {
        GameObject[] frames = GameObject.FindGameObjectsWithTag("Frame");
        foreach (var frame in frames)
        {
            frame.GetComponent<RawImage>().enabled = false;
        }
        chosenFrame.enabled = true;
    }

    public void PlayPreviewVideo(GameObject part)
    {
        VideoFrame.SetActive(true);
        VideoFrame.GetComponentInChildren<VideoPlayer>().clip = part.GetComponent<Part>().PreviewVideo;
        VideoFrame.GetComponentInChildren<VideoPlayer>().Play();
        
    }

    public void UpdatePlayerHealthBar(float ratio)
    {
        playerHealth.fillAmount = ratio;
    }

    public void UpdateBossHealthBar(float ratio)
    {
        bossHealth.fillAmount = ratio;
    }
    public void ShowBossHealth()
    {
        print("show bar");
        bossHealthBar.SetActive(true);
    }
    public void CloseBossHealth()
    {
        bossHealthBar.SetActive(false);
    }

    private void InitProgressBar()
    {
        WaveData[] datas = waveManager.waveData;
        totalWaveCount = datas.Length;
        waveLengthUnit = progressBar.rectTransform.rect.height / totalWaveCount;
        
        for (int i = 0; i < datas.Length; i++)
        {
            Texture icon = null;
            switch (datas[i].Type)
            {
                case WaveType.Boss:
                    icon = BossIcon;
                    break;
                //case WaveType.MidBoss:
                //    icon = MidBossIcon;
                //    break;
                //case WaveType.Normal:
                //    break;
                //case WaveType.Prepare:
                //    icon = PrepareIcon;
                //    break;
                default:
                    break;
            }
            if (icon == null) continue;
            RectTransform rect = startPoint;
            //rect.position += new Vector3(0, waveLengthUnit*i);
            GameObject obj = Instantiate(iconObject ,rect.position + new Vector3(0, waveLengthUnit * i), Quaternion.identity, progressBar.transform);
            obj.GetComponent<RawImage>().texture = icon;
        }
    }

    //private void CountUnlock()
    //{
    //    UnlockedPartAmount = 0;
    //    for (int i = 0; i < partPanel.transform.childCount; i++)
    //    {
    //        if (partPanel.transform.GetChild(i).gameObject.activeSelf) UnlockedPartAmount++;
    //    }
    //}


    public void UpdateProgressBar(int waveCount)
    {
        playerIcon.rectTransform.position = startPoint.position + new Vector3(0f, waveCount * waveLengthUnit);
        progressBar.fillAmount = waveCount / totalWaveCount;
    }

    public void ShowTutorialNextPage()
    {
        if (currentPage == PageAmount) return;
        TutorialPageText.text = (currentPage+1).ToString() + "/" + PageAmount.ToString();
        TutorialPages[currentPage - 1].SetActive(false);
        currentPage++;
        TutorialPages[currentPage-1].SetActive(true);
    }
    public void ShowTutorialPrevPage()
    {
        if (currentPage == 1) return;
        TutorialPageText.text = (currentPage - 1).ToString() + "/" + PageAmount.ToString();
        TutorialPages[currentPage-1].SetActive(false);
        currentPage--;
        TutorialPages[currentPage-1].SetActive(true);
    }


    public void UpdateOverdriveMeter(float ratio)
    {
        ODMeterImage.fillAmount = ratio;
    }
    public void ChangeOverdriveMeterColor(Color color)
    {
        ODMeterImage.color = color;
    }

    private void InitODText()
    {
        string OD_CanAssign = PlayerStatsManager.Instance.GetMaxOverdrive().ToString();
        ChangeODText(OD_CanAssign);
    }
    public void ChangeODText(string OD_CanAssign)
    {
        ODTextMesh.text = "X" + OD_CanAssign;
    }

    //private void InitRadarText()
    //{
    //    string leftChance = PlayerStatsManager.Instance.GetRadarChance().ToString();
    //    ChangeRadarText(leftChance);
    //}
    //public void ChangeRadarText(string chance)
    //{
    //    RadarTextMesh.text = "X" + chance;
    //}


    private void InitCoinText()
    {
        string coinAmount = PlayerStatsManager.Instance.GetCoin().ToString();
        ChangeCoinText(coinAmount);
    }
    public void ChangeCoinText(string coinAmount)
    {
        coinTextMesh.text = coinAmount;
    }

    private void InitLivesText()
    {
        string lives = PlayerStatsManager.Instance.GetLives().ToString();
        ChangeLivesText(lives);
    }
    public void ChangeLivesText(string lives)
    {
        livesTextMesh.text = "X" + lives;
    }


    public void ShowMessage(string msg)
    {
        message.text = msg;
        message.GetComponent<Animator>().SetTrigger("show");
    }

    public void ShowEnemyList()
    {
        //if (!PlayerStatsManager.Instance.CanUseRadar) 
        //{
        //    ShowMessage("can't use radar"); 
        //    return;
        //}

        //ChangeRadarText(PlayerStatsManager.Instance.LeftRadarChance.ToString());
        //PlayerStatsManager.Instance.UseRadar();

        radar.gameObject.GetComponentInChildren<Radarline>(true).gameObject.SetActive(true);
        for (int i = 0; i < enemyImageLayout.transform.childCount; i++)
        {
            Destroy(enemyImageLayout.transform.GetChild(i).gameObject);
        }
        enemyListPanel.SetActive(true);

        List<GameObject> enemyList = radar.GetEnemyList();
        foreach (GameObject enemy in enemyList)
        {
            GameObject enemyImage = Instantiate(enemyImageTemplate, enemyImageLayout.transform);
            if (enemy.GetComponent<EnemyStatsManager>().Break == null)
            {
                enemyImage.GetComponent<RawImage>().texture = null;
            }
            else
            {
                enemyImage.GetComponent<RawImage>().texture = enemy.GetComponent<EnemyStatsManager>().Break.texture;
            }
        }
    }
    public void CloseEnemyList()
    {
        radar.gameObject.GetComponentInChildren<Radarline>(true).gameObject.SetActive(false);
        enemyListPanel.SetActive(false);
        for (int i = 0; i < enemyImageLayout.transform.childCount; i++)
        {
            Destroy(enemyImageLayout.transform.GetChild(i).gameObject);
        }
    }

    public void ShowNewPart(int ID)
    {
        // todo: add animation
        
        partPanel.transform.GetChild(ID).gameObject.SetActive(true);
        //CountUnlock();
        if (UnlockedPartAmount != ID) print("something went wrong on unlock new part");


        UnlockedPartAmount++;
        
        if (UnlockedPartAmount > shownPartAmount)
        {
            nextPartBtn.SetActive(true);
        }
    }


    //temp solution
    public void ShowNextPart()
    {
        partPanel.transform.GetChild(currentFirstPartID).gameObject.SetActive(false);
        currentFirstPartID++;
        if (currentFirstPartID > 0)
        {
            //nextPartBtn.SetActive(false);
            prevPartBtn.SetActive(true);
        }
        if (currentFirstPartID == Mathf.Max(UnlockedPartAmount-shownPartAmount, 0))
        {
            nextPartBtn.SetActive(false);
        }
    }
    public void ShowNewPartObject(GameObject enemy, int ID)
    {
        //if (partPanel.transform.GetChild(ID).gameObject.GetComponentInChildren<AddPart>())
        //{
        //    GameObject NewPartObject = partPanel.transform.GetChild(ID).gameObject.GetComponentInChildren<AddPart>().Part();
        //    GameObject newPart = Instantiate(NewPartObject, enemy.transform.position, enemy.transform.rotation);
        //    for (int i = 0; i < newPart.transform.childCount; i++)
        //    {
        //        if (newPart.transform.GetChild(i).CompareTag("RedCircle"))
        //        {
        //            newPart.transform.GetChild(i).gameObject.SetActive(false);
        //        }
        //    }
        //    print("newPart = " + newPart);
        //    return newPart;
        //}
        //else
        //{
        //    return null;
        //}
        if (partPanel.transform.GetChild(ID).gameObject.GetComponentInChildren<AddPart>())
        {
            partPanel.transform.GetChild(ID).gameObject.GetComponentInChildren<AddPart>().Part(enemy.transform.position);
        }
    }
    public void ShowPrevPart()
    {
        partPanel.transform.GetChild(currentFirstPartID-1).gameObject.SetActive(true);
        currentFirstPartID--;
        if (currentFirstPartID == 0)
        {
            //nextPartBtn.SetActive(true);
            prevPartBtn.SetActive(false);
        }
        if (currentFirstPartID != Mathf.Max(UnlockedPartAmount - shownPartAmount, 0))
        {
            nextPartBtn.SetActive(true);
        }
    }


}
