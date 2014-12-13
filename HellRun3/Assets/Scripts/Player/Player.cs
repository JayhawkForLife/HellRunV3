using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

    public bool isFacingRight { get; set; }
   
    private CharacterController2D controller;
    private float normalizeHorizontalSpeed;
    private float normalizeVerticalSpeed;

    public float maxSpeed = 10f;
    public float speedAccelerationOnGround = 10f;
    public float speedAccelerationInAir = 5f;

    public bool isDead { get; private set; }

    public AudioClip footSteps;
    public AudioClip jump;

    AudioSource audioSource;

    Animator anim;
    float movementFactor;

	bool isRunning = false;

    public void Awake()
    {
        controller = GetComponent<CharacterController2D>();
        audioSource = GetComponent<AudioSource>();
        anim = GetComponent<Animator>();
        isFacingRight = transform.localScale.x > 0;
        
    }

    

    public void Update()
    {
        if(!isDead)
            HandleInput();

        movementFactor = controller.playerState.isGrounded ? speedAccelerationOnGround : speedAccelerationInAir;
        controller.SetHorizontalForce(Mathf.Lerp(controller.velocity.x, normalizeHorizontalSpeed * maxSpeed, Time.deltaTime * movementFactor));
        
            
        
        

        
    }

    private void HandleInput()
    {
        anim.SetFloat("speed", Mathf.Abs(Input.GetAxis("Horizontal")));

		

        if(Input.GetAxisRaw("Horizontal") > 0)
        {

            normalizeHorizontalSpeed = 1;
            if (!isFacingRight)
                FlipDirection();
        }
        else if(Input.GetAxisRaw("Horizontal") < 0)
        {

            normalizeHorizontalSpeed = -1;
            if (isFacingRight)
                FlipDirection();
        }
        else
        {
            normalizeHorizontalSpeed = 0;
        }

        if(controller.CanJump && Input.GetKeyDown(KeyCode.Space))
        {
            controller.Jump();
        }
        
        if(controller.isClimbing)
        {
            if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
            {
                transform.Translate(Vector2.up * maxSpeed * Time.deltaTime);
            }

            if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
            {
                transform.Translate(-Vector2.up * maxSpeed * Time.deltaTime);
            }
        }
    }

	



    private void FlipDirection()
    {
        transform.localScale = new Vector2(-transform.localScale.x, transform.localScale.y);
        isFacingRight = transform.localScale.x > 0;
    }

    public void KillPlayer()
    {
        //anim.SetBool("isDead", true);
        collider2D.enabled = false;
        isDead = true;
    }

    public void RespawnPlayer()
    {
        if (!isFacingRight)
            FlipDirection();
        anim.SetBool("isDead", false);
        collider2D.enabled = true;
        transform.position = new Vector2(0,0);
        isDead = false;

    }


}
