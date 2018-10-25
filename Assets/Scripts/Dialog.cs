using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Dialog : MonoBehaviour {
  public TextMeshProUGUI textDisplay;
  public int current_interaction = 0;
  // First interaction
  public string[] sentences;
  private int index;

  // Second interaction
  public string[] sentences2;
  private int index2;

  public float typingSpeed;
  public GameObject Panel;
  public GameObject Player;
  private PlayerController pc;

  public bool started = false;
  void Start() {
    pc = Player.GetComponent<PlayerController>();
    Panel.gameObject.SetActive(true);
  }

  void Update() {

    switch (current_interaction) {
      case 0:
          if(!started) {
            StartCoroutine(Type(sentences, index));
            started = true;
            pc.SetActive(false);
          }
          if(Input.GetKeyDown(KeyCode.Return)) {
            NextSentence(sentences, index);
            index++;
          }
          break;
      case 1: {
          if(!started) {
            StartCoroutine(Type(sentences2, index2));
            started = true;
            // pc.SetActive(false);
          }
          if(Input.GetKeyDown(KeyCode.Return)) {
            NextSentence(sentences2, index2);
            index2++;
          }
          break;
      }
    }
  }

  public void NextSentence(string[] sentences, int index) {
    if(index < sentences.Length - 1) {
      index++;
      textDisplay.text = "";
      StartCoroutine(Type(sentences, index));
    } else {
      textDisplay.text = "";
      Panel.gameObject.SetActive(false);
      pc.SetActive(true);
      started = false;
      current_interaction += 1;
    }
  }


  // For typing animation
	IEnumerator Type(string[] sentences, int index) {
    foreach(char letter in sentences[index].ToCharArray()) {
      textDisplay.text += letter;
      yield return new WaitForSeconds(typingSpeed);
    }

  }

}
