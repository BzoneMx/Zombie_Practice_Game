using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
    public Transform firePoint;
    public LayerMask playerMask;

    public LineRenderer rayLine;
    public GameObject impactFx;
    public GameObject bulletFx;
    public float angleOffset;

    public float damage_stats;

    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            StartCoroutine(Shoot());
        }
    }

    IEnumerator Shoot()
    {
        RaycastHit2D hitInfo = Physics2D.Raycast(firePoint.position, firePoint.right, Mathf.Infinity, playerMask);
        int maxLength = 50;

       // Instantiate(bulletFx, firePoint.position, firePoint.rotation * Quaternion.Euler(new Vector3(0, 0, angleOffset)));

        if (hitInfo)
        {
            Enemy enemy = hitInfo.transform.gameObject.GetComponent<Enemy>();

            if(enemy != null)
            {
                enemy.TakeDamage(damage_stats);
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
