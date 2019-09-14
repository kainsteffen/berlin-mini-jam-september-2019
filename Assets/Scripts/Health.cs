using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Health : MonoBehaviour
{
    float currentHealth;
    public float maxHealth;

    public UnityEvent onDie;

    public PigEnemy pigRoot;

    private AudioSource audioSource;

    public bool dead= false;



    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
    }



    public void TakeDamage(float damage)
    {
        if (!dead)
        {
            currentHealth -= damage;
            if (currentHealth <= 0)
            {
                dead = true;
                print("died");
                GameManager.Instance.pigsKilled++;
                audioSource.Play();
                pigRoot.Kill();
            }
        }
    }
}
