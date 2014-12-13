using UnityEngine;
using System.Collections;

public class PlayerHealth : MonoBehaviour
{
    GameObject gameManager;
    GameObject player;
    GameManager gm;
    public int maxHealth;
    public int currentHealth;
    public GUITexture healthGUI;
    public Texture[] images;
    public Transform testTrans;

    public AudioClip healSound;
    public AudioClip hurtSound;
    public AudioClip dieSound;

    public bool isDead { get; private set; }

    GameObject camera;

    public GameObject currentSpawnPoint;
    int spawnHeight = 3;
	Animator animCP;

    public bool hasSoul = true;
    bool canDie = true;

    Animator anim;
    // Use this for initialization



    void Start()
    {


        maxHealth = 3;
        currentHealth = maxHealth;
        testTrans = ((GameObject)Instantiate(healthGUI.gameObject)).transform;
        //gameManager = GameObject.FindGameObjectWithTag("GameManager");
        //gm = gameManager.GetComponent<GameManager>();
        player = GameObject.FindGameObjectWithTag("Player");
        camera = GameObject.FindGameObjectWithTag("MainCamera");

        currentSpawnPoint = GameObject.FindGameObjectWithTag("StartPoint");

        anim = GetComponent<Animator>();
    }

    public void ModifyHealth(int amount)
    {
        Debug.Log("Modifying : " + amount);
        for (int i = 0; i < Mathf.Abs(amount); i++)
        {
            if (amount > 0 && currentHealth < maxHealth)
            {
                currentHealth++;
            }
            else
            {
                if (currentHealth > 0)
                {
                    currentHealth--;
                }
            }
        }
        UpdateHealth();
    }

    public void UpdateHealth()
    {
        testTrans.guiTexture.texture = images[currentHealth];
    }

    // Update is called once per frame
    void Update()
    {
        UpdateHealth();
        if (currentHealth <= 0)
        {
            KillPlayer();
        }
    }

    private void KillPlayer()
    {
        StartCoroutine(KillPlayerCo());
    }

    private IEnumerator KillPlayerCo()
    {

       

        anim.SetBool("isDead", true);

        if (canDie)
        {

            if (hasSoul)
            {
                //Debug.Log("Dead WITH soul");
                this.GetComponentInChildren<PlayerDeath>().setDeath();
                hasSoul = false;
                
            }
            else
            {
                //Debug.Log("Died WITHOUT soul");
                yield return new WaitForSeconds(1f);
                Application.LoadLevel("GameOver");
                //currentSpawnPoint = GameObject.FindGameObjectWithTag("StartPoint");
            }
            if (dieSound != null)
            {
                AudioSource.PlayClipAtPoint(dieSound, transform.position);
            }
            canDie = false;
        }
        yield return new WaitForSeconds(1f);
        Respawn();
        



    }

    public void Respawn()
    {
        anim.SetBool("isDead", false);
        canDie = true;
        Debug.Log(player.transform.position);
        player.transform.position = new Vector2(currentSpawnPoint.gameObject.transform.position.x, currentSpawnPoint.gameObject.transform.position.y + spawnHeight);
        camera.transform.position = new Vector3(currentSpawnPoint.gameObject.transform.position.x, currentSpawnPoint.gameObject.transform.position.y, -10f);

        currentHealth = 1;
    }

    public void TakeDamage(int damage)
    {
        if (hurtSound != null && (currentHealth - damage > 0))
        {
            AudioSource.PlayClipAtPoint(hurtSound, transform.position);
        }
        ModifyHealth(-damage);

    }

    public void Heal(int healAmount)
    {
        if (healSound != null)
        {
            AudioSource.PlayClipAtPoint(healSound, transform.position);
        }
        ModifyHealth(healAmount);
    }

    public int getCurrentHealth()
    {
        return currentHealth;
    }

    public void setSpawnPoint(GameObject CP)
    {
		Debug.Log ("Setting a new spawn point");
		if (currentSpawnPoint != GameObject.FindGameObjectWithTag ("StartPoint")) 
		{
			animCP = currentSpawnPoint.GetComponent<Animator>();
			animCP.SetBool("startCP", false);
			animCP.SetBool("resetCP", true);

			currentSpawnPoint.GetComponent<CheckPoint>().alreadyTouched = false;

		}
        currentSpawnPoint = CP;
    }

}
