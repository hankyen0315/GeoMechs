using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlowTimeBullet : Bullet
{
    private Dictionary<IMovable, int> affectingObjects = new Dictionary<IMovable, int>();
    [SerializeField]
    [Range(0, 1)]
    private float slowDownFactor;


    public void AddAffected(IMovable affected) 
    {
        if (affectingObjects.ContainsKey(affected))
        {
            return;
        }
        affectingObjects.Add(affected, 1);
        print("slow down affected");
        affected.SlowDown(slowDownFactor);
    }
    public void RemoveAffected(IMovable affected)
    {
        if (!affectingObjects.ContainsKey(affected))
        {
            return;
        }
        affectingObjects.Remove(affected);
        affected.ResumeSpeed(slowDownFactor);
    }

    public override void DestroySelf()
    {
        foreach (IMovable affected in affectingObjects.Keys)
        {
            affected.ResumeSpeed(slowDownFactor);
        }
        affectingObjects.Clear();
        base.DestroySelf();
    }

    public override void OnHit()
    {
        return;
    }

}
