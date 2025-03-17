using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageColorChanger : MonoBehaviour
{
    public SpriteRenderer sp;
    public EnemyStatsManager statsManager;
    private float maxHealth;
    private float currentHealth;
    private float healthRatio;
    private Color originalColor;
    private void Awake()
    {
        maxHealth = statsManager.GetHealth();
        currentHealth = statsManager.GetHealth();
        originalColor = sp.color;
    }
    private void Update()
    {
        if (currentHealth == statsManager.GetHealth()) return;
        currentHealth = statsManager.GetHealth();
        healthRatio = currentHealth / maxHealth;
        sp.color = Color.Lerp(originalColor, new Color(1, 0, 0, 0.5f), 1-healthRatio);
    }


}
