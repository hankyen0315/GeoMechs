using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;


public class CalculateEffect : MonoBehaviour
{
    private Effect[] effects;
    public bool Auto = false;
    [Tooltip("so far only boss1 use this, so other gameobject can ignore it")]
    public float triggerInterval = 0.5f;
    [SerializeField]
    private float bulletInterval = 0.05f;
    [Tooltip("To make the enemy attack timing has some sort of randomness, so they won't attack at once")]
    [SerializeField]
    private float randomTimeOffsetRange = 0f;

    private Stack<AttackTask> attackTasks = new Stack<AttackTask>();
    private Dictionary<string, int> attackCountByBulletType = new Dictionary<string, int>();

    [SerializeField]
    private bool isEnemy = true; // temp solution QQ
    private bool started = false;
    public bool AutoState;
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
        print("there are " + attackTasks.Count.ToString() + " tasks for " + gameObject.name);
        foreach (AttackTask task in attackTasks)
        {
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
        float startTime = (attackCountByBulletType[attack.Bullet.name]-1) * bulletInterval;
        AttackTask newTask = new AttackTask(attack, startTime, randomTimeOffsetRange);
        attackTasks.Push(newTask);
    }

    //assume that only the tail, which is the last element of the task queue, could be remove
    public void UnregisterAttack()
    {
        print(gameObject.name + "unregister the last task");
        AttackTask removeTask = attackTasks.Pop();
        print("bullet type: " + removeTask.attack.Bullet.name);
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




    //TODO: use AttackTask instead
    //Boss use these method, should be unify in the future
    #region for boss
    public void TriggerMultipleTimes(int times)
    {
        StartCoroutine(StartTrigger(times));
    }
    private void TriggerOnce()
    {
        effects = gameObject.GetComponentsInChildren<Effect>();
        foreach (Effect effect in effects)
        {
            effect.UpdateEffect(effect.gameObject, 1, 1);
        }
    }
    private IEnumerator StartTrigger(int times)
    {
        for (int i = 0; i < times; i++)
        {
            TriggerOnce();
            yield return new WaitForSeconds(triggerInterval);
        }
    }
    #endregion

}



