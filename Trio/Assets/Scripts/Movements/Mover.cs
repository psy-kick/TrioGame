using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Mover : MonoBehaviour
{
    #region Private Variables
    private InputActions inputActions;
    private Rigidbody2D rb;
    [SerializeField]
    private float MoveSpeed = 5f;
    [SerializeField]
    private float JumpHeight = 15f;
    [SerializeField]
    private Transform Groundcheck;
    [SerializeField]
    private LayerMask GroundLayer;
    private bool isGrounded = false;
    [SerializeField]
    private float GroundCheckRadius = 0.3f;
    private bool isFacingRight = true;
    private Vector2 InputAxis;
    private Animator anim;
    private bool isRunning;
    private float LastClickedTime;
    private float LastComboEnd;
    private int ComboCounter;
    private int AttackCounter;
    [SerializeField]
    private List<AnimationSO> ComboOverrides;
    private bool canMove = true;
    private bool canJump = true;
    private bool canFlip = true;
    [SerializeField]
    private float RollSpeed = 5f;
    private int FacingDirection = 1;
    #endregion

    #region Public Variables

    #endregion

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        inputActions = new InputActions();
        anim = GetComponent<Animator>();
        inputActions.Player.Enable();
    }
    // Start is called before the first frame update
    void Start()
    {

    }
    // Update is called once per frame
    private void Update()
    {
        if (!isFacingRight && InputAxis.x > 0)
        {
            Flip();
        }
        else if (isFacingRight && InputAxis.x < 0)
        {
            Flip();
        }
        if (rb.linearVelocity.x != 0)
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
        //else
        //{
        //    anim.SetBool("Attack", false);
        //}
        ExitAttack();
        if (inputActions.Player.Roll.IsPressed() && isGrounded)
        {
            DodgeRoll();
        }
        else
        {
            anim.SetBool("Roll", false);
        }
    }
    private void FixedUpdate()
    {
        Jump();
        Move();
    }
    private void Move()
    {
        if (canMove)
        {
            InputAxis = inputActions.Player.Mover.ReadValue<Vector2>();
            rb.linearVelocity = new Vector2(InputAxis.x * MoveSpeed, rb.linearVelocity.y);
            anim.SetBool("isRunning", isRunning);
        }
    }
    private void Jump()
    {
        if (inputActions.Player.Jump.IsPressed() && isGrounded && canJump)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, JumpHeight);
            anim.SetBool("Jump", true);
            anim.SetFloat("yVel", rb.linearVelocity.y);
        }
        else
        {
            anim.SetFloat("yVel", rb.linearVelocity.y);
            anim.SetBool("Jump", false);
        }
    }
    private void GroundChecker()
    {
        isGrounded = Physics2D.OverlapCircle(Groundcheck.position, GroundCheckRadius, GroundLayer);
    }
    private void Flip()
    {
        if (canFlip)
        {
            isFacingRight = !isFacingRight;
            Vector3 LocalScale = transform.localScale;
            LocalScale.x *= -1f;
            transform.localScale = LocalScale;
        }
    }
    private void Attack()
    {
        //foreach(var combo in ComboOverrides)
        //{
        //    anim.runtimeAnimatorController = combo.ComboController;
        //    anim.SetBool("Attack", true);
        //    Debug.Log("Playing");
        //}
        if(Time.time - LastClickedTime > 0.2f && ComboCounter <= ComboOverrides.Count)
        {
            AnimatorStateInfo currentState = anim.GetCurrentAnimatorStateInfo(0);
            if (currentState.IsName("Attack1") && currentState.normalizedTime < 1.0f)
            {
                // Animation is currently playing, do nothing
                return;
            }
            CancelInvoke("EndCombo");
            if (Time.time - LastClickedTime >= 0.1f)
            {
                anim.runtimeAnimatorController = ComboOverrides[ComboCounter].ComboController;
                anim.Play("Attack1", 0, 0);
                ComboCounter++;
                LastClickedTime = Time.time;
                if (ComboCounter+1 > ComboOverrides.Count)
                {
                    ComboCounter = 0;
                }
            }
        }
    }
    private void DodgeRoll()
    {
        anim.SetBool("Roll", true);
        rb.linearVelocity = new Vector2(RollSpeed * transform.localScale.x, rb.linearVelocity.y);
    }
    private void ExitAttack()
    {
        if (anim.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.9f && anim.GetCurrentAnimatorStateInfo(0).IsTag("Attack"))
        {
            Invoke("EndCombo", 1f);
        }
    }
    private void EndCombo()
    {
        ComboCounter = 0;
        LastClickedTime = Time.time;
    }
    #region Anim Event trigger methods
    public void AnimMoverEvent()
    {
        canMove = true;
    }
    public void StopMoverEvent()
    {
        canMove = false;
    }
    public void AnimJumpEvent()
    {
        canJump = true;
    }
    public void StopJumpEvent()
    {
        canJump = false;
    }
    public void AnimFlipEvent()
    {
        canFlip = true;
    }
    public void StopFlipEvent()
    {
        canFlip = false;
    }
    #endregion
}
