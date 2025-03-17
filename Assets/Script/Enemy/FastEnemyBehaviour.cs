using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FastEnemyBehaviour : MonoBehaviour, IMovable
{
    [SerializeField]
    protected int wayPointAmount = 3;
    [SerializeField]
    protected float wayPointDistance;
    [SerializeField]
    protected float offset = 0.3f;
    [SerializeField]
    protected float speed;
    [SerializeField]
    protected float rotateSpeed;
    private Quaternion from;
    private Quaternion to;
    private float progress = 0f;

    [SerializeField]
    protected float rotateDelta = 15f;
    [SerializeField]
    protected int nextPointID = 0;
    protected List<Vector3> wayPoints = new List<Vector3>();
    [SerializeField]
    protected GameObject circleShieldPrefab;
    private GameObject circleShield;

    [SerializeField]
    protected GameObject warning;
    [SerializeField]
    protected float warningInterval = 0.25f;
    [SerializeField]
    protected float preAttackTime = 0.5f;



    protected BehaviourState bState = BehaviourState.OffScreen;
    protected MoveState mState = MoveState.Normal;
    //private void Start()
    //{
    //    wayPoints = new List<Vector3>(wayPointAmount);
    //}
    private void Update()
    {
        switch (bState)
        {
            case BehaviourState.OffScreen:
                MoveToScreen();
                break;
            case BehaviourState.Decide:
                Decide();
                break;
            case BehaviourState.Wait:
                Wait();
                break;
            case BehaviourState.Move:
                MoveOnPath(wayPoints[nextPointID]);
                break;
            case BehaviourState.OnWayPoint:
                OnWayPoint();
                break;
            case BehaviourState.Rotate:
                FaceWayPoint();
                break;
            default:
                break;
        }
    }

    private void MoveToScreen()
    {
        Vector3 viewport = Camera.main.WorldToViewportPoint(transform.position);
        if (viewport.x >= 1 || viewport.x <= 0 || viewport.y >= 1 || viewport.y <= 0)
        {
            transform.position = Vector3.MoveTowards(transform.position, Vector3.zero, speed * Time.deltaTime);
            return;
        }
        bState = BehaviourState.Decide;
    }

    protected virtual void Decide()
    {
        Vector3 pre = transform.position;
        for (int i = 0; i < wayPointAmount; i++)
        {
            //todo: seems like it would go outside the screen
            wayPoints.Add(GetNextWayPoint(pre));

            pre = wayPoints[i];
        }
        CreateShield();
        StartCoroutine(CreateWarning());
        bState = BehaviourState.Wait;
    }



    private Vector3 GetNextWayPoint(Vector3 prePoint)
    {
        Transform player = PlayerStatsManager.Instance.gameObject.transform;
        Vector3 moveVector = player.position - prePoint;
        float angleOffset = Random.Range(-rotateDelta, rotateDelta);
        //print("offset:" + angleOffset.ToString());
        //print("move vector before:" + moveVector.ToString());
        moveVector = Quaternion.AngleAxis(angleOffset, Vector3.forward) * moveVector;
        //print("move vector after: " + moveVector.ToString());
        moveVector = moveVector.normalized * wayPointDistance;
        Vector3 nextPoint = prePoint + moveVector;
        Vector3 screenBottomLeft = Camera.main.ScreenToWorldPoint(new Vector3(0, 0, Camera.main.transform.position.z));  //Ãä¬Éªº©N©N
        Vector3 screenTopRight = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));

        nextPoint.x = Mathf.Clamp(nextPoint.x, screenBottomLeft.x + offset, screenTopRight.x - offset);
        nextPoint.y = Mathf.Clamp(nextPoint.y, screenBottomLeft.y + offset, screenTopRight.y - offset);
        return nextPoint;
    }




    private IEnumerator CreateWarning()
    {
        for (int i = 0; i < wayPoints.Count; i++)
        {
            LineRenderer line;
            GameObject obj = Instantiate(warning, wayPoints[i], Quaternion.identity);
            Vector3[] startEnd = new Vector3[2];
            if (i > 0)
            {
                startEnd[0] = wayPoints[i - 1];
                startEnd[1] = wayPoints[i];
            }
            else
            {
                startEnd[0] = transform.position;
                startEnd[1] = wayPoints[i];
            }
            line = obj.GetComponent<LineRenderer>();
            line.startWidth = 0.1f;
            line.endWidth = 0.1f;
            line.SetPositions(startEnd);
            yield return new WaitForSeconds(warningInterval);
        }
        //foreach (Vector3 point in wayPoints)
        //{
        //    Instantiate(warning, point, Quaternion.identity);
        //    yield return new WaitForSeconds(warningInterval);
        //}
        yield return new WaitForSeconds(preAttackTime);
        CloseShield();
        Vector3 relativePos = wayPoints[0] - transform.position;
        float angle = Mathf.Atan2(relativePos.y, relativePos.x) * Mathf.Rad2Deg - 90;
        to = Quaternion.AngleAxis(angle, Vector3.forward);
        from = transform.rotation;
        progress = 0f;
        bState = BehaviourState.Rotate;
    }

    private void CreateShield()
    {
        circleShield = Instantiate(circleShieldPrefab, transform);
    }
    private void CloseShield()
    {
        Destroy(circleShield);
    }

    protected virtual void Wait()
    {
        return;
    }

    protected virtual void MoveOnPath(Vector3 goal)
    {
        if (Vector3.Distance(transform.position, goal) < 0.01f)
        {
            bState = BehaviourState.OnWayPoint;
        }
        transform.position = Vector3.MoveTowards(transform.position, goal, speed*Time.deltaTime);
    }
    protected virtual void OnWayPoint()
    {
        print("on way point: " + nextPointID.ToString());
        nextPointID++;
        if (nextPointID == wayPointAmount)
        {
            OnLastPoint();
            return;
        }
        else
        {
            
            Vector3 relativePos = wayPoints[nextPointID] - transform.position;
            float angle = Mathf.Atan2(relativePos.y, relativePos.x) * Mathf.Rad2Deg - 90;
            to = Quaternion.AngleAxis(angle, Vector3.forward);
            from = transform.rotation;
            progress = 0f;
            bState = BehaviourState.Rotate;
            //FaceWayPoint()
        }
    }
    protected virtual void OnLastPoint()
    {
        nextPointID = 0;
        wayPoints.Clear();
        bState = BehaviourState.Decide;
    }
    protected virtual void OnSecondLastPoint()
    {
        return;
    }

    protected virtual void FaceWayPoint()
    {
        progress = progress + Time.deltaTime * rotateSpeed;
        transform.rotation = Quaternion.Slerp(from, to, progress);
        if (progress >= 1f)
        {
            bState = BehaviourState.Move;
        }


    }

    public virtual void SlowDown(float slowDownFactor)
    {
        speed *= slowDownFactor;
        rotateSpeed *= slowDownFactor;
        mState = MoveState.Slow;
    }

    public virtual void ResumeSpeed(float slowDownFactor)
    {
        speed /= slowDownFactor;
        rotateSpeed /= slowDownFactor;
        mState = MoveState.Normal;
    }

    public MoveState GetState()
    {
        return mState;
    }

    protected enum BehaviourState 
    {
        Decide, Wait, Move, OnWayPoint, Rotate, OffScreen
    }


}
