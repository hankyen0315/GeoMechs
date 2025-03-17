using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlowTimeArea : MonoBehaviour
{


    [SerializeField]
    private List<string> affectedTagList;

    [SerializeField]
    private SlowTimeBullet slowTimeBullet;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        print("enter: " + collision.gameObject.name);
        if (!affectedTagList.Contains(collision.gameObject.tag))
        {
            return;
        }
        if (collision.gameObject.GetComponent<IMovable>()?.GetState() == MoveState.Normal)
        {
            print("affect: " + collision.gameObject.name);
            slowTimeBullet.AddAffected(collision.gameObject.GetComponent<IMovable>());
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (!affectedTagList.Contains(collision.gameObject.tag))
        {
            return;
        }
        if (collision.gameObject.GetComponent<IMovable>()?.GetState() == MoveState.Slow)
        {
            slowTimeBullet.RemoveAffected(collision.gameObject.GetComponent<IMovable>());
        }
    }
}
