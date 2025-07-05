using UnityEngine;

public interface IMoveable
{
    Rigidbody2D rb { get; set; }
    bool isFacingRight { get; set; }
    void Move(Vector2 velocity);
    void CheckFacing(Vector2 velocity);
    void Jump(bool JumpRequested);
}
