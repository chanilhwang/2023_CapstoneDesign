using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySimpleMovement : MonoBehaviour
{
    public float speed = 2f;
    public float turnTime = 1f;
    public float raycastDistance = 1f;
    public LayerMask obstacleMask;

    private float turnTimer = 0f;

    // Update is called once per frame
    void Update()
    {
        //bool isGrounded = GetComponent<Collider2D>().IsTouchingLayers(obstacleMask);
        bool hasObstacle = Physics2D.Raycast(transform.position, transform.right, raycastDistance, obstacleMask);

        if(hasObstacle)
        {
            turnTimer = turnTime;
            transform.rotation = Quaternion.AngleAxis(180f, Vector3.up) * transform.rotation;
        }

        turnTimer -= Time.deltaTime;

        if (turnTimer > 0f)
        {
            return;
        }

        transform.Translate(Vector2.right * speed * Time.deltaTime);
    }
}
