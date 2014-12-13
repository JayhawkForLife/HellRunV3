using UnityEngine;
using System.Collections;


public class CheckPoint : MonoBehaviour
{
    GameObject gameManager;





    Animator anim;
    GameObject player;

    public PlayerHealth pH;

    bool playerOnCP = false;
    public bool alreadyTouched = false;



    void Start()
    {

        anim = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player");
        //gameManager = GameObject.FindGameObjectWithTag("GameManager");

    }

    void Update()
    {
		if (anim.GetCurrentAnimatorStateInfo (0).IsName ("Checkpoint_Idle")) 
		{
			anim.SetBool("resetCP", false);
		}
		
        if (playerOnCP && !alreadyTouched)
        {
            alreadyTouched = true;
            anim.SetBool("startCP", true);

            pH.setSpawnPoint(this.gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            playerOnCP = true;

        }
    }

	void OnTriggerExit2D(Collider2D other)
	{
		if (other.tag == "Player")
		{
			playerOnCP = false;
			
		}
	}
}
