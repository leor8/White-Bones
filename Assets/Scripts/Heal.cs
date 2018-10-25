using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heal : MonoBehaviour {
  PlayerController player;
  public GameObject Panel;

  // Use this for initialization
  void Start () {
    player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
  }

  // Update is called once per frame
  void Update () {

  }

  void OnTriggerEnter2D(Collider2D coll) {
    if(coll.CompareTag("Player")) {
      if(player.health + 50 > 100) {
        player.health = 100;
      } else {
        player.health += 50;
      }
      Destroy(gameObject);

      // Start second interaction
      Panel.SetActive(true);
    }

  }
}