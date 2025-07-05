using UnityEngine;

public class EnemyTriggerCheck : MonoBehaviour
{
    public GameObject PlayerTarget {  get; set; }
    public EnemyBase enemy { get; set; }
    public EnemyTriggerCheck(GameObject playerTarget, EnemyBase enemy)
    {
        this.PlayerTarget = playerTarget;
        this.enemy = enemy;
    }
}
