using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.InputSystem;

public class Mover : MonoBehaviour
{
    private InputActions inputActions;
    private Rigidbody2D rb;
    [SerializeField]
    private float MoveSpeed;
    [SerializeField]
    private float JumpHeight;
    [SerializeField]
    private Transform Groundcheck;
    [SerializeField]
    private LayerMask GroundLayer;
    [SerializeField]
    private bool isGrounded;
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        inputActions = new InputActions();
        inputActions.Player.Enable();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        GroundChecker();
    }
    private void FixedUpdate()
    {
        Move();
        Jump();
    }
    private void Move()
    {
        Vector2 InputAxis = inputActions.Player.Mover.ReadValue<Vector2>();
        rb.velocity = new Vector2(InputAxis.x, 0f) * MoveSpeed;
    }
    private void Jump()
    {
        if (inputActions.Player.Jump.IsPressed() && isGrounded)
        {
            rb.AddForce(Vector2.up * JumpHeight, ForceMode2D.Impulse);
        }
        else
        {
            isGrounded = false;
        }
        
    }
    private void GroundChecker()
    {
        isGrounded = Physics2D.Raycast(Groundcheck.position, Vector2.down * 0.1f, GroundLayer);
    }
}
