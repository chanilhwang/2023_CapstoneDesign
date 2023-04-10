using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class CollideToDamage : MonoBehaviour
{
    public int damageAmount = 10;
    public float knockBack = 0.5f;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            HealthController playerHealth = collision.gameObject.GetComponent<HealthController>();

            if(playerHealth != null)
            {
                playerHealth.TakeDamage(damageAmount);
            }
        }
    }
}
