using UnityEngine;
using System.Collections;

public class PlayerDeath : MonoBehaviour
{

    int currentAmmo;
    int dropAmount;
    int leftOverAmmo;
    public GameObject ammo1; //Each ammo back can hold at max 5 bullets 
    public GameObject ammo2;
    public GameObject ammo3;
    public GameObject ammo4;
    public GameObject ammo5;
    public GameObject ammo6;

    public GameObject soul;


    public void setDeath()
    {

        GameObject soulA = Instantiate(soul, new Vector3(transform.position.x -5, transform.position.y), Quaternion.identity) as GameObject;

        GameObject[] ammoArray = { ammo1, ammo2, ammo3, ammo4, ammo5, ammo6 };
        currentAmmo = this.GetComponentInChildren<TommyGun>().getCurrentAmmo();
        dropAmount = currentAmmo / 5;
        leftOverAmmo = currentAmmo - dropAmount * 5;
        for (int i = 0; i < dropAmount; i++)
        {
            GameObject ammoA = Instantiate(ammoArray[i], new Vector3(transform.position.x - dropAmount / 2 + i, transform.position.y, 0), Quaternion.identity) as GameObject;
        }

        this.GetComponentInChildren<TommyGun>().currentAmmo = 0;

    }
}
