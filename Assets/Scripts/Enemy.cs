using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour {

  public int health = 100;
  SpriteRenderer mySpriteRenderer;
  private bool counted = false;
  private int countdown = 10;
  public bool destroyed = false;
	// Use this for initialization
	void Start () {
    mySpriteRenderer = GetComponent<SpriteRenderer>();
	}

	// Update is called once per frame
	void Update () {
    // print(health);
    if(health <= 0) {
      Destroy(gameObject);
      destroyed = true;
    }

    if(counted) {
      countdown--;
      if(countdown <= 0) {
        mySpriteRenderer.color = new Color(1, 1, 1, 1);
        countdown = 10;
        counted = false;
      }
    }
	}

  public void TakeDamage(int damage) {
    health -= damage;
    mySpriteRenderer.color = new Color(1, 1, 1, 0.3f);
    counted = true;
  }

  void OnTriggerStay2D(Collider2D coll) {
    coll.GetComponent<PlayerController>().health--;
  }
}
