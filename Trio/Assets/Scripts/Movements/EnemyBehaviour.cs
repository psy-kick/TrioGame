using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour
{
    [SerializeField]
    private GameObject PatrolA;
    [SerializeField]
    private GameObject PatrolB;
    private Transform CurrentPoint;
    [SerializeField]
    private float speed;
    private Rigidbody2D rb;
    private Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        CurrentPoint = PatrolB.transform;
        Flip();
        anim = GetComponent<Animator>();
    }
    private void UpdateAnims()
    {
        anim.SetBool("move", true); 
    }
    // Update is called once per frame
    void Update()
    {
        Vector2 point = CurrentPoint.position - transform.position;
        if (CurrentPoint == PatrolB.transform)
        {
            anim.SetBool("move", true);
            rb.velocity = new Vector2(speed, 0);
        }
        else if (CurrentPoint == PatrolA.transform)
        {
            anim.SetBool("move", true);
            rb.velocity = new Vector2(-speed, 0);
        }
        if (Vector2.Distance(transform.position, CurrentPoint.position) < 1f && CurrentPoint == PatrolB.transform)
        {
            Flip();
            CurrentPoint = PatrolA.transform;
        }
        if (Vector2.Distance(transform.position, CurrentPoint.position) < 1f && CurrentPoint == PatrolA.transform)
        {
            Flip();
            CurrentPoint = PatrolB.transform;
        }
    }
    private void Flip()
    {
        Vector3 LocalScale = transform.localScale;
        LocalScale.x *= -1f;
        transform.localScale = LocalScale;
    }
}
