using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Mover : MonoBehaviour
{
    private InputActions inputActions;
    private Rigidbody2D rb;
    [SerializeField]
    private float MoveSpeed;
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
        
    }
    private void FixedUpdate()
    {
        Move();
    }
    public void Move()
    {
        Vector2 InputAxis = inputActions.Player.Mover.ReadValue<Vector2>();
        rb.AddForce(new Vector2(InputAxis.x, InputAxis.y) * MoveSpeed, ForceMode2D.Force);
        Debug.Log("Arrows pressed");
    }
}
