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
    private Animator anim;
    private bool isRunning;
    private float LastClickedTime;
    private float LastComboEnd;
    private int ComboCounter;
    public Queue<AttackAnimCombo> Combos;
    private Animator AttackAnim;
    
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        inputActions = new InputActions();
        anim = GetComponent<Animator>();
        inputActions.Player.Enable();
        AttackAnim = GetComponent<Animator>();
        Combos = new Queue<AttackAnimCombo>();
    }
    // Start is called before the first frame update
    void Start()
    {
        AddToQueue();
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
        if(rb.velocity.x != 0)
        {
            isRunning = true;
        }
        else
        {
            isRunning = false;
        }
        GroundChecker();
        if (inputActions.Player.Attack.IsPressed())
        {
            Attack();
        }
        ExitAttack();
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
        anim.SetBool("isRunning", isRunning);
    }
    private void Jump()
    {
        if (inputActions.Player.Jump.IsPressed() && isGrounded)
        {
            rb.velocity = new Vector2(rb.velocity.x, JumpHeight);
            anim.SetBool("Jump", true);
            anim.SetFloat("yVel", rb.velocity.y);
        }
        else
        {
            anim.SetFloat("yVel", rb.velocity.y);
            anim.SetBool("Jump", false);
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
    private void AddToQueue()
    {
        foreach(var combo in Combos)
        {
            Combos.Enqueue(combo);
        }
    }
    private void Attack()
    {
        //if (Time.time - LastComboEnd > 0.2f && ComboCounter <= Combos.Count)
        //{
        //    CancelInvoke("EndCombo");
        //    if (Time.time - LastClickedTime >= 0.1f)
        //    {
        //        AttackAnim.runtimeAnimatorController = Combos[ComboCounter].animatorOverrideController;
        //        AttackAnim.Play("Attack", 0, 0);
        //        //set the damage here
        //        ComboCounter++;
        //        LastClickedTime = Time.time;
        //        if(ComboCounter+1 > Combos.Count)
        //        {
        //            ComboCounter = 0;
        //        }
        //    }
        //}
    }
    private void ExitAttack()
    {
        //if (AttackAnim.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.95 && AttackAnim.GetCurrentAnimatorStateInfo(0).IsTag("Attack"))
        //{
        //    Invoke("EndCombo", 0.2f);
        //}
    }
    private void EndCombo()
    {
        //ComboCounter = 0;
        //LastComboEnd = Time.time;
    }
}
