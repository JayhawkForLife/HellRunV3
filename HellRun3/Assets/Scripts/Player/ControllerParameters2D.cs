using System;
using UnityEngine;
using System.Collections;

[Serializable]
public class ControllerParameters2D
{
    
    public enum JumpRestrictions
    {
        OnGround,
        JumpAnywhere,
        CantJump
    }

    public Vector2 characterVelocity = new Vector2(float.MaxValue, float.MaxValue);

    
    public float gravity = -15;


    public JumpRestrictions jumpRestrictions;

    public float jumpFrequency = .25f;

    public float JumpMagnitude = 20f;

    public void setGravity(int newGravity)
    {
        gravity = newGravity;
    }
	
}
