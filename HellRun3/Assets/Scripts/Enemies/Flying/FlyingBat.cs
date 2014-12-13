using UnityEngine;
using System.Collections;

public class FlyingBat : MonoBehaviour {

    public float speed;
    public float timer = 0;
    public int howlong = 50;
    public float chaseDistance = 100f;
    public float attackDistance = 50;
    public float attackTimer = 0;
    public int damage;

    public bool canSeePlayer { get; set; }
    public bool canAttack { get; set; }
    public bool canMultiplySpeed { get; set; }
    public bool isTouchingPlayer { get; set; }
    public bool isDead { get; set; }
    public bool isSquished { get; set; }
    public bool isStomped { get; set; } 

    public Vector2 direction;

    GameObject player;
    Transform playerTransform;
    Sight sight;
    Animator anim;
    // Use this for initialization
    void Start()
    {
        
        player = GameObject.FindGameObjectWithTag("Player");
        playerTransform = player.transform;
        sight = GetComponentInChildren<Sight>();
        anim = GetComponent <Animator>();
        canSeePlayer = false;
        canMultiplySpeed = true;
        isTouchingPlayer = false;
        isSquished = false;
        isStomped = false;

    }

    void Update()
    {
        if (attackTimer <= 0)
        {
            canAttack = true;
        }
        else
        {
            canAttack = false;
            attackTimer -= Time.deltaTime;
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(isDead || isStomped)
            StartCoroutine(Dead());
       

        if (!isDead && !isStomped)
        {
            anim.SetFloat("speed", Mathf.Abs(rigidbody2D.velocity.x));
            // patroll if it can not see the player
            if (!canSeePlayer)
            {
                timer += 1;

                if (timer >= howlong)
                {
                    timer = 0;
                    if (direction.x > 0)
                    {
                        direction.x = -1;
                        return;
                    }
                    if (direction.x < 0)
                    {
                       direction.x = 1;
                        return;
                    }
                }

                // pace right
                if (direction.x > 0)
                {
                    if (rigidbody2D.velocity.x < 0)
                    {
                        Debug.Log("Flip!");
                        FlipDirection();

                    }
                    rigidbody2D.velocity = new Vector2(speed, 0);


                }

                // pace left
                if (direction.x < 0)
                {
                    if (rigidbody2D.velocity.x > 0)
                    {
                        FlipDirection();

                    }
                    rigidbody2D.velocity = new Vector2(-speed, 0);



                }
            }
            if (Vector2.Distance(transform.position, playerTransform.position) < chaseDistance)
            {
                if(!canMultiplySpeed)
                {

                    speed /= 2;
                    canMultiplySpeed = true;
                }

                canSeePlayer = true;
                rigidbody2D.velocity = new Vector2(sight.playerPos.direction.x * speed, 0);
                if (sight.playerPos.direction.magnitude < 1)
                    sight.playerPos.direction = sight.playerPos.direction.normalized;

                // Flip the AI in the right dirrection base on which way the AI is running
                //rotate to look at the player
                if (direction.x < 0)
                {
                    if (rigidbody2D.velocity.x > 0)
                    {
                        FlipDirection();
                        direction.x = 1;
                    }

                }
                else if (direction.x > 0)
                {
                    if (direction.x > 0 && rigidbody2D.velocity.x < 0)
                    {
                        FlipDirection();
                        direction.x = -1;
                    }

                }
            }
            else
                canSeePlayer = false;

            if (Vector2.Distance(transform.position, playerTransform.position) < attackDistance)
            {
                if(canMultiplySpeed)
                {
                    speed *= 2;
                    anim.SetBool("canAttack", true);
                    canMultiplySpeed = false;
                }
                
                if (canAttack && isTouchingPlayer)
                {

                    Debug.Log("im attacking u");
                    
                    player.GetComponent<PlayerHealth>().SendMessage("TakeDamage", damage, SendMessageOptions.DontRequireReceiver);
                    attackTimer += 1f;


                }

            }
            else if (Vector2.Distance(transform.position, playerTransform.position) > attackDistance)
            {
                anim.SetBool("canAttack", false);
                canAttack = false;


            }
        }








    }


    // Helper method to flip the character when needed
    private void FlipDirection()
    {
         transform.localScale = new Vector2(-transform.localScale.x, transform.localScale.y);

    }

    private IEnumerator Dead()
    {
        if (isStomped)
        {
            Debug.Log("Stomped!");
            anim.SetFloat("speed", 0);
            anim.SetBool("isSquished", true);
            
            
            if(!isSquished)
            {
                transform.localScale = new Vector2(transform.localScale.x, transform.localScale.y * .2f);
                isSquished = true;
            }
            
            rigidbody2D.gravityScale = 5;
            rigidbody2D.velocity = new Vector2(0, 0);
            yield return new WaitForSeconds(2f);
            Destroy(gameObject);
        }
        else if(isDead)
        {
            anim.SetBool("canAttack", false);
            anim.SetBool("isDead", true);

            rigidbody2D.gravityScale = 5;
            rigidbody2D.velocity = new Vector2(0, 0);
            yield return new WaitForSeconds(2f);
            Destroy(gameObject);
        }
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.tag == "Player")
        {
            isTouchingPlayer = true;
        }
    }

    void OnCollisionExit2D(Collision2D other)
    {
        if(other.gameObject.tag == "Player")
        {
            isTouchingPlayer = false;
        }
    }
}
