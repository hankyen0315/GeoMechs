using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Radarline : MonoBehaviour
{
    [SerializeField]
    private float speed = 3.0f;
    private void FixedUpdate()
    {
        gameObject.transform.Rotate(Vector3.back * speed);
    }


}
