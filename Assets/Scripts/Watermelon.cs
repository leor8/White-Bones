using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Watermelon : MonoBehaviour {
  public GameObject Player;
  private Inventory inventory;

	// Use this for initialization
	void Start () {
    inventory = Player.GetComponent<Inventory>();
	}

	// Update is called once per frame
	void Update () {

	}

  void OnCollisionEnter2D(Collision2D coll) {
    if(coll.gameObject.CompareTag("Player")){
      inventory.watermelon = true;
      Destroy(gameObject);
    }
  }
}
