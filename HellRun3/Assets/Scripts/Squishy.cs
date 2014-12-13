using UnityEngine;
using System.Collections;

public class Squishy : MonoBehaviour {

    
    public string AIName;
    void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.tag== "Player" && AIName.Equals("Bat"))
        {
            gameObject.GetComponentInParent<FlyingBat>().isStomped = true;
        }
        else if(other.gameObject.tag == "Player" && AIName.Equals("Dog"))
        {
            
            gameObject.GetComponentInParent<DogsAI>().isStomped = true;
            Debug.Log("Stomped: " + gameObject.GetComponentInParent<DogsAI>().isStomped);
        }
    }
}
