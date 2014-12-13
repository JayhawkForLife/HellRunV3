using UnityEngine;
using System.Collections;

public class Ammo : MonoBehaviour {

	GameObject player;
	bool playerOnAmmo = false;
	
	// Use this for initialization
	void Start () {
		
		player = GameObject.FindGameObjectWithTag("Player");
		
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.E) && playerOnAmmo)
		{
			if (player.gameObject.GetComponentInChildren<TommyGun>().getCurrentAmmo() < 30)
			{
				player.gameObject.GetComponentInChildren<TommyGun>().addAmmo(5);
				Destroy(this.gameObject);
			}
			else
			{
				Debug.Log ("Cannot carry anymore Ammo!");
			}
		}
	}
	
	
	void OnTriggerEnter2D(Collider2D coll)
	{
		if(coll.gameObject.tag == "Player")
		{
			Debug.Log("Player touching ammo");
			playerOnAmmo = true;
			
		}
	}

	void OnTriggerExit2D(Collider2D coll)
	{
		if(coll.gameObject.tag == "Player")
		{
			playerOnAmmo = false;
		}
	}
}