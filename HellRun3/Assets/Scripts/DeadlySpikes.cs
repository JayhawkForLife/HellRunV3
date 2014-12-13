using UnityEngine;
using System.Collections;

public class DeadlySpikes : MonoBehaviour {
    GameObject player;
    PlayerHealth playerHealth;

    public int damage = 3;

    public bool inSpikes;
	// Use this for initialization
	void Start () {
        // Get the player
        player = GameObject.FindGameObjectWithTag("Player");
        // Get the player health script of the player
        playerHealth = player.GetComponent<PlayerHealth>();

        // set it initially to false since the player is not inspikes at the begining of the game
        inSpikes = false;
	}
	
	// Update is called once per frame
	void Update () {
        if(inSpikes)
        {
            playerHealth.SendMessage("TakeDamage", damage, SendMessageOptions.DontRequireReceiver);
        }
	
	}


    // checks to see if the player is in spikes
    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            inSpikes = true;
        }
    }
}
