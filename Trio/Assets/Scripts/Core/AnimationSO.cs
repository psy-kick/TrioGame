using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "Combos", menuName ="ComboList")]
public class AnimationSO : ScriptableObject
{
    public AnimatorOverrideController ComboController;
    public int Damage;
}
