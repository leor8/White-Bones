using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pig_Trigger : MonoBehaviour {
  public GameObject dialog;
  private DialogManager dm;
  private string[] sentences = {"Zzzz...", "Excuse me, are you Pigsy?", "Zzzzzzz....", "Hello?", "Zzzzz... Hungry"};
  private bool[] sent1_panel = {false, true, false, true, false};
  private bool first_encounter = false;

  private string[] sentences2 = {"Watermelon!! How do you know that's my favorite food?", "Thank you so much I feel so energetic right now.",
                              "I'm glad you like it :).", "Yummm....", "What do you need from me? You weird looking monkey.",
                              "...Huh?", "You are a monkey, aren't you?", "Ok... I'm here to ask you a favour.",
                              "My master, Master Tang was kidnapped by the White-Boned De...", "The White-Boned Demon? I know her, she's really powerful",
                              "Yes, that's why I'm looking for help to save my master.", "Uh, I wanted to help but I can't, I lost all my power",
                              "How come?", "Well, a freaking horse stole my weapon and ran away, I can't do anything cause he is so fast",
                              "What if I find the weapon for you?", "Then I'll help you defeating Master Tang!"};
  private bool[] sent2_panel = {false, false, true, false, false, true, false, true, true, false, true, false, true, false, true, false};
  private bool second_encounter = false;

  private string[] sentences3 = {"Have you found my weapon?"};
  private bool[] sent3_panel = {false};

  private string[] sentences4 = {"YES! This is my weapon!! I got all my power back now! Thank you so much weird looking monkey!", "My name is Mon...",
                                "Weird Looking Monkey? I get it! Now let's go and save Tang.", "Oh before that, Now you can press Down Arrow to switch characters",
                                "And I have my weapon with me so we can get the white bone pieces now by simply switching to me and attack.", "We can try to collect all the white bone pieces and try to trick the White-Boned Demon into trading Master Tang",
                                "Oh? How so?", "The white bone pieces were rumored to be able to grant immortality which the White-Boned Demon needs from Tang's flesh",
                                "But in fact white bone pieces can only give people second life based on what I have observed",
                                "I get it! So we will tell the White-Boned Demon that the white bone pieces can grant immortality.",
                                "Yes. But before that, maybe let's try to get the white horse on board as well.", "And also, my superpower is instant heal."};
  private bool[] sent4_panel = {false, true, false, false, false, false, true, false, false, true, false, false};


  // Getting player inventory
  public GameObject player;
  private Inventory inventory;
  private PlayerController pc;
	// Use this for initialization
	void Start () {
    dm = dialog.GetComponent<DialogManager>();
    inventory = player.GetComponent<Inventory>();
    pc = player.GetComponent<PlayerController>();
	}

	// Update is called once per frame
	void Update () {

	}

  void OnCollisionEnter2D(Collision2D coll) {
    if(coll.gameObject.CompareTag("Player")){

      if(!first_encounter && !inventory.watermelon) {
        dm.StartDialog(sentences, sent1_panel);
      }

      if(inventory.watermelon && !first_encounter && !second_encounter) {
        first_encounter = true;
        second_encounter = true;
        dm.StartDialog(sentences2, sent2_panel);
      } else if(!inventory.pig_weapon && first_encounter) {
        dm.StartDialog(sentences3, sent3_panel);
      }

      if(inventory.watermelon && inventory.pig_weapon && second_encounter) {
        dm.StartDialog(sentences4, sent4_panel);
        pc.isConvinced[1] = true;
        Destroy(gameObject);
      }

    }
  }
}
