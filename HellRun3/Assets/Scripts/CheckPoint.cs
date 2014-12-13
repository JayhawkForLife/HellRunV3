using UnityEngine;
using System.Collections;


public class CheckPoint : MonoBehaviour
{
    GameObject gameManager;





    Animator anim;
    GameObject player;

    GameObject pHealth;
    public PlayerHealth pH;

    public bool startCP = false;
    bool playerOnCP = false;
    bool alreadyTouched = false;



    void Start()
    {

        anim = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player");
        //gameManager = GameObject.FindGameObjectWithTag("GameManager");

    }

    void Update()
    {
        if (playerOnCP && !alreadyTouched)
        {
            alreadyTouched = true;
            startCP = true;
            anim.SetBool("startCP", startCP);

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
}
