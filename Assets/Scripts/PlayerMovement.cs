using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    // = (x, y)
    Vector2 moveInput;
    [SerializeField] float moveSpeed = 5f;

    Rigidbody2D myRigidbody;

    void Awake() {
        myRigidbody = GetComponent<Rigidbody2D>();
    }

    void Start()
    {
        
    }

    void Update()
    {
        Run();
    }

    // Will be called on every change in movement bindings (press right-arrow, let go of left-arrow, ...)
    void OnMove(InputValue value)
    {
        // Assign which movement happened to moveInput
        // value.Get<Vector2>() = (x, y) (example: -1.00, 0.00) (0.00, 0.00 when letting go of movement button)
        moveInput = value.Get<Vector2>();

        Debug.Log(moveInput);
    }

    void Run()
    {
        // Player may only move on x-axis
        // Just keep current velocity on y-axis (gravity)
        Vector2 playerVelocity = new Vector2(moveInput.x * moveSpeed, myRigidbody.velocity.y);

        myRigidbody.velocity = playerVelocity;
    }
    
}
