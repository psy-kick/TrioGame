using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ITriggerCheckable
{
    bool isTriggered { get; set; }
    bool isInRange { get; set; }
    void SetAgroStatus(bool IsTriggered);
    void SetStrikingDistance(bool IsInRange);
}
