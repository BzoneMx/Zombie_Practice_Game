using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
    public Transform firePoint;
    public LayerMask playerMask;

    public LineRenderer rayLine;

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

        if (hitInfo)
        {
            //Enemy script (get component)
            //if enemy isnt null damage em
            //instantiate impact effect

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
