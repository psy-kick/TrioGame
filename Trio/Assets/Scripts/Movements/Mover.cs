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
    private bool isGrounded = false;
    [SerializeField]
    private float GroundCheckRadius;
    private bool isFacingRight = true;
    private Vector2 InputAxis;
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
        if (!isFacingRight && InputAxis.x > 0)
        {
            Flip();
        }
        else if(isFacingRight && InputAxis.x < 0)
        {
            Flip();
        }
        GroundChecker();
    }
    private void FixedUpdate()
    {
        Jump();
        Move();
    }
    private void Move()
    {
        InputAxis = inputActions.Player.Mover.ReadValue<Vector2>();
        rb.velocity = new Vector2(InputAxis.x * MoveSpeed, rb.velocity.y);
    }
    private void Jump()
    {
        if (inputActions.Player.Jump.IsPressed() && isGrounded)
        {
            rb.velocity = new Vector2(rb.velocity.x, JumpHeight);
        }        
    }
    private void GroundChecker()
    {
        isGrounded = Physics2D.OverlapCircle(Groundcheck.position, GroundCheckRadius, GroundLayer);
    }
    private void Flip()
    {
        isFacingRight = !isFacingRight;
        Vector3 LocalScale = transform.localScale;
        LocalScale.x *= -1f;
        transform.localScale = LocalScale;
    }
}
