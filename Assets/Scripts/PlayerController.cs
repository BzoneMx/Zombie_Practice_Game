using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rb;
    public float speed;
    private Vector2 direction;

    private Camera cam;
    private Vector2 mousePos;


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        cam = Camera.main;
    }

    void Update()
    {
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");

        direction = new Vector2(h, v);

        Aiming();
    }

    void FixedUpdate()
    {
        MoveCharacter();
    }

    void MoveCharacter()
    {
        rb.MovePosition((Vector2)transform.position + (direction * speed * Time.deltaTime));
    }

    void Aiming()
    {
        mousePos = cam.ScreenToWorldPoint(Input.mousePosition);

        Vector2 lookDir = mousePos - rb.position;

        float angle = (Mathf.Atan2(lookDir.y, lookDir.x)*Mathf.Rad2Deg);

        rb.rotation = angle;
    }
}
