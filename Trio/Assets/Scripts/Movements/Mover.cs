using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using Unity.VisualScripting;
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
    private int AttackCounter;
    public List<AttackAnimCombo> Actions;
    private Animator AttackAnim;
    private Task AttackTasks;
    private CancellationTokenSource token;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        inputActions = new InputActions();
        anim = GetComponent<Animator>();
        inputActions.Player.Enable();
        AttackAnim = GetComponent<Animator>();
    }
    // Start is called before the first frame update
    void Start()
    {
        token = new CancellationTokenSource();
    }
    // Update is called once per frame
    async void Update()
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
            Combo();
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
    private async Task Attack(AttackAnimCombo AttackCombo,CancellationToken token)
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
        if(token.IsCancellationRequested)
        {
            return;
        }
        anim.Play("Attack");
        await Task.Delay(5000);
    }
    private async Task Combo()
    {
        Task AttackTask1 = Attack(Actions[0], token.Token);
        Task AttackTask2 = Attack(Actions[1], token.Token);
        Task AttackTask3 = Attack(Actions[2], token.Token);
        //for (int i = 0; i < Actions.Count; i++)
        //{
        //    AttackTasks = Attack(Actions[i]);
        //}
        if (token.IsCancellationRequested)
        {
            return;
        }
        if (token.IsCancellationRequested)
        {
            Debug.Log("Task was cancelled");
        }
        if (Time.time - LastClickedTime >= 0.1f)
        {
            await AttackTask1;
            await AttackTask2;
            await AttackTask3;
            LastClickedTime = Time.time;
        }
        else
        {
            token.Cancel();
        }
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
