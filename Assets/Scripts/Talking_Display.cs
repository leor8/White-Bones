using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Talking_Display : MonoBehaviour {
  public Sprite[] talking;
  public GameObject Player;
  private PlayerController pc;
  Image curr_img;
	// Use this for initialization
	void Start () {
		pc = Player.GetComponent<PlayerController>();
    curr_img = GetComponent<Image>();
	}

	// Update is called once per frame
	void Update () {
    curr_img.sprite = talking[pc.currentCharacter];
	}
}
