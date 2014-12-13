using UnityEngine;
using System.Collections;

public class Soul : MonoBehaviour
{

    GameObject player;
    bool playerOnSoul = false;

    // Use this for initialization
    void Start()
    {

        player = GameObject.FindGameObjectWithTag("Player");

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && playerOnSoul)
        {
            Destroy(this.gameObject);
            player.GetComponentInChildren<PlayerHealth>().hasSoul = true;
            player.GetComponentInChildren<PlayerHealth>().currentHealth = 3;
        }
    }


    void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.gameObject.tag == "Player")
        {
            Debug.Log("Player touching Soul");
            playerOnSoul = true;

        }
        if (coll.gameObject.tag == "Ground")
        {
            rigidbody2D.isKinematic = true;
        }
    }

    void OnTriggerExit2D(Collider2D coll)
    {
        if (coll.gameObject.tag == "Player")
        {
            playerOnSoul = false;
        }

    }
}
