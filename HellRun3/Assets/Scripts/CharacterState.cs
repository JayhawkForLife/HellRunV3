using UnityEngine;
using System.Collections;

public class CharacterState
{
    public bool isCollidingRight { get; set; }
    public bool isCollidingLeft { get; set; }
    public bool isCollidingAbove { get; set; }
    public bool isCollidingBelow { get; set; }
    public bool isMovingUpHill { get; set; }
    public bool isMovingDownHill { get; set; }
    public bool isGrounded { get { return isCollidingBelow; } }
    public float hillAngle { get; set; }

    public bool collided { get { return isCollidingAbove || isCollidingBelow || isCollidingLeft || isCollidingRight; } }

    public void reset()
    {
        isMovingUpHill =
            isMovingDownHill =
            isCollidingAbove =
            isCollidingBelow =
            isCollidingLeft =
            isCollidingRight = false;
        
        hillAngle = 0;

    }

    public override string ToString()
    {
        return string.Format("(Player: r:{0} l:{1} a:{2} b:{3} upHill:{4} downHill:{5} angle:{6})",
            isCollidingRight,
            isCollidingLeft,
            isCollidingAbove,
            isCollidingBelow,
            isMovingUpHill,
            isMovingDownHill,
            hillAngle);
    }
}
