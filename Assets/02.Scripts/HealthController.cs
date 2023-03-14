using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthController : MonoBehaviour
{
    public Image healthBarImage;

    public int startingHealth = 100;
    private int currentHealth = 0;
    public int maxHealth = 100;

    void Start()
    {
        if(GetComponent<EnemyHealthBar>() != null)
            healthBarImage = GetComponent<EnemyHealthBar>().healthBarImage;
        currentHealth = startingHealth;
    }

    public void UpdateHealthBar()
    {
        healthBarImage.fillAmount = (float)currentHealth / maxHealth;
    }

    public void TakeDamage(int amount)
    {
        currentHealth -= amount;
        UpdateHealthBar();
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        //die
        Destroy(gameObject);
        Destroy(healthBarImage.gameObject);
    }
}
