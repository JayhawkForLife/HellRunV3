using UnityEngine;
using System.Collections;

public class PoisonBall : MonoBehaviour 
{
    // Check to see if the poison ball touches the player
    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag == "Player")
        {
            other.GetComponent<PlayerHealth>().SendMessage("TakeDamage", 1, SendMessageOptions.DontRequireReceiver);
        }
    }

}
