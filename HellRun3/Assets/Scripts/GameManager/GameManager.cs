using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour
{
    public static GameManager gameManager { get; private set; }
    GameObject player;
    CharacterController2D cc;
    GameObject camera;
    public GameObject checkpoint;
    PlayerHealth ph;

    public void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        cc = player.GetComponent<CharacterController2D>();
        ph = player.GetComponent<PlayerHealth>();
        camera = GameObject.FindGameObjectWithTag("MainCamera");
        gameManager = this;
    }

    public void Start()
    {

        player = GameObject.FindGameObjectWithTag("Player");
    }

    public void KillPlayer()
    {
        StartCoroutine(KillPlayerCo());
    }

    private IEnumerator KillPlayerCo()
    {
        //player.KillPlayer();
        yield return new WaitForSeconds(10f);

    }

    // Respawn the player and the main camera to the nearest saved checkpoint
    public void RespawnPlayerAtCheckpoint()
    {
        player.transform.position = new Vector2(checkpoint.transform.position.x, checkpoint.transform.position.y);
        camera.transform.position = new Vector3(checkpoint.transform.position.x, camera.transform.position.y, -10);
        ph.currentHealth = ph.maxHealth;
        player.collider2D.enabled = true;
        cc.HandleCollisions = true;
        
        

    }

    public void CurrentCheckpoint(GameObject currentCheckpoint)
    {
        checkpoint = currentCheckpoint;
    }



}
