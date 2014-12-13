using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Chest : MonoBehaviour {

	Animator anim;
	GameObject player;
	int numberOfItems = 3;
	int item;

    public AudioClip openSound;

	public GameObject health;
	public GameObject ammo2;
	public GameObject bomb;
	
	bool playerOnChest = false; // Checks if player is touching the chest
	public bool openChest = false; // Used to activate open animation
	bool alreadyOpened = false; // Make sure chests can only be opened once

	// Use this for initialization
	void Start () {

		anim = GetComponent<Animator> ();
		player = GameObject.FindGameObjectWithTag("Player");
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.E) && playerOnChest && !alreadyOpened) 
		{
            if (openSound != null)
            {
                AudioSource.PlayClipAtPoint(openSound, transform.position);
            }
			openChest = true;
			anim.SetBool("openChest", openChest);

			alreadyOpened = true;
			Debug.Log("Player opened chest");

			item = Random.Range (0, numberOfItems);

			if (item == 0)
			{
				// Health pack
				GameObject health1 = Instantiate(health, transform.position, Quaternion.identity) as GameObject;

			}
			else if(item == 1)
			{
				// Ammo
				GameObject ammo2a = Instantiate(ammo2, transform.position, Quaternion.identity) as GameObject;

			}
			else if(item == 2)
			{
				// Bomb
				GameObject bomb1 = Instantiate(bomb, transform.position, Quaternion.identity) as GameObject;
			}
		}
	}

	void OnTriggerEnter2D(Collider2D coll)
	{
		if(coll.gameObject.tag == "Player")
		{
			playerOnChest = true;
		}
	}

	void OnTriggerExit2D(Collider2D coll)
	{
		if(coll.gameObject.tag == "Player")
		{
			playerOnChest = false;
		}
	}

}
