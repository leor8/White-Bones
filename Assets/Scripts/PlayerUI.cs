using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUI : MonoBehaviour {
  PlayerController pc;
  public GameObject slots;
	// Use this for initialization
	void Start () {
		pc = gameObject.GetComponent<PlayerController>();
	}

	// Update is called once per frame
	void Update () {
    for(int i = 0; i< slots.transform.childCount; i++) {
      if(pc.isConvinced[i]) {
        Image curr_img = slots.transform.GetChild(i).GetComponent<Image>();
        curr_img.color = new Color(1, 1, 1, 1);
      } else {
        Image curr_img = slots.transform.GetChild(i).GetComponent<Image>();
        curr_img.color = new Color(1, 1, 1, 0.39f);
      }
    }
	}
}
