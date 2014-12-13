using UnityEngine;
using System.Collections;

public class InstaKill : MonoBehaviour {

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag == "Player")
            other.gameObject.GetComponent<PlayerHealth>().TakeDamage(3);
        else if (other.gameObject.tag == "DogAI")
        {
            Debug.Log("Enemy touching buzzsaw");
            other.gameObject.GetComponent<EnemyHealth>().health = 0;
            int currentHealth = other.gameObject.GetComponent<EnemyHealth>().GetHealth();
            if (currentHealth == 0)
            {
                other.gameObject.GetComponent<DogsAI>().isDead = true;
            }
        }
       else if(other.gameObject.tag == "PacerAI")
        {
			other.gameObject.GetComponent<EnemyHealth>().health = 0;
            int currentHealth = other.gameObject.GetComponent<EnemyHealth>().GetHealth();
            if (currentHealth == 0)
            {
                other.gameObject.GetComponent<PacerAI>().isDead = true;
            }
        }
        else if (other.gameObject.tag == "BatAI")
        {
            Destroy(gameObject);
            other.gameObject.GetComponent<EnemyHealth>().health = 0;
            int currentHealth = other.gameObject.GetComponent<EnemyHealth>().GetHealth();
            if (currentHealth == 0)
            {

                other.gameObject.GetComponent<FlyingBat>().isDead = true;

            }
        }
        else if (other.gameObject.tag == "DemonRange")
        {
            other.gameObject.GetComponent<EnemyHealth>().health = 0;
            int currentHealth = other.gameObject.GetComponent<EnemyHealth>().GetHealth();
            if (currentHealth == 0)
            {
                other.gameObject.GetComponent<DemonRanged>().isDead = true;
            }
        }
    }
	
}
