using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEditor.Timeline.TimelinePlaybackControls;

public class Charactor2DMovementController : MonoBehaviour
{
    [SerializeField] private float walkSpeed = 1f;

    [Header("Run")]
    [SerializeField] private float runSpeed = 3f;
    [SerializeField] private float doubleInputInterval = 0.5f;
    [SerializeField] private float runTime = 0.2f;
    [SerializeField] private float runDelay = 0f;
    private bool isRunning = false;

    [Header("Dash")]
    [SerializeField] private float dashForce = 5f;
    [SerializeField] private float dashTime = 0.2f;
    [SerializeField] private float dashDelay = 0.4f;
    private bool canDash = true;
    private bool isDashing = false;

    [Header("Jump")]
    [SerializeField] private float jumpForce = 1f;
    [SerializeField] private int maxJumps = 2;
    private int jumpRemaining = 0;

    [Header("Components")]
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Animator animator;
    [SerializeField] public PlayerInput playerInput;
    [SerializeField] private float groundCheckRadius;
    [SerializeField] private LayerMask whatIsGround;

    private Vector2 moveDirection;
    private bool isGrounded = false;
    private bool doubleTapDetected = false;
    private float lastTapTime = -1f;
    private float doubleTapTimeThreshold = 0.3f;
    private float speed = 0;
    private string lastMoveButton;

    void Start()
    {
        if(rb == null)
        {
            rb = GetComponent<Rigidbody2D>();
        }
        rb.constraints = RigidbodyConstraints2D.FreezeRotation;

        if(animator == null)
        {
            animator = GetComponent<Animator>();
        }

        if(playerInput == null)
        {
            playerInput = GetComponent<PlayerInput>();
        }

        //moveAction
        InputAction moveAction = playerInput.actions.FindAction("Move");
        moveAction.performed += context =>
        {
            if (isDashing)
            {
                return;
            }
            moveDirection = context.ReadValue<Vector2>();
            OnMovePressed(context.control.name);
            lastMoveButton = context.control.name;
        };
        moveAction.canceled += context =>
        {
            OnMoveReleased();
        };

        //jumpAction
        InputAction jumpAction = playerInput.actions.FindAction("Jump");
        jumpAction.performed += context =>
        {
            if (isDashing)
            {
                return;
            }
            OnJumpPressed();
        };
    }

    private void FixedUpdate()
    {
        CheckGounded();

        if (IsGrounded())
        {
            jumpRemaining = maxJumps;
        }
        else
        {
            if(jumpRemaining == maxJumps)
            {
                jumpRemaining--;
            }
        }
    }

    void Update()
    {
        if(moveDirection != Vector2.zero)
        {
            transform.Translate(new Vector3(moveDirection.x * speed * Time.deltaTime, 0f, 0f));
        }
    }

    void OnMovePressed(string name)
    {
        float currentTime = Time.time;

        if (lastTapTime >= 0f && currentTime - lastTapTime < doubleTapTimeThreshold && !doubleTapDetected && lastMoveButton == name)
        {
            StartCoroutine(Dash());
            doubleTapDetected = true;
        }

        lastTapTime = currentTime;
        speed = walkSpeed;
    }

    private void OnMoveReleased()
    {
        moveDirection = Vector2.zero;
        doubleTapDetected = false;
        isRunning = false;
        speed = walkSpeed;
    }

    private IEnumerator Dash()
    {
        if (IsGrounded())
        {
            isRunning = true;
            //Run
            for(float i = 0; i <= 1; i += Time.deltaTime)
            {
                if (!isRunning)
                    break;
                speed = Mathf.Lerp(walkSpeed, runSpeed, i);
                yield return new WaitForSeconds(Time.deltaTime);
            }
            speed = runSpeed;
        }
        else
        {
            if (jumpRemaining > 0)
            {
                jumpRemaining--;
                canDash = false;
                isDashing = true;
                float originalGravity = rb.gravityScale;
                rb.gravityScale = 0f;
                rb.velocity = new Vector2((moveDirection.x > 0 ? 1 : -1) * dashForce, 0f);
                //대시 이펙트
                yield return new WaitForSeconds(dashTime);
                //대시 이펙트 제거
                rb.gravityScale = originalGravity;
                isDashing = false;
                yield return new WaitForSeconds(dashDelay);
                canDash = true;
            }
        }
    }

    void OnJumpPressed()
    {
        if(jumpRemaining > 0)
        {
            rb.velocity = Vector2.up * jumpForce;

            jumpRemaining--;
        }
    }

    public bool IsGrounded()
    {
        return isGrounded;
    }

    private void CheckGounded()
    {
        isGrounded = Physics2D.OverlapCircle(transform.position, groundCheckRadius, whatIsGround);
    }
}