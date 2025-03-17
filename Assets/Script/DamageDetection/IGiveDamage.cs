using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IGiveDamage
{
    void OnHit();
    string GetAttacker();
    float GetAttackPower();
    bool Active();
}
