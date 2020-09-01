using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float maxHealth;
    private float currentHealth;

    void Start()
    {
        currentHealth = maxHealth;
    }

    void Update()
    {
        Death();
    }

    public void TakeDamage(float damageAmount)
    {
        currentHealth -= damageAmount;
    }

    void Death()
    {
        if(currentHealth <= 0)
        {
            Destroy(gameObject);
        }
    }
}
