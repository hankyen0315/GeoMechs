using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using static TMPro.SpriteAssetUtilities.TexturePacker_JsonArray;
using UnityEngine.UI;
using TMPro;

public class AssembleManager : MonoBehaviour
{
    [SerializeField]
    private bool RotationAdjusting ;
    [SerializeField]
    private SpriteRenderer RotationGauge;
    [SerializeField]
    private float minAdjustableAngle = 30f;
    [SerializeField]
    private float maxMouseRange;
    [SerializeField]
    private float minMouseRange;
    [SerializeField]
    private int delayFrameCount;

    private Vector2 mousePosition;
    private float mouseDistance;
    private GameObject selected;


    [HideInInspector]
    public GameObject ChosenPart;
    //GameObject[] frames;
    //public TextMeshProUGUI PartDetailText;
    //public TextMeshProUGUI PriceText;
    //private Dictionary<string, string> chosenPartDetail;

    //public static ChosePart Instance { get; private set; }
    public static AssembleManager Instance { get; private set; }
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
    public void SetChosen(GameObject part)
    {
        print(part.name + "lmage clicked ");
        ChosenPart = part;
        UIManager.Instance.ChangePartDetailText(part);
        UIManager.Instance.PlayPreviewVideo(part);
    }

    //call by pointer event
    public void HighlightChosen(RawImage chosenFrame)
    {
        UIManager.Instance.HighlightedChosen(chosenFrame);
    }

    public bool BuyPart()
    {
        if (Instance.ChosenPart == null) return false;

        int partPrice = 0;
        if (Instance.ChosenPart.GetComponent<Part>())
        {
            partPrice = Instance.ChosenPart.GetComponent<Part>().Price;
        }
        else
        {
            print("chosed object does not have a Attack or Buff component on it");
        }


        bool purchaseSucceed = PlayerStatsManager.Instance.TryChangeCoin(-partPrice);
        if (!purchaseSucceed)
        {
            UIManager.Instance.ShowMessage("Not enough coin!");
        }
        return purchaseSucceed;
    }

    public void AddPart(GameObject connectPoint, Transform parent, int layerOrder)
    {
        GameObject newPart = Instantiate(Instance.ChosenPart, connectPoint.transform.position, connectPoint.transform.rotation, parent);
        newPart.GetComponent<Part>().SpriteRenderer.sortingOrder = layerOrder + 1;
        newPart.GetComponent<Part>().ConnectTo = connectPoint;

        newPart.GetComponent<SelectPart>().Select();
        RotatePart();
    }

    public void RemovePart()
    {
        GameObject SelectPart = GameObject.FindGameObjectWithTag("SelectPart");
        if (SelectPart == null) return;
        if (!SelectPart.GetComponent<Part>().IsPartAtTail)
        {
            UIManager.Instance.ShowMessage("Can't remove part not at the end!");
            return;
        }
        if (SelectPart.GetComponent<Part>())
        {
            PlayerStatsManager.Instance.TryChangeCoin(SelectPart.GetComponent<Part>().Price);
        }
        GameObject connectTo = SelectPart.GetComponent<Part>().ConnectTo;
        connectTo.GetComponent<SpriteRenderer>().enabled = true;
        connectTo.GetComponent<CircleCollider2D>().enabled = true;
        connectTo.tag = "RedCircle";
        Destroy(SelectPart);
    }

    public void RotatePart()
    {
        StartCoroutine(EnterRotatePhase());
    }

    private IEnumerator EnterRotatePhase()
    {
        for (int i = 0; i < delayFrameCount; i++)
        {
            yield return new WaitForEndOfFrame();
        }
        if (GameObject.FindGameObjectWithTag("SelectPart") == null) yield break;
        selected = GameObject.FindGameObjectWithTag("SelectPart");
        RotationAdjusting = true;
        RotationGauge.enabled = true;
        gameObject.transform.position = selected.transform.position;
    }


    private void Update()
    {
        if (RotationAdjusting)
        {
            RotatingWithMousePosition();
        }
    }

    private void RotatingWithMousePosition()
    {
        mousePosition = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
        mouseDistance = Vector2.Distance(new Vector2(mousePosition.x, mousePosition.y), new Vector2(selected.transform.position.x, selected.transform.position.y));
        if (mouseDistance > maxMouseRange || mouseDistance < minMouseRange) return;

        float angleInRad = Mathf.Atan2(mousePosition.y - selected.transform.position.y, mousePosition.x - selected.transform.position.x);
        float angleInDeg = Mathf.Rad2Deg * angleInRad;
        int sign = (angleInDeg > 0) ? 1 : -1;
        //print("original angle: " + angleInDeg.ToString());
        //print("angle/min: " + ((int)(Mathf.Abs(angleInDeg) / minAdjustableAngle)).ToString());
        if (angleInDeg < -165f)
        {
            angleInDeg = 6 * minAdjustableAngle;
        }
        else
        {
            angleInDeg = (int)(Mathf.Abs(angleInDeg) / minAdjustableAngle) * minAdjustableAngle;
        }
        //angleInDeg = (int)(Mathf.Abs(angleInDeg) / minAdjustableAngle) * minAdjustableAngle;
        //if (angleInDeg < -150f)
        //{
        //    angleInDeg = 6 * minAdjustableAngle;
        //}
        //else
        //{
        //    angleInDeg = (int)(angleInDeg / minAdjustableAngle) * minAdjustableAngle;
        //}
        selected.transform.rotation = Quaternion.Euler(0f, 0f, sign*angleInDeg-90);

        if (LeftMouseClick() || NotSelected())
        {
            EndAdjusting();
        }
    }


    //Need fix: the mouse click is detected in SelectPart, but not here
    private bool LeftMouseClick()
    {
        return Mouse.current.leftButton.wasPressedThisFrame;

    }
    private bool NotSelected()
    {
        return !selected.CompareTag("SelectPart");
    }

    private void EndAdjusting()
    {
        RotationAdjusting = false;
        RotationGauge.enabled = false;
        selected.GetComponent<SelectPart>().Unselect();
        selected = null;
    }
}
