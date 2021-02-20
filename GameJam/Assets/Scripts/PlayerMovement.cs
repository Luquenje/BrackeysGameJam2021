using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
	public Rigidbody2D rB;
    public Camera cam;
    public SpriteRenderer sprite;
    public Animator anim;
	public bool facingRight;
    public float moveSpeed = 5f;

    Vector2 movement;
    //Vector2 mousePos;
    

    // Update is called once per frame
    void Update()
    {
        anim.SetBool("isRun", false);
        //Input
        movement.x = Input.GetAxisRaw("Horizontal") * moveSpeed;
        movement.y = Input.GetAxisRaw("Vertical") * moveSpeed;

        Vector3 characterScale = transform.localScale;
        
        if (Input.GetAxisRaw("Horizontal") > 0 && facingRight) {
            Flip ();
            facingRight = false;
           
        }

        if (Input.GetAxisRaw("Horizontal") < 0 && !facingRight){
            Flip ();
            facingRight = true;
            anim.SetBool("isRun", true);
        }

        if (Input.GetAxisRaw("Horizontal") > 0 || Input.GetAxisRaw("Horizontal") < 0 || Input.GetAxisRaw("Vertical") < 0 || Input.GetAxisRaw("Vertical") > 0){
            anim.SetBool("isRun", true);
        }

        //mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
    }

    private void FixedUpdate()
    {
        //Movement
        rB.MovePosition(rB.position + movement * moveSpeed * Time.fixedDeltaTime);

        //Vector2 lookDir = mousePos - rB.position;
        //float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg - 90f;
        //rB.rotation = angle;

    }

    void Flip()
{
    // Switch the way the player is labelled as facing
    facingRight = !facingRight;

    sprite.flipX = !sprite.flipX;
}

	

}
