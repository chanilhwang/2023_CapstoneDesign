using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Charactor2DAttackController : MonoBehaviour
{
    [Header("Attack")]
    [SerializeField] private float delay = 0.5f;
    [SerializeField] private Vector2 size = new Vector2(1, 1);
    [SerializeField] private Vector3 startingPosition = new Vector3(1,0,0);
    [SerializeField] private int damage = 10;
    [SerializeField] private float knockbackForce = 0.5f;

    [Header("Component")]
    [SerializeField] private Animator animator;
    [SerializeField] public PlayerInput playerInput;
    [SerializeField] private GameObject attackObject;

    private float timerDelay;

    void Start()
    {
        if (animator == null)
        {
            animator = GetComponent<Animator>();
        }

        if (playerInput == null)
        {
            playerInput = GetComponent<PlayerInput>();
        }

        //attackAction
        InputAction attackAction = playerInput.actions.FindAction("Attack");
        attackAction.performed += context =>
        {
            if (timerDelay <= 0)
            {
                OnAttackPressed();
                timerDelay = delay;
            }
        };
    }

    private void Update()
    {
        timerDelay -= Time.deltaTime;
    }

    private void OnAttackPressed()
    {
        if (attackObject == null)
        {
            return;
        }
        GameObject obj = Instantiate(attackObject);
        obj.transform.position = startingPosition + transform.position;
        obj.transform.localScale = new Vector3(size.x, size.y, 1);
        
        AttackController ac = obj.GetComponent<AttackController>();
        ac.damage = damage;
        ac.knockbackForce = knockbackForce;
        ac.hitLayer = LayerMask.NameToLayer("Enemy");
    }
}
