using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IMovement
{
    void FlipSprite(float direction);
    bool Grounded();
    void IsGroundedCheck(bool isGrounded, RaycastHit2D hit2D);
    void Move();
}
