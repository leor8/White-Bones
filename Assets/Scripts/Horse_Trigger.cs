using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Horse_Trigger : MonoBehaviour {
  public GameObject dialog;
  private DialogManager dm;
  public GameObject player;
  private PlayerController pc;
  public Button btn1, btn2;

  private string[] sentences = {"Hey! This horse looks intereting and shockingly white", "Horsey horsey",
                                "Why are you touching me?", "AHHHHHHHHH. A talking horse?!", "AHHHHHHHHH. A talking monkey?",
                                "Okay you are just mocking me", "Okay you are just mocking me", "Stop", "Fine. What do you want monkey?",
                                "I'm on my way to find help to save my master Tang, and I'm just looking stuff for Pigsy",
                                "Pigsy? You need help with finding his stuff?"};
  private bool[] sent1_panel = {true, true, false, true, false, true, false, true, false, true, false};

  private string[] sentences2 = {"You changed your mind?"};
  private bool[] sen2_panel = {false};

  private bool done = false;
  private bool refused = false;


	// Use this for initialization
	void Start () {
    dm = dialog.GetComponent<DialogManager>();
    pc = player.GetComponent<PlayerController>();
    btn1.onClick.AddListener(Yes);
    btn2.onClick.AddListener(No);
	}

	// Update is called once per frame
	void Update () {

    if(done && !dm.started) {
      btn2.gameObject.SetActive(true);
      btn1.gameObject.SetActive(true);

      btn2.GetComponentInChildren<Text>().text = "No";
      btn1.GetComponentInChildren<Text>().text = "Yes";
    }
	}

  void OnCollisionEnter2D(Collision2D coll) {
    if(coll.gameObject.CompareTag("Player")) {
      if(!pc.isConvinced[1] && !refused) {
        dm.StartDialog(sentences, sent1_panel);
        done = true;
      }

      if(!pc.isConvinced[1] && refused) {
        dm.StartDialog(sentences2, sen2_panel);
        done = true;
      }
    }
  }

  void Yes() {
    done = false;
    refused = false;
    pc.isConvinced[2] = true;
    btn2.gameObject.SetActive(false);
    btn1.gameObject.SetActive(false);
    Destroy(gameObject);
  }

  void No() {
    done = false;
    btn2.gameObject.SetActive(false);
    btn1.gameObject.SetActive(false);
    refused = true;
  }
}
