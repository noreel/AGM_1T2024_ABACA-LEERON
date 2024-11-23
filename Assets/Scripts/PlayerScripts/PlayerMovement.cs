using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    
    private Vector2 movement;
    private Vector2 mousePos;

    public Rigidbody2D rb;
    public Camera cam;
    public float speed = 5f;
    public Transform gunSprite;
    public Transform pSprite;
    public SpriteRenderer sp;
    public Animator anim;

    void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        float speed = Mathf.Sqrt(movement.x * movement.x + movement.y * movement.y);
        anim.SetFloat("speed", speed);
        

        mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
    }

    void FixedUpdate()
    {
       
        rb.MovePosition(rb.position + movement * speed * Time.fixedDeltaTime);

        Vector2 lookDir = mousePos - rb.position;
        float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg - 90f;
        gunSprite.rotation = Quaternion.Euler(0,0, angle);

        if (mousePos.x > pSprite.transform.position.x && sp.flipX)
        {
            sp.flipX = false;
            gunSprite.localScale = new Vector3(1, 1, 1);

        }
        else if (mousePos.x < pSprite.transform.position.x && !sp.flipX)
        {
            sp.flipX = true;
            gunSprite.localScale = new Vector3(-1, 1, 1);
        }
    }


}
