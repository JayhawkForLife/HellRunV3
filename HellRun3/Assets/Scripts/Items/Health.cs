using UnityEngine;
using System.Collections;

public class Health : MonoBehaviour {

	GameObject player;
	bool playerOnHealth = false;

	// Use this for initialization
	void Start () {

		player = GameObject.FindGameObjectWithTag("Player");

	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.E) && playerOnHealth)
		{
			if (player.gameObject.GetComponent<PlayerHealth>().getCurrentHealth() < 3)
			{
				player.gameObject.GetComponent<PlayerHealth>().Heal(1);
				Destroy(this.gameObject);
			}
			else
			{
				Debug.Log("Health is full");
			}
		}
	}


	void OnTriggerEnter2D(Collider2D coll)
	{
		if(coll.gameObject.tag == "Player")
		{
			playerOnHealth = true;

		}
	}

	void OnTriggerExit2D(Collider2D coll)
	{
		if(coll.gameObject.tag == "Player")
		{
			playerOnHealth = false;
			
		}
	}
}
