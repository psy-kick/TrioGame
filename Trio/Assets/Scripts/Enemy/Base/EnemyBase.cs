using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBase : MonoBehaviour, IMoveable, ITriggerCheckable
{
    #region Private Variables
    [SerializeField]
    private GameObject PatrolA;
    [SerializeField]
    private GameObject PatrolB;
    private Transform CurrentPoint;
    [SerializeField]
    private float speed;
    private Animator anim;
    [SerializeField]
    private List<GameObject> PatrolPoints;
    #endregion

    #region Public Variables
    public bool isPatrolling;
    public EnemyStateMachine enemyStateMachine {  get; set; }
    public EnemyAttack enemyAttack { get; set; }
    public bool isFacingRight { get; set; } = false;
    public Rigidbody2D rb { get; set; }
    public bool isTriggered { get; set; }
    public bool isInRange { get; set; }
    #endregion

    public enum AnimTriggers
    {
        EnemyDamaged,
        PlayFootSteps
    }
    private void Awake()
    {
        enemyStateMachine = new EnemyStateMachine();
        enemyAttack = new EnemyAttack(this, enemyStateMachine);
    }
    private void AnimationTriggerEvents(AnimTriggers animTriggers)
    {
        //will be done
    }
    // Start is called before the first frame update
    void Start()
    {
        enemyStateMachine.Initialized(enemyAttack);
        rb = GetComponent<Rigidbody2D>();
        CurrentPoint = PatrolA.transform;
        anim = GetComponent<Animator>();
        if (PatrolPoints.Count != 0)
        {
            isPatrolling = true;
        }
        else
        {
            isPatrolling = false;
        }
        Debug.Log(isTriggered);
    }
    private void UpdateAnims()
    {
        anim.SetBool("move", true); 
    }
    // Update is called once per frame
    void Update()
    {
        Patrolling();
        enemyStateMachine.CurreState.FrameUpdate();
    }
    private void FixedUpdate()
    {
        enemyStateMachine.CurreState.PhysicsUpdate();
    }
    private void Patrolling()
    {
        if (isPatrolling)
        {
            Vector2 point = CurrentPoint.position - transform.position;
            if (CurrentPoint == PatrolB.transform)
            {
                anim.SetBool("move", true);
                Move(new Vector2(speed,0));
            }
            else if (CurrentPoint == PatrolA.transform)
            {
                anim.SetBool("move", true);
                Move(new Vector2(-speed, 0));
            }
            if (Vector2.Distance(transform.position, CurrentPoint.position) < 1f && CurrentPoint == PatrolB.transform)
            {
                CurrentPoint = PatrolA.transform;
            }
            if (Vector2.Distance(transform.position, CurrentPoint.position) < 1f && CurrentPoint == PatrolA.transform)
            {
                CurrentPoint = PatrolB.transform;
            }
        }
    }
    private void Flip()
    {
        Vector3 LocalScale = transform.localScale;
        LocalScale.x *= -1f;
        transform.localScale = LocalScale;
    }

    public void Move(Vector2 velocity)
    {
        CheckFacing(velocity);
        rb.velocity = velocity;
    }

    public void CheckFacing(Vector2 velocity)
    {
        if (!isFacingRight && velocity.x < 0)
        {
            Vector2 rotator = new Vector2(transform.rotation.x, 0f);
            transform.rotation = Quaternion.Euler(rotator);
            isFacingRight = true;
        }
        else if (isFacingRight && velocity.x > 0f)
        {
            Vector2 rotator = new Vector2(transform.rotation.x, 180f);
            transform.rotation = Quaternion.Euler(rotator);
            isFacingRight = false;
        }
    }

    public void SetAgroStatus(bool issTriggered)
    {
        isTriggered = issTriggered;
    }

    public void SetStrikingDistance(bool issInRange)
    {
        isInRange = issInRange;
        Debug.Log("this happens");
    }
}
