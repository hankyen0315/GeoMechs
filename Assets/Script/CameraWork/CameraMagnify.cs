using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMagnify : MonoBehaviour
{
    //[SerializeField]
    //private float zoomInSize;
    [SerializeField]
    private float normalSize;
    [SerializeField]
    private float zoomSpeed = 100;
    private Camera cam;

    private void Start()
    {
        cam = GetComponent<Camera>();
    }


    public bool GoToEnemy(Vector2 enemyPosition, float targetSize)
    {
        cam.orthographicSize = Mathf.MoveTowards(cam.orthographicSize, targetSize, zoomSpeed * Time.deltaTime);
        gameObject.transform.position = Vector3.MoveTowards(gameObject.transform.position, new Vector3(enemyPosition.x, enemyPosition.y, transform.position.z), zoomSpeed * Time.deltaTime);
        return cam.orthographicSize == targetSize;
    }

    public bool GoToCenter()
    {
        cam.orthographicSize = Mathf.MoveTowards(cam.orthographicSize, normalSize, zoomSpeed * Time.deltaTime);
        gameObject.transform.position = Vector3.MoveTowards(gameObject.transform.position, new Vector3(0, 0, transform.position.z), zoomSpeed * Time.deltaTime);
        return cam.orthographicSize == normalSize;
    }
}
