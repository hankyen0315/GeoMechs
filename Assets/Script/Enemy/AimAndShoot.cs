using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AimAndShoot : MonoBehaviour
{
    private Transform player;
    [SerializeField]
    private float rotationOffset = 90f;
    [SerializeField]
    private float rotateSpeed;
    [SerializeField]
    private float threshold;
    [SerializeField]
    private float timeout;
    [SerializeField]
    private GameObject warning;
    private bool coolDown = false;
    [SerializeField]
    private float coolDownTime;
    private float timer;


    // Start is called before the first frame update
    void Start()
    {
        timer = 0f;
        coolDown = false;
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        
        if (coolDown) return;
        timer += Time.deltaTime;
        Vector3 relativePos = player.position - gameObject.transform.position;
        float angle = Mathf.Atan2(relativePos.y, relativePos.x) * Mathf.Rad2Deg - rotationOffset;
        Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);

        gameObject.transform.rotation = Quaternion.Lerp(gameObject.transform.rotation, rotation, Time.deltaTime * rotateSpeed);
        if (timer > timeout)
        {
            Debug.Log("aimed");
            GetComponent<CalculateEffect>().TriggerMultipleTimes(1);
            timer = 0f;
            StartCoroutine(CoolDown());
        }
    }

    private IEnumerator CoolDown()
    {
        coolDown = true;
        warning.SetActive(false);
        yield return new WaitForSeconds(coolDownTime);
        coolDown = false;
        warning.SetActive(true);
    }

    private void OnDestroy()
    {
        StopCoroutine(CoolDown());
    }
}
