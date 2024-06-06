using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "Attack/Normal Attack")]
public class AttackAnimCombo : ScriptableObject
{
    public AnimatorOverrideController animatorOverrideController;
    public float Damage;
}
