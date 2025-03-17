using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerControl : MonoBehaviour, IMovable
{
    public bool CanMove = false;

    private Vector2 moveDirection;
    private float slowDown = 1f;
    private int rotateDirection;
    private Vector3 mousePosition;
    private float lookAngle;
    private float mouseDistance;
    [SerializeField]
    private float ignoreMouseRange;

    private PlayerStatsManager statsManager;

    private MoveState moveState = MoveState.Normal;

    private void Start()
    {
        statsManager = PlayerStatsManager.Instance;
    }

    private void FixedUpdate()
    {
        if (CanMove == false)
        {
            return;
        }
        Move();
        //choose one of the following two rotation methods
        Rotate();
        LookAt();
    }

    private void Rotate()
    {
        transform.Rotate(new Vector3(0f, 0f, rotateDirection * statsManager.GetRotateSpeed()* slowDown * Time.deltaTime));
    }
    private void LookAt()
    {
        // to prevent override Rotate method, this if block is going to be removed in the future
        if (lookAngle == 0)
        {
            return;
        }
        transform.rotation = Quaternion.Euler(0f, 0f, lookAngle);
    }
    private void Move()
    {
        Vector3 moveVector = new Vector3(moveDirection.x, moveDirection.y, 0f);
        transform.Translate(moveVector * statsManager.GetMoveSpeed()* slowDown * Time.deltaTime, Space.World);
        Vector3 pos = Camera.main.WorldToViewportPoint(transform.position);
        pos.x = Mathf.Clamp01(pos.x);
        pos.y = Mathf.Clamp01(pos.y);
        transform.position = Camera.main.ViewportToWorldPoint(pos);
    }


    // the input data would be send to following method by the unity input system
    public void OnMove(InputAction.CallbackContext context)
    {
        moveDirection = context.ReadValue<Vector2>();
    }
    public void OnRotate(InputAction.CallbackContext context)
    {
        float value = context.ReadValue<float>();

        // change it to digital mode 
        if (value == 0)
        {
            rotateDirection = 0;
            return;
        }
        rotateDirection = value > 0 ? 1 : -1; 
    }
    public void OnMouseMove(InputAction.CallbackContext context)
    {
        mousePosition = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());

        mouseDistance = Vector2.Distance(new Vector2(mousePosition.x, mousePosition.y), new Vector2(transform.position.x, transform.position.y));
        if (mouseDistance < ignoreMouseRange)
        {
            return;
        }

        float angleInRad = Mathf.Atan2(mousePosition.y - transform.position.y, mousePosition.x - transform.position.x);
        lookAngle = (180 / Mathf.PI) * angleInRad - 90;
    }

    public void OnActivateOverdrive(InputAction.CallbackContext context)
    {
        if (context.performed && CanMove)// Is CanMove a good name for it?
        {
            GetComponent<OverdriveManager>().ActivateOverdrive();
        }
    }


    //consider if the slow down and resume need to have a uniform implementation
    public void SlowDown(float slowDownFactor)
    {
        moveState = MoveState.Slow;
        slowDown = slowDownFactor;
    }

    public void ResumeSpeed(float slowDownFactor)
    {
        moveState = MoveState.Normal;
        slowDown = 1f;
    }

    public MoveState GetState()
    {
        return moveState;
    }
}
