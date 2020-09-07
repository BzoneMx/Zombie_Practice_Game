using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
    //Access Weapon Data 
    public WeaponData data;
   
    private string Name;
    private string Desc;

    private Sprite Img;
    private Color Color;

    private int Speed;
    private int Dexterity;
    private int Attack;
    private int MaxAmmo;
    private float ReloadTime;

    private float nextTimetoFire = 0f;
    private bool isReloading;
    private int currentAmmo;

    //Basic Weapon GFX
    public LineRenderer rayLine;
    public GameObject impactFx;
    public GameObject bulletFx;

    public Transform firePoint;
    public LayerMask enemyMask;

    //Sets Weapon Data
    void SetData()
    {
        Name = data.name;
        Desc = data.desc;

        Img = data.img;
        Color = data.color;

        Speed = data.speed; //access through get & set.
        Dexterity = data.dexterity;
        Attack = data.attack;

        ReloadTime = data.reloadTime;
        MaxAmmo = data.maxAmmo;

        currentAmmo = MaxAmmo;
    }

    void Start()
    {
        SetData(); 
    }

    void Update()
    {
        //Cant shoot or reload whilst already reloading.
        if (isReloading)
            return;

        //if you run out of ammo, or you press r and u have less than your maximum ammo, reload.
        if (currentAmmo <= 0 || Input.GetKeyDown(KeyCode.R) && currentAmmo <= MaxAmmo - 1)
        {
            StartCoroutine(Reload());
            return;
        }

        //Input check with dexterity delay
        if (Input.GetButtonDown("Fire1") && Time.time >= nextTimetoFire)
        {
            nextTimetoFire = Time.time + 1f / Dexterity;
            StartCoroutine(Shoot());
        }
        
    }

    IEnumerator Reload()
    {
        isReloading = true;
        Debug.Log("Reloading");

        yield return new WaitForSeconds(ReloadTime);
        Debug.Log("Done");

        currentAmmo = MaxAmmo;
        isReloading = false;
    }

    IEnumerator Shoot()
    {
        currentAmmo--;

        RaycastHit2D hitInfo = Physics2D.Raycast(firePoint.position, firePoint.right, Mathf.Infinity, enemyMask);
        int maxLength = 50;

       // Instantiate(bulletFx, firePoint.position, firePoint.rotation * Quaternion.Euler(new Vector3(0, 0, angleOffset)));

        if (hitInfo)
        {
            Enemy enemy = hitInfo.transform.gameObject.GetComponent<Enemy>();

            if(enemy != null)
            {
                enemy.TakeDamage(Attack);
            }

            Instantiate(impactFx, hitInfo.point, Quaternion.identity);
            //Destroy impactFx

            rayLine.SetPosition(0, firePoint.position);
            rayLine.SetPosition(1, hitInfo.point);
        } else
        {
            rayLine.SetPosition(0, firePoint.position);
            rayLine.SetPosition(1, firePoint.position + firePoint.right * maxLength);
        }

        rayLine.enabled = true;
        yield return new WaitForSeconds(0.02f);
        rayLine.enabled = false;
    }
}
