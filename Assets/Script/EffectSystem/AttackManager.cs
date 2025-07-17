using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class AttackManager : MonoBehaviour
{
    public bool Auto = false;
    [Tooltip("so far only boss1 use this, so other gameobject can ignore it")]
    public float triggerInterval = 0.5f;
    [SerializeField]
    private float bulletInterval = 0.05f;
    [Tooltip("To make the enemy attack timing has some sort of randomness, so they won't attack at once")]
    [SerializeField]
    private float randomTimeOffsetRange = 0f;



    [SerializeField]
    private bool isEnemy = true; // temp solution QQ
    private bool started = false;
    public bool AutoState;

    private Stack<AttackTask> attackTasks = new Stack<AttackTask>();
    private Dictionary<string, int> attackCountByBulletType = new Dictionary<string, int>();
    private void Awake()
    {
        AutoState = Auto;
    }
    private void Update()
    {
        if (isEnemy && !started)
        {
            ExecuteAllTasks();
        }
    }

    public void ExecuteAllTasks()
    {
        if (!Auto) return;
        foreach (AttackTask task in attackTasks)
        {
            task.attack.RebuildAttackPaths();
            StartCoroutine(task.ExecuteRepeatedly());
        }
        started = true;
    }

    public void StopAllTasks()
    {
        StopAllCoroutines();
        started = false;
    }


    public void RegisterAttack(Attack attack)
    {
        print(gameObject.name + " register task, bullet: " + attack.Bullet.name + " interval: " + attack.AttackInterval.ToString());
        if (!attackCountByBulletType.ContainsKey(attack.Bullet.name))
        {
            attackCountByBulletType[attack.Bullet.name] = 1;
        }
        else
        {
            attackCountByBulletType[attack.Bullet.name] += 1;
        }
        //calculate the start time of this attack, so that it won't overlap with other attacks
        float startTime = (attackCountByBulletType[attack.Bullet.name]-1) * bulletInterval;
        AttackTask newTask = new AttackTask(attack, startTime, randomTimeOffsetRange);
        attackTasks.Push(newTask);
    }

    //assume that only the tail, which is the last element of the task stack, could be remove
    public void UnregisterAttack()
    {
        AttackTask removeTask = attackTasks.Pop();
        attackCountByBulletType[removeTask.attack.Bullet.name]--;
    }

    private struct AttackTask
    {
        public Attack attack;
        private float startTime;
        private float randomTimeOffsetRange;
        public AttackTask(Attack _attack, float _startTime, float offset)
        {
            attack = _attack;
            startTime = _startTime;
            randomTimeOffsetRange = offset;
        }
        public IEnumerator ExecuteRepeatedly()
        {
            yield return new WaitForSeconds(this.startTime);
            while (LevelManager.State == LevelState.Fight)
            {
                attack.AttackOnce();
                float offset = Random.Range(-randomTimeOffsetRange, randomTimeOffsetRange);
                yield return new WaitForSeconds(attack.AttackInterval + offset);
            }
        }
    }





    #region for boss
    public void TriggerMultipleTimes(int times)
    {
        StartCoroutine(StartTrigger(times));
    }
    


    private IEnumerator StartTrigger(int times)
    {
        for (int i = 0; i < times; i++)
        {
            TriggerOnce();
            yield return new WaitForSeconds(triggerInterval);
        }
    }



    private void TriggerOnce()
    {
        foreach (AttackTask task in attackTasks)
        {
            task.attack.AttackOnce();
        }
    }
    #endregion

}



