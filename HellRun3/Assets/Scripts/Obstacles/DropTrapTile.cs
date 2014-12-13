using UnityEngine;
using System.Collections;

public class DropTrapTile : MonoBehaviour
{


    public bool changeObstacle = false;
    bool alreadyChanged = false;
    //public GameObject trap;
    public GameObject[] traps;
    bool soundPlayed = false;
    public AudioClip dropSound;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        if (changeObstacle && !alreadyChanged)
        {
            alreadyChanged = true;

            // set traps activate value to true
            //trap.GetComponent("DropTrap");
            //trap.rigidbody2D.isKinematic = false;

            Debug.Log("Activating Traps");
            for (int i = 0; i < traps.Length; i++)
            {
                traps[i].GetComponent("DropTrap");
                if (soundPlayed != true)
                {
                    if (dropSound != null)
                    {
                        AudioSource.PlayClipAtPoint(dropSound, transform.position);
                    }
                    soundPlayed = true;
                }
                traps[i].rigidbody2D.isKinematic = false;
                soundPlayed = false;
            }
        }
    }


    void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.gameObject.tag == "Player")
        {
            changeObstacle = true;
        }
    }
}
