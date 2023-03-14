using System.Collections;
using System.Collections.Generic;
using UnityEditor.UIElements;
using UnityEngine;

public class AttackController : MonoBehaviour
{
    public int damage;
    public float knockbackForce;
    public float life = 1;
    public ParticleSystem hitEffect;
    public LayerMask hitLayer;
    public Transform hitSubject = null;

    private HashSet<GameObject> hitEnemies = new HashSet<GameObject>();

    private Collider2D col;

    void Start()
    {
        col = gameObject.AddComponent<BoxCollider2D>();
        col.isTrigger = true;
    }

    private void Update()
    {
        life -= Time.deltaTime;
        if(life <= 0)
        {
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer != hitLayer || hitEnemies.Contains(other.gameObject))
            return;
        hitEnemies.Add(other.gameObject);
        HealthController health = other.GetComponent<HealthController>();
        if (health != null)
        {
            health.TakeDamage(damage);

            Rigidbody2D rb = other.GetComponent<Rigidbody2D>();
            if(rb != null)
            {
                Vector2 dir = new Vector3(other.transform.position.x - hitSubject.position.x, 0, 0);
                rb.AddForce(dir.normalized * knockbackForce, ForceMode2D.Impulse);
            }

            if(hitEffect != null)
            {
                Instantiate(hitEffect, transform.position, Quaternion.identity);
            }
        }
    }
}
