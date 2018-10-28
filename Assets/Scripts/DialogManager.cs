using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class DialogManager : MonoBehaviour {
  public TextMeshProUGUI textDisplay1;
  public TextMeshProUGUI textDisplay2;
  private TextMeshProUGUI textDisplay;
  public float typingSpeed;
  public GameObject Panel1;
  public GameObject Panel2;
  private string[] curr_dialog;
  private int curr_index;
  public bool started = false;
  private bool next = false;
  private bool[] panelManager;

	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {
    if(started) {
      // print("Array length: " + panelManager.Length);
      // print("Current Index: " + curr_index);
      if(Input.GetKeyDown(KeyCode.Return) && curr_index < curr_dialog.Length) {
        next = true;
        textDisplay.text = "";
        PanelSwitch(panelManager[curr_index]);
        StartCoroutine(Type(curr_dialog, curr_index));
        curr_index++;
      } else if(Input.GetKeyDown(KeyCode.Return) && curr_index == curr_dialog.Length) {
        Panel1.SetActive(false);
        Panel2.SetActive(false);
        textDisplay.text = "";
        started = false;
        curr_index = 0;
      }
    }
	}

  public void StartDialog(string[] sentences, bool[] panel) {
    curr_dialog = new string[sentences.Length];
    PanelSwitch(panel[0]);
    curr_dialog = sentences;
    panelManager = panel;
    StartCoroutine(Type(sentences, 0));
    curr_index = 1;
    started = true;
  }

  private void PanelSwitch(bool isPanel1) {
    if(isPanel1) {
      Panel1.SetActive(true);
      Panel2.SetActive(false);
      textDisplay = textDisplay1;
    } else {
      Panel2.SetActive(true);
      Panel1.SetActive(false);
      textDisplay = textDisplay2;
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
