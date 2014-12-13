using UnityEngine;
using System.Collections;

public class Ladder : MonoBehaviour {

    GameObject player;
    CharacterController2D characterController;
	// Use this for initialization
	void Start () {
        player = GameObject.FindGameObjectWithTag("Player");

        characterController = player.GetComponent<CharacterController2D>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            Debug.Log("isClimbing!");
            characterController.isClimbing = true;
            
        }
    }


    void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            characterController.isClimbing = false;
            
        }
    }
}
