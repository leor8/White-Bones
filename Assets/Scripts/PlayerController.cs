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

  // Player Switching
  // private string[] characters = ["Wu", "Zhu", "Bai", "Sha", "Tang"];
  public Sprite[] characters;
  public bool[] isConvinced = {true, false, false, false, false};
  public int currentCharacter = 0;
  private bool switched = false;
  private int switch_cd = 30;
  private bool dead = false;

  // Shrinking monkey
  public float targetScale = 0.1f;
  public float shrinkSpeed = 0.1f;
  private bool shrinking = false;
  private bool growing = true;
  private bool shrunk = false;
  private float targetGrow;

  // Heal pig
  private bool healed = false;
  private int heal_cd = 7200;

  // Jump horse
  private bool jumped = false;

	// Use this for initialization
	void Start () {
    rb = GetComponent<Rigidbody2D>();

    left = KeyCode.LeftArrow;
    right = KeyCode.RightArrow;
    jump = KeyCode.UpArrow;
    switch_char = KeyCode.DownArrow;

    targetGrow = gameObject.transform.localScale.x;
	}

	// Update is called once per frame
	void Update () {
    if(!inProgress) {
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

      // Switching character
      if(Input.GetKey(switch_char)) {
        SwitchCharacter();
      }

      // Abilities
      // Monkey King
      if(currentCharacter == 0) {
        if(growing && Input.GetKey(KeyCode.B)) {
          shrinking = true;
        } if (shrunk && Input.GetKey(KeyCode.B)) {
          shrunk = false;
        }

        if (shrinking) {
           gameObject.transform.localScale -= Vector3.one*Time.deltaTime*shrinkSpeed;
           if (gameObject.transform.localScale.x < targetScale) {
              shrinking = false;
              growing = false;
              shrunk = true;
            }
        }

        if(!shrunk && !growing) {
          gameObject.transform.localScale += Vector3.one*Time.deltaTime*shrinkSpeed;
           if (gameObject.transform.localScale.x > targetGrow) {
              growing = true;
              shrunk = false;
            }
        }
      } else if (currentCharacter == 1) {
        if(Input.GetKey(KeyCode.B) && !healed) {
          health += 50;
          healed = true;
        }
      } else if (currentCharacter == 2) {
        if(Input.GetKeyDown(KeyCode.B) && !jumped) {
          print("jumped");
          jumpForce += 5;
          doubleJumpForce += 5;
          jumped = true;
        } else if(Input.GetKeyDown(KeyCode.B) && jumped) {
          print("unjump");
          jumpForce -= 5;
          doubleJumpForce -= 5;
          jumped = false;
        }
      }
      // Pig heal cooldown will be counted even in other form
      if(healed) {
        heal_cd--;
        if(heal_cd <= 0) {
          heal_cd = 7200;
          healed = false;
        }
      }

      // Make sure other character wont jump higher
      if(jumped && currentCharacter != 2) {
        jumpForce -= 5;
        doubleJumpForce -= 5;
        jumped = false;
      }

      if(shrunk && currentCharacter != 0) {
        gameObject.transform.localScale += Vector3.one*Time.deltaTime*shrinkSpeed;
         if (gameObject.transform.localScale.x > targetGrow) {
            growing = true;
            shrunk = false;
          }
      }

      // Check if all characters are dead
      for(int i = 0; i < isConvinced.Length; i++) {
        if(isConvinced[i]) {
          break;
        } else if (i == isConvinced.Length - 1 && !isConvinced[i]) {
          Destroy(gameObject);
          dead = true;
        }
      }

      if(health <= 0 && !dead) {
        isConvinced[currentCharacter] = false;
        SwitchCharacter();
        health = 100;
      }

      if(switched) {
        switch_cd--;
        if(switch_cd <= 0) {
          switched = false;
          switch_cd = 30;
        }
      }


    }
	}

  public void TakeDamage(int amount) {
    health -= amount;
  }

  public void DealDamage(int amount) {

  }

  void OnCollisionEnter2D(Collision2D coll) {
    if(coll.gameObject.CompareTag("ground")){
      isGrounded = true;
    }

    if(coll.gameObject.CompareTag("Climbable")) {
      rb.gravityScale = 2.5f;
    }
  }

  void OnCollisionExit2D(Collision2D coll) {
    if(coll.gameObject.CompareTag("ground")){
      isGrounded = false;
    }

    if(coll.gameObject.CompareTag("Climbable")) {
      rb.gravityScale = 1;
    }
  }

  public void SetActive(bool state) {
    if(!state) {
      inProgress = true;
    } else {
      inProgress = false;
    }
  }

  private void SwitchCharacter() {
    int i;
    if(currentCharacter < isConvinced.Length - 1) {
      i = currentCharacter + 1;
    } else {
      i = 0;
    }
    while(!switched && !shrunk) {
      if(isConvinced[i]) {
        currentCharacter = i;
        SpriteRenderer sr = gameObject.GetComponent<SpriteRenderer>();
        sr.sprite = characters[i];
        switched = true;
      } else {
        if(i < isConvinced.Length - 1) {
          i++;
        } else {
          i = 0;
        }
      }
    }
  }

  public void DialogCharacterSwitch(int index) {
    if(isConvinced[index]) {
      currentCharacter = index;
      SpriteRenderer sr = gameObject.GetComponent<SpriteRenderer>();
      sr.sprite = characters[index];
    }
  }

}
