using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dodge_SCRIPT : MonoBehaviour
{
    public float dodgeDistance = 5.0f;
    private float lastDodgeTime = -5.0f;
    private float buffer = 0.5f;       // ��ɽw��(�Y���ܴ�����m�W�X�a�ϡA���·|�j��X�{�b�e���W�ëO��0.5���Z��)
    public RandomMoveBehaviour baseMove;
    public float dodgeSpeed => baseMove.speed;   // �s�W�@���ܼƨӱ���ʳt��
    [SerializeField]
    private float dodgeCD;

    private Coroutine dodgeCoroutine;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("player_side") && collision.gameObject.GetComponent<Bullet>() != null && Time.time - lastDodgeTime >= dodgeCD)                   //�T�O���������|�A��Ĳ�o
        {
            Bullet attacker = collision.gameObject.GetComponent<Bullet>();
            //float randomX = Random.value < 0.5f ? Random.Range(-2.0f, -1.0f) : Random.Range(1.0f, 2.0f);           //�ǰe��m__Random ��X���Ȥ@�w�|�j��1�άO�p��-1(�קK�µۤl�u�����|�W��)
            //Vector3 randomDirection = new Vector3(randomX, Random.Range(-1.0f, 1.0f), Random.Range(-1.0f, 1.0f));
            int RorL = Random.Range(1, 3);
            Vector3 dodgeDirection = (RorL == 1) ? Vector2.Perpendicular(attacker.rb.linearVelocity) : -Vector2.Perpendicular(attacker.rb.linearVelocity);
            Vector3 newPosition = transform.parent.position + dodgeDirection.normalized * dodgeDistance;

            Vector3 screenBottomLeft = Camera.main.ScreenToWorldPoint(new Vector3(0, 0, Camera.main.transform.position.z));  //��ɪ��N�N
            Vector3 screenTopRight = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));

            newPosition.x = Mathf.Clamp(newPosition.x, screenBottomLeft.x + buffer, screenTopRight.x - buffer);
            newPosition.y = Mathf.Clamp(newPosition.y, screenBottomLeft.y + buffer, screenTopRight.y - buffer);
            if (dodgeCoroutine != null)
            {
                StopCoroutine(dodgeCoroutine);
            }
            
            dodgeCoroutine = StartCoroutine(MoveToPosition(newPosition));  // �ϥΨ�{�ӹ�{���Ʋ���
            lastDodgeTime = Time.time;
        }
    }

    IEnumerator MoveToPosition(Vector3 newPosition)
    {
        while (Vector3.Distance(transform.parent.position, newPosition) > 1.5f)
        {
            print("parent pos: " + transform.parent.position.ToString());
            print("new pos: " + newPosition.ToString());
            transform.parent.position = Vector3.Lerp(transform.parent.position, newPosition, dodgeSpeed * Time.deltaTime);
            yield return null;
        }
        transform.position = newPosition;
        dodgeCoroutine = null;
    }
}



/*
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dodge_SCRIPT : MonoBehaviour
{
    public float dodgeDistance = 5.0f;
    private float lastDodgeTime = -5.0f;
    private float buffer = 0.5f;         // ��ɽw��(�Y���ܴ�����m�W�X�a�ϡA���·|�j��X�{�b�e���W�ëO��0.5���Z��)

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("player_side") && Time.time - lastDodgeTime >= 5.0f)
        {
            Vector3 randomDirection = new Vector3(Random.Range(-1.0f, 1.0f), Random.Range(-1.0f, 1.0f), Random.Range(-1.0f, 1.0f));
            Vector3 newPosition = transform.position + randomDirection.normalized * dodgeDistance;

            
            Vector3 screenBottomLeft = Camera.main.ScreenToWorldPoint(new Vector3(0, 0, Camera.main.transform.position.z));
            Vector3 screenTopRight = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));

            
            newPosition.x = Mathf.Clamp(newPosition.x, screenBottomLeft.x + buffer, screenTopRight.x - buffer);
            newPosition.y = Mathf.Clamp(newPosition.y, screenBottomLeft.y + buffer, screenTopRight.y - buffer);

            transform.position = newPosition;
            lastDodgeTime = Time.time;
        }
    }
}
*/
