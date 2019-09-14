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

    [SerializeField]
    private float hitDelayTime, hitDelayCounter;

    private bool justBeenHit;



    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
    }


    private void Update()
    {
        if (justBeenHit)
        {

            hitDelayCounter -= Time.deltaTime;
            if(hitDelayCounter < 0)
                justBeenHit= false;
        }
    }


    public void TakeDamage(float damage)
    {
        if(justBeenHit) return;

        currentHealth -= damage;
        if (currentHealth <= 0)
        {
           
            GameManager.Instance.pigsKilled ++;
            audioSource.Play();
            pigRoot.Kill();
        }
    }
}
