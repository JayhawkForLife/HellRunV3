using UnityEngine;
using System.Collections;

public class Stomp : MonoBehaviour {

    GameObject Enemy;
    DogsAI dogAI;
	// Use this for initialization
	void Start () 
    {
        Enemy = GameObject.FindGameObjectWithTag("Enemy");
        dogAI = Enemy.GetComponent<DogsAI>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            Debug.Log("Player Entered");
            transform.root.gameObject.GetComponent<DogsAI>().isDead = true;
        }
    }
}
