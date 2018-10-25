using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Lvl1 : MonoBehaviour {
  public Enemy enemy;

	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {

	}

  void OnTriggerEnter2D(Collider2D coll) {
    if(enemy.destroyed) {
      SceneManager.LoadScene(1);
    }

  }
}
