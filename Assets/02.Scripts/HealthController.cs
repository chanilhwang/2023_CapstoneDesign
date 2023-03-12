using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthController : MonoBehaviour
{
    public int startingHealth = 100;
    private int currentHealth = 0;

    void Start()
    {
        currentHealth = startingHealth;
    }

    public void TakeDamage(int amount)
    {
        currentHealth -= amount;
        if(currentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        //die
        Destroy(gameObject);
    }
}
