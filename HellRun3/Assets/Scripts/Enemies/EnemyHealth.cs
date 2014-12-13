using UnityEngine;
using System.Collections;

public class EnemyHealth : MonoBehaviour {

    public int health;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void DecrementHealth()
    {
        health--;
    }

    public int GetHealth()
    {
        return health;
    }
}
