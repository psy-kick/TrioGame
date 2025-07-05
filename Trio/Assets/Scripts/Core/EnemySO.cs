using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "Enemies", menuName = "EnemyType")]
public class EnemySO : ScriptableObject
{
    public string EnemyName;
    public int Health;
}
