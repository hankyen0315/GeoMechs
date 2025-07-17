using UnityEngine;
using System.Collections.Generic;

public static class AttackPathBuilder
{
    public static List<AttackPath> BuildAllAttackPaths(Transform start)
    {
        List<AttackPath> result = new List<AttackPath>();
        BuildAttackPathsRecursive(start, result, new List<Buff>());
        return result;
    }



    private static void BuildAttackPathsRecursive(Transform current, List<AttackPath> result, List<Buff> buffs)
    {
        if (current == null) return;

        bool isEndPoint = current.CompareTag("RedCircle") || current.name == "EnemyGunGO" || current.name == "PlayerGunGO";
        if (isEndPoint)
        {
            AttackPath newPath = new AttackPath
            {
                EndPoint = current,
                BuffsOnPath = new List<Buff>(buffs)
            };
            result.Add(newPath);
            return;
        }

        Buff buff = current.GetComponent<Buff>();
        if (buff != null)
        {
            buffs = new List<Buff>(buffs);
            buffs.Add(buff);
        }

        for (int i = 0; i < current.childCount; i++)
        {
            BuildAttackPathsRecursive(current.GetChild(i), result, buffs);
        }
    }
}
