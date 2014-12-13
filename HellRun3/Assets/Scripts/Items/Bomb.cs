using UnityEngine;
using System.Collections;

public class Bomb : MonoBehaviour {

	GameObject player;
	Animator anim;

	public float blastRadius = 1f;


	// Use this for initialization
	void Start () {

		player = GameObject.FindGameObjectWithTag("Player");
		anim = GetComponent<Animator> ();
	}
	
	// Update is called once per frame
	void Update () {

		if (anim.GetCurrentAnimatorStateInfo (0).IsName ("End")) 
		{
			Collider2D[] colliders = Physics2D.OverlapCircleAll (transform.position,blastRadius);
			foreach(Collider2D col in colliders){
				if (col.tag == "Player")
				{
					player.gameObject.GetComponent<PlayerHealth>().TakeDamage(3);	
				}
			}
			Destroy(gameObject);

		}
	}

}