using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour {
  public float speed; // For movement speed
  public float jumpForce; // for how high the player can jump
  public float doubleJumpForce;

  private KeyCode left, right, jump, switch_char; // Controll

  // Prevent player from jumping nonstop
  public Transform groundCheckPoint;
  public float groundCheckRadius;
  public LayerMask whatIsGround;
  public bool isGrounded = false;
  public bool mid_air = false;

  private Rigidbody2D rb;

  public float health = 50;
  public float MAX_HEALTH = 100;
  public bool inProgress;

  //

	// Use this for initialization
	void Start () {
    rb = GetComponent<Rigidbody2D>();

    left = KeyCode.LeftArrow;
    right = KeyCode.RightArrow;
    jump = KeyCode.UpArrow;
    switch_char = KeyCode.DownArrow;
	}

	// Update is called once per frame
	void Update () {
    if(!inProgress) {
      if(rb.velocity.y == 0) {
        isGrounded = true;
      } else {
        isGrounded = false;
      }

      if(isGrounded) {
        mid_air = true;
      }

      if(isGrounded && Input.GetKeyDown(jump)) {
        rb.velocity = new Vector2(rb.velocity.x, jumpForce);
      } else if(mid_air && Input.GetKeyDown(jump)) {
        rb.velocity = new Vector2(rb.velocity.x, doubleJumpForce);
        mid_air = false;
      }


      // Getting key inputs to move the player
      if(Input.GetKey(left)) {
          rb.velocity = new Vector2(-speed, rb.velocity.y);
      } else if (Input.GetKey(right)) {
          rb.velocity = new Vector2(speed, rb.velocity.y);
      } else {
          rb.velocity = new Vector2(0, rb.velocity.y);
      }

    }



	}

  public void SetActive(bool state) {
    if(!state) {
      inProgress = true;
    } else {
      inProgress = false;
    }
  }

}
