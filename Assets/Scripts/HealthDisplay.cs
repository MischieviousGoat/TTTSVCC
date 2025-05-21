using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthDisplay : MonoBehaviour
{
    private RectTransform healthBar;
    public Target playerHealth;

    private float initWidth;

    private void Awake()
    {
        playerHealth = GameObject.FindObjectOfType<Target>();
        healthBar = transform.Find("AuraBar").GetComponent<RectTransform>();
        initWidth = healthBar.sizeDelta.x;
    }

    private void Update()
    {
        RefreshHealth();
    }

    private void RefreshHealth()
    {
        healthBar.sizeDelta = new Vector2((playerHealth.health / 100) * initWidth, healthBar.sizeDelta.y);
    }
}
