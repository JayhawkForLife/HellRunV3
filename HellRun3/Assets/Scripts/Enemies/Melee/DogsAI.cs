using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody2D))]
public class DogsAI : MonoBehaviour {

    GameObject player;
    Transform playerTransform;
    Sight sight;
    Animator anim;

    public float speed;
    public float chaseDistance = 10f;
    public float attackDistance = 1f;
    public float attackTimer;

    public int damage;

    
    
    public bool canAttack { get; private set; }
    public bool isStomped { get; set; }
    public bool isSquished { get; set; }
    public bool isDead { get; set; }
    public Vector2 direction;
	// Use this for initialization
	void Start () {
        player = GameObject.FindGameObjectWithTag("Player");
        playerTransform = player.transform;
        sight = GetComponentInChildren<Sight>();
        anim = GetComponent<Animator>();
        
        isDead = false;
        isStomped = false;
        isSquished = false;
        
        

        
	}
	
	// Update is called once per frame
	void Update () 
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

    void FixedUpdate()
    {
        if(isDead || isStomped)
            StartCoroutine(Dead());
        // Cache the horizontal input.
        
        // if the dog can see the player, run towards the player
       

        if(!isDead && !isSquished)
        {
            
            float h = Input.GetAxis("Horizontal");
            anim.SetFloat("speed", Mathf.Abs(rigidbody2D.velocity.x));
            
            if (Vector2.Distance(transform.position, playerTransform.position) < chaseDistance)
            {
                Debug.Log("Can chase");
                speed = 8f;
                
                rigidbody2D.velocity = new Vector2(sight.playerPos.direction.x * speed, 0);
                if (sight.playerPos.direction.magnitude < 1)
                    sight.playerPos.direction = sight.playerPos.direction.normalized;

                

                // Flip the AI in the right dirrection base on which way the AI is running
                //rotate to look at the player
                if(direction.x < 0)
                {
                    if (rigidbody2D.velocity.x > 0)
                    {
                        FlipDirection();
                        direction.x = 1;
                    }
                    
                }
                else if(direction.x > 0)
                {
                    if(direction.x > 0 && rigidbody2D.velocity.x < 0)
                    {
                        FlipDirection();
                        direction.x = -1;
                    }
                   
                }
            }
            
            if(Vector2.Distance(transform.position, playerTransform.position) < attackDistance)
            {
                if(canAttack)
                {
                    speed = 0;
                    anim.SetBool("canAttack", true);
                    player.GetComponent<PlayerHealth>().SendMessage("TakeDamage", damage, SendMessageOptions.DontRequireReceiver);
                    attackTimer += 1f;
                    
                }
                
            }
            else if (Vector2.Distance(transform.position, playerTransform.position) > attackDistance)
            {
                anim.SetBool("canAttack", false);
                

            }

        }
        else
        {
            canAttack = false;
            attackTimer -= Time.deltaTime;
        }
	}



    private void FlipDirection()
    {
        transform.localScale = new Vector2(-transform.localScale.x, transform.localScale.y);
        
    }

   
    

    private IEnumerator Dead()
    {
        if(isDead)
        {
            anim.SetBool("isDead", true);
            rigidbody2D.velocity = new Vector2(0, 0);
            yield return new WaitForSeconds(2f);
            Destroy(gameObject);
            
        }
        else if(isStomped)
        {
            
            anim.SetBool("canAttack", false);
            if (!isSquished)
            {
                transform.localScale = new Vector2(transform.localScale.x, transform.localScale.y * .2f);
                anim.SetBool("isSquished", true);
                isSquished = true;
            }

            rigidbody2D.gravityScale = 5;
            rigidbody2D.velocity = new Vector2(0, 0);
            yield return new WaitForSeconds(2f);
            Destroy(gameObject);
            
        }
    }

    


}
