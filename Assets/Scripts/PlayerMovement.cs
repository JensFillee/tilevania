using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float moveSpeed = 5f;
    [SerializeField] float jumpSpeed = 5f;
    [SerializeField] float climbSpeed = 5f;
    [SerializeField] float deathForceX = 5f;
    [SerializeField] float deathForceY = 5f;
    [SerializeField] GameObject projectile;
    [SerializeField] Transform slingshot;

    float myGravityScale;

    // = (x, y)
    Vector2 moveInput;
    Rigidbody2D myRigidbody;
    Animator myAnimator;
    CapsuleCollider2D myBodyCollider;
    BoxCollider2D myFeetCollider;

    bool isAlive = true;

    void Awake()
    {
        myRigidbody = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
        myBodyCollider = GetComponent<CapsuleCollider2D>();
        myFeetCollider = GetComponent<BoxCollider2D>();

        myGravityScale = myRigidbody.gravityScale;
        myBodyCollider.sharedMaterial.friction = 0f;
    }

    void Update()
    {
        if (!isAlive)
        {
            return;
        }

        Run();
        FlipSprite();
        ClimbLadder();

        Die();

    }

    // Will be called on every change in movement bindings (press right-arrow, let go of left-arrow, ...)
    void OnMove(InputValue value)
    {
        if (!isAlive)
        {
            return;
        }

        // Assign which movement happened to moveInput
        // value.Get<Vector2>() = (x, y) (example: -1.00, 0.00) (0.00, 0.00 when letting go of movement button)
        moveInput = value.Get<Vector2>();

        Debug.Log(moveInput);
    }

    void OnJump(InputValue value)
    {
        if (!isAlive)
        {
            return;
        }

        // If not touching ground
        if (!myFeetCollider.IsTouchingLayers(LayerMask.GetMask("Ground")))
        {
            // Don't do anything
            return;
        }


        if (value.isPressed)
        {
            myRigidbody.velocity += new Vector2(0f, jumpSpeed);
        }

    }

    void OnFire(InputValue value)
    {
        if (!isAlive)
        {
            return;
        }

        // transform.rotation refers to transform of projectile
        Instantiate(projectile, slingshot.position, transform.rotation);
    }

    void Run()
    {
        // Player may only move on x-axis
        // Just keep current velocity on y-axis (gravity)
        Vector2 playerVelocity = new Vector2(moveInput.x * moveSpeed, myRigidbody.velocity.y);
        myRigidbody.velocity = playerVelocity;

        bool playerHasHorizontalSpeed = Mathf.Abs(myRigidbody.velocity.x) > Mathf.Epsilon;
        myAnimator.SetBool("isRunning", playerHasHorizontalSpeed);
    }

    void FlipSprite()
    {
        // Best practise: Use Mathf.Epsilon (= smallest possible value) instead of 0
        bool playerHasHorizontalSpeed = Mathf.Abs(myRigidbody.velocity.x) > Mathf.Epsilon;

        if (playerHasHorizontalSpeed)
        {
            // Change x-scale to -1 to flip sprite (leave y-scale at 1)
            // Mathf.Sign(value): returns 1 if value is positive, -1 if negative
            transform.localScale = new Vector2(Mathf.Sign(myRigidbody.velocity.x), 1f);
        }

    }

    void ClimbLadder()
    {
        if (!myBodyCollider.IsTouchingLayers(LayerMask.GetMask("Climbing")))
        {
            myRigidbody.gravityScale = myGravityScale;
            myAnimator.SetBool("isClimbing", false);
            return;
        }

        Vector2 climbVelocity = new Vector2(myRigidbody.velocity.x, moveInput.y * climbSpeed);
        myRigidbody.velocity = climbVelocity;

        myRigidbody.gravityScale = 0f;

        bool playerHasVerticalSpeed = Mathf.Abs(myRigidbody.velocity.y) > Mathf.Epsilon;
        myAnimator.SetBool("isClimbing", playerHasVerticalSpeed);
    }


    // void OnCollisionEnter2D(Collision2D other)
    // {
    //     if (other.gameObject.tag == "Enemy")
    //     {
    //         Die();
    //     }
    // }

    void Die()
    {
        // If touching Enemies OR Hazards
        if (myBodyCollider.IsTouchingLayers(LayerMask.GetMask("Enemies", "Hazards")))
        {
            isAlive = false;
            myAnimator.SetTrigger("Dying");
            myRigidbody.velocity = new Vector2(-(Mathf.Sign(myRigidbody.velocity.x)) * deathForceX, deathForceY);

            myBodyCollider.sharedMaterial.friction = 5f;
        }
    }
}