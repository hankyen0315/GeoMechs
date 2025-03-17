using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SinusoialMove : MonoBehaviour
{
    [Min(0f)]
    public float MoveSpeed = 1f;
    [Min(0f)]
    public float SinusoialFrequency = 20f;
    [Min(0f)]
    public float SinusoialMagnitude = 1f;

    public float RightBount = 5f;
    public float LeftBound = -5f;

    bool facingRight = true;
    float Y;

    // Start is called before the first frame update
    void Start()
    {
        if (LeftBound > RightBount)
            (LeftBound, RightBount) = (RightBount, LeftBound);

        Y = transform.position.y; 
    }

    // Update is called once per frame
    void Update()
    {
        CheckBound();

        if (facingRight)
            MoveRight();
        else
            MoveLeft();
    }

    void CheckBound()
    {
        if (transform.position.x < LeftBound)
            facingRight = true;
        else if (transform.position.x > RightBount)
            facingRight = false;
    }

    void MoveRight()
    {
        Vector3 motion = transform.position + Vector3.right * MoveSpeed * Time.deltaTime;
        motion.y = Y+ (Mathf.PingPong(Time.time * SinusoialFrequency, 1) - 0.5f) * SinusoialMagnitude;
        transform.position = motion;
    }

    void MoveLeft()
    {
        Vector3 motion = transform.position + Vector3.left * MoveSpeed * Time.deltaTime;
        motion.y =Y+ (Mathf.PingPong(Time.time * SinusoialFrequency, 1) - 0.5f) * SinusoialMagnitude;
        transform.position = motion;
    }
}
