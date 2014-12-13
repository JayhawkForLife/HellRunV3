using UnityEngine;
using System.Collections;

public class TommyGun : MonoBehaviour
{

    public GameObject bulletGo;
    GameObject mainCamera;
    GameObject player;
    Player pc;
    Camera cam;
    public bool canFire { get; private set; }
    float shootingTimer;

    int maxAmmo = 30;
    public int currentAmmo;


    Transform ammoTrans;
    public GUIText ammoScore;
    public GUITexture ammoGUI;

    public AudioClip gunSound;
    public AudioClip reloadSound;



    void Start()
    {
        Instantiate(ammoGUI.gameObject);
        ammoTrans = ((GameObject)Instantiate(ammoScore.gameObject)).transform;
        currentAmmo = maxAmmo;
        canFire = true;
        shootingTimer = 0;

        mainCamera = GameObject.FindGameObjectWithTag("MainCamera");
        player = GameObject.FindGameObjectWithTag("Player");
        pc = player.gameObject.GetComponent<Player>();


        cam = mainCamera.GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        ammoTrans.guiText.text = "" + currentAmmo + "/" + maxAmmo;
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = 10f;
        Vector3 worldPos = cam.camera.ScreenToWorldPoint(mousePos);
        transform.LookAt(worldPos);

        if (shootingTimer <= 0 && currentAmmo > 0)
        {
            canFire = true;
        }
        else
        {
            canFire = false;
            shootingTimer -= Time.deltaTime;
        }

        if (Input.GetButtonDown("Fire1") && canFire)
        {
            if (gunSound != null)
            {
                AudioSource.PlayClipAtPoint(gunSound, transform.position);
            }
            if (pc.isFacingRight)
            {
                GameObject bullet = Instantiate(bulletGo, gameObject.transform.position, Quaternion.identity) as GameObject;
                Physics2D.IgnoreCollision(bullet.collider2D, player.collider2D);
                bullet.rigidbody2D.AddForce(transform.forward * 3000f);
                Destroy(bullet, 1.5f);
            }
            else
            {
                GameObject bullet = Instantiate(bulletGo, gameObject.transform.position, Quaternion.identity) as GameObject;
                Physics2D.IgnoreCollision(bullet.collider2D, player.collider2D);
                bullet.rigidbody2D.AddForce(transform.forward * 3000f);
                Destroy(bullet, 1.5f);
            }
            currentAmmo -= 1;
            Debug.Log(currentAmmo);


        }
    }

    public void addAmmo(int amount)
    {
        if (amount + currentAmmo > maxAmmo)
        {
            currentAmmo = maxAmmo;
        }
        else
        {
            if (reloadSound != null)
            {
                AudioSource.PlayClipAtPoint(reloadSound, transform.position);
            }
            currentAmmo += amount;
        }
    }

    public int getCurrentAmmo()
    {
        return currentAmmo;

    }
}
