using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAgroCheck : MonoBehaviour
{
    public GameObject PlayerTarget { get; set; }
    public EnemyBase enemy { get; set; }
    //public EnemyAgroCheck(GameObject playerTarget, EnemyBase enemy) : base(playerTarget, enemy)
    //{
        
    //}
    private void Awake()
    {
        PlayerTarget = GameObject.FindGameObjectWithTag("Player");
        enemy = GetComponent<EnemyBase>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            enemy.SetAgroStatus(true);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject == PlayerTarget)
        {
            enemy.SetAgroStatus(false);
        }
    }
}