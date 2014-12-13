using UnityEngine;
using System.Collections;

public class CharacterController2D : MonoBehaviour 
{
    private const float SkinWidth = .1f;
    private const int TotalHorizontalRays = 8;
    private const int TotalVerticalRays = 4;

    private static readonly float SlopLimitTangent = Mathf.Tan(75f * Mathf.Deg2Rad);

    public LayerMask platformMask;
    public ControllerParameters2D defaultParameters;

    public CharacterState playerState { get; private set; }
    public Vector2 velocity { get { return _velocity; } }
    
    public bool HandleCollisions { get; set; }
    public ControllerParameters2D Parameters { get { return _overideParameters ?? defaultParameters; } }
    public GameObject StandingOn { get; private set; }

    Player player;
    public bool CanJump
    {
        get
        {
            if (Parameters.jumpRestrictions == ControllerParameters2D.JumpRestrictions.JumpAnywhere)
                return jumpAllowed < 0;
            if (Parameters.jumpRestrictions == ControllerParameters2D.JumpRestrictions.OnGround)
                return playerState.isGrounded;
            return false;
        }
    }
    public Vector3 PlatformVelocity { get; private set; }

    public Vector2 _velocity;
    private Transform _transform;
    private Vector2 _localScale;
    private BoxCollider2D _boxCollider;
    private ControllerParameters2D _overideParameters;
    private Vector3 rayTopLeft;
    private Vector3 rayBotRight;
    private Vector3 rayBotLeft;
    private Vector3 activeLocalPlatformPoint;
    private Vector3 activeGlobalPlatformPoint;
    private float jumpAllowed;

    private float _verticalDistanceBetweenRays;
    private float _horizontalDistanceBetweenRays;

    public bool isClimbing { get; set; }

    public void Awake()
    {
        isClimbing = false;
        player = GetComponent<Player>();

        HandleCollisions = true;
        playerState = new CharacterState();
        _transform = transform;
        _localScale = transform.localScale;
        _boxCollider = GetComponent<BoxCollider2D>();

        var colliderWidth = _boxCollider.size.x * Mathf.Abs(transform.localScale.x) - (2 * SkinWidth);
        _horizontalDistanceBetweenRays = colliderWidth / (TotalVerticalRays - 1);

        var colliderHeight = _boxCollider.size.y * Mathf.Abs(transform.localScale.y) - (2 * SkinWidth);
        _verticalDistanceBetweenRays = colliderHeight / (TotalHorizontalRays - 1);
    }
    public void AddForce(Vector2 force)
    {
        _velocity += force;
    }
    public void SetForce(Vector2 force)
    {
        _velocity = force;
    }
    public void SetHorizontalForce(float x)
    {
        _velocity.x = x;
    }
    public void SetVerticalForce(float y)
    {
        _velocity.y = y;
    }
    public void Jump()
    {
        AddForce(new Vector2(0, Parameters.JumpMagnitude));
        jumpAllowed = Parameters.jumpFrequency;

    }

    public void update()
    {
        
    }
    
    public void LateUpdate()
    {
        
        if (isClimbing)
            Parameters.setGravity(0);
        else
            Parameters.setGravity(-25);
        
        
        _velocity.y += Parameters.gravity * Time.deltaTime;
        Move(velocity * Time.deltaTime);
        jumpAllowed = Parameters.jumpFrequency;

    }
    private void Move(Vector2 movement)
    {
        var wasGround = playerState.isCollidingBelow;
        playerState.reset();

        if(HandleCollisions)
        {
            HandlePlatforms();
            CalculateRaysOrigins();

            if (movement.y < 0 && wasGround)
                HandleVerticalSlope(ref movement);

            if (Mathf.Abs(movement.x) > .00f)
                HorizontalMovement(ref movement);

            VerticalMovement(ref movement);
        }

        _transform.Translate(movement, Space.World);

        if (Time.deltaTime > 0)
            _velocity = movement / Time.deltaTime;

        _velocity.x = Mathf.Min(_velocity.x, Parameters.characterVelocity.x);
        _velocity.y = Mathf.Min(_velocity.y, Parameters.characterVelocity.y);

        if (playerState.isMovingUpHill)
            _velocity.y = 0;

        
        // Calculate the velocity of an object(for moving platforms)
        if(StandingOn != null)
        {
            activeGlobalPlatformPoint = transform.position;
            activeLocalPlatformPoint = StandingOn.transform.InverseTransformPoint(transform.position);
        }
    }
    private void HandlePlatforms()
    {
        if(StandingOn != null)
        {
            var platformPoint = StandingOn.transform.TransformPoint(activeLocalPlatformPoint);
            var moveDistance = platformPoint - activeGlobalPlatformPoint;

            if(moveDistance != Vector3.zero)
                transform.Translate(moveDistance, Space.World);

            PlatformVelocity = (platformPoint - activeGlobalPlatformPoint) / Time.deltaTime;

            Debug.DrawLine(transform.position, activeGlobalPlatformPoint);
            Debug.DrawLine(transform.position, activeLocalPlatformPoint);
        }
    }
    private void CalculateRaysOrigins()
    {
        var size = new Vector2(_boxCollider.size.x * Mathf.Abs(_localScale.x), _boxCollider.size.y * Mathf.Abs(_localScale.y)) / 2;
        var center = new Vector2(_boxCollider.center.x * _localScale.x, _boxCollider.center.y * _localScale.y);

        rayTopLeft = _transform.position + new Vector3(center.x - size.x + SkinWidth, center.y - SkinWidth );
        rayBotRight = _transform.position + new Vector3(center.x + size.x - SkinWidth, center.y - size.y + SkinWidth);
        rayBotLeft = _transform.position + new Vector3(center.x - size.x + SkinWidth, center.y - size.y + SkinWidth);
    }
    private void HorizontalMovement(ref Vector2 movement)
    {
        // is the character moving in the right direction
        var movingRight = movement.x > 0;
        var rayDistance = Mathf.Abs(movement.x) + SkinWidth;
        var rayDirection = movingRight ? Vector2.right : -Vector2.right;
        var rayOrigin = movingRight ? rayBotRight : rayBotLeft;

        for(var i = 0; i < TotalHorizontalRays; i++)
        {

            var rayVector = new Vector2(rayOrigin.x, rayOrigin.y + (i * _verticalDistanceBetweenRays));
            Debug.DrawRay(rayVector, rayDirection * rayDistance, Color.black);

            var rayCastHit = Physics2D.Raycast(rayVector, rayDirection, rayDistance, platformMask);
            if (!rayCastHit)
                continue;

            if (i == 0 && HandleHorizontalSlope(ref movement, Vector2.Angle(rayCastHit.normal, Vector2.up), movingRight))
                break;

            movement.x = rayCastHit.point.x - rayVector.x;
            rayDistance = Mathf.Abs(movement.x);

            if(movingRight)
            {
                movement.x -= SkinWidth;
                playerState.isCollidingRight = true;
            }
            else
            {
                movement.x += SkinWidth;
                playerState.isCollidingLeft = true;
            }

            if (rayDistance < SkinWidth + .0001f)
                break;
        }
    }
    private void VerticalMovement(ref Vector2 movement)
    {
        var isMovingUp = movement.y > 0;
        var rayDistance = Mathf.Abs(movement.y) + SkinWidth;
        var rayDirection = isMovingUp ? Vector2.up : -Vector2.up;
        var rayOrigin = isMovingUp ? rayTopLeft : rayBotLeft;

        rayOrigin.x += movement.x;
        var standingOnDistance = float.MaxValue;
        for(var i = 0; i < TotalVerticalRays; i++)
        {
            var rayVector = new Vector2(rayOrigin.x + (i * _horizontalDistanceBetweenRays), rayOrigin.y);
            Debug.DrawRay(rayVector, rayDirection * rayDistance, Color.black);

            var rayCastHit = Physics2D.Raycast(rayVector, rayDirection, rayDistance, platformMask);
            if (!rayCastHit)
                continue;

            if(!isMovingUp)
            {
                var verticalDistanceToHit = transform.position.y - rayCastHit.point.y;
                if(verticalDistanceToHit < standingOnDistance)
                {
                    standingOnDistance = verticalDistanceToHit;
                    StandingOn = rayCastHit.collider.gameObject;
                }

                movement.y = rayCastHit.point.y - rayVector.y;
                rayDistance = Mathf.Abs(movement.y);

                if(isMovingUp)
                {
                    movement.y -= SkinWidth;
                    playerState.isCollidingAbove = true;
                }
                else
                {
                    movement.y += SkinWidth;
                    playerState.isCollidingBelow = true;
                }

                if (!isMovingUp && movement.y > .001f)
                    playerState.isMovingUpHill = true;

                if (rayDistance < SkinWidth + .0001f)
                    break;
            }
        }
    }
    private void HandleVerticalSlope(ref Vector2 movement)
    {

    }
    private bool HandleHorizontalSlope(ref Vector2 movement, float angle, bool isMovingRight)
    {
        return false;
    }

    public void OnTriggerEnter2D(Collider2D other)
    {

    }
    public void OnTriggerExit2D(Collider2D other)
    {

    }

	
}
