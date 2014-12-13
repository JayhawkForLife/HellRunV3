using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody2D))]
public class DemonRanged : MonoBehaviour {
    public GameObject PoisonBall;
    GameObject player;
    Transform playerTransform;
    Sight sight;
    Animator anim;

    float speed;
    public float chaseDistance = 30f;
    public float attackDistance = 30f;
    float gettingTooClose = 7.5f;
    float attackTimer;

    public int damage;


    public bool isDead { get; set; }
    public bool canAttack { get; private set; }
    public Vector2 direction;
    // Use this for initialization
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerTransform = player.transform;
        sight = GetComponentInChildren<Sight>();
        anim = GetComponent<Animator>();

        isDead = false;




    }

    // Update is called once per frame
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

    void FixedUpdate()
    {

        StartCoroutine(Dead());
        // Cache the horizontal input.
        float h = Input.GetAxis("Horizontal");
        anim.SetFloat("speed", Mathf.Abs(rigidbody2D.velocity.x));
        // if the dog can see the player, run towards the player

        if (!isDead)
        {
            if (Vector2.Distance(transform.position, playerTransform.position) < chaseDistance)
            {
                speed = 7f;

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

            if (Vector2.Distance(transform.position, playerTransform.position) < attackDistance)
            {
                if (canAttack)
                {
                    GameObject temp = Instantiate(PoisonBall, transform.position, Quaternion.identity) as GameObject;
                    Physics2D.IgnoreCollision(temp.collider2D, gameObject.collider2D);
                    temp.rigidbody2D.AddForce(sight.transform.forward * 2000);
                    speed = 0;
                    anim.SetBool("canAttack", true);
                    attackTimer += 1f;

                }

            }
            else if (Vector2.Distance(transform.position, playerTransform.position) > attackDistance)
            {
                anim.SetBool("canAttack", false);
                canAttack = false;
                rigidbody2D.isKinematic = false;

            }
            if (Vector2.Distance(transform.position, playerTransform.position) < gettingTooClose)
            {
                rigidbody2D.isKinematic = false;
                speed = 7;
                rigidbody2D.velocity = new Vector2(-sight.playerPos.direction.x * speed, 0);
                if (sight.playerPos.direction.magnitude < 1)
                    sight.playerPos.direction = sight.playerPos.direction.normalized;
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

    private void FlipPoisonBallDirection()
    {
        PoisonBall.transform.localScale = new Vector2(-PoisonBall.transform.localScale.x, PoisonBall.transform.localScale.y);
    }




    private IEnumerator Dead()
    {
        if (isDead)
        {
            anim.SetBool("isDead", true);
            rigidbody2D.velocity = new Vector2(0, 0);
            yield return new WaitForSeconds(2f);
            Destroy(gameObject);

        }
    }
	
	
}
