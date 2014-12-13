using UnityEngine;
using System.Collections;

public class Sight : MonoBehaviour {
    GameObject player;
    Transform playerTransform;

    public Ray2D playerPos;
	// Use this for initialization
	void Start () {
        player = GameObject.FindGameObjectWithTag("Player");
        playerTransform = player.transform;
	
	}
	
	// Update is called once per frame
	void Update () {
        LookAtPlayer();
	}

    private void LookAtPlayer()
    {
        playerPos = new Ray2D(transform.position, transform.forward);
        Debug.DrawRay(playerPos.origin, playerPos.direction * 10, Color.red);
        transform.LookAt(playerTransform);
    }
}
