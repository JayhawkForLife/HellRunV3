using UnityEngine;
using System.Collections;

public class BuzzSaw : MonoBehaviour {

	int rotationSpeed = 200;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		transform.Rotate (0,0,-rotationSpeed*Time.deltaTime);
	}
	
	void OnTriggerEnter2D(Collider2D other)
	{
		Debug.Log ("Colliding with an object");
		if(other.gameObject.tag == "Player")
		{
			Debug.Log("Player touching buzzsaw");
			other.gameObject.GetComponent<PlayerHealth>().TakeDamage(1);
		}
		else if (other.gameObject.tag == "DogAI") 
		{
			Debug.Log ("Enemy touching buzzsaw");
			other.gameObject.GetComponent<EnemyHealth>().DecrementHealth();
			int currentHealth = other.gameObject.GetComponent<EnemyHealth>().GetHealth();
			if(currentHealth == 0)
			{
				other.gameObject.GetComponent<DogsAI>().isDead = true;
			}
		}
        else if (other.gameObject.tag == "PacerAI")
        {
            Debug.Log("Enemy touching buzzsaw");
            other.gameObject.GetComponent<EnemyHealth>().DecrementHealth();
            int currentHealth = other.gameObject.GetComponent<EnemyHealth>().GetHealth();
            if (currentHealth == 0)
            {
                other.gameObject.GetComponent<PacerAI>().isDead = true;
            }
        }
	}
	
}
