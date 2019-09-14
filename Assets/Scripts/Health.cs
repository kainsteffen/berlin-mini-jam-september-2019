﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Health : MonoBehaviour
{
    float currentHealth;
    public float maxHealth;

    public UnityEvent onDie;

    private PigEnemy pigRoot;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            GameManager.Instance.pigsKilled ++;
            pigRoot.Kill();
        }
    }
}
