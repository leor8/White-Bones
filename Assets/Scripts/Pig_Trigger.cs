using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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

  private string[] convinced = {"White Horse?! How dare you show your face here! Give back the weapon you stole!", "What weapon? What are you talking about?",
                                "When I first being punished and transformed into this body, I was not quiet used to how to move around", "And you took that opportunity and stole my weapon",
                                "You ran so fast and I couldn't catch up with you!", "Your Nine-teeth Rake was a pretty good weapon, I...",
                                "Give it back to me!", "You need to stop interupting me. I lost it and I have never seen it since", "Ahhhh I will kill you"};
  private bool[] con_panel = {false, true, false, false, false, true, false, true, false};

  private string[] convicing = {"Ok you have convinced me. I will join you to save Master Tang with weird looking monkey"};
  private bool[] ing_panel = {false};

  // Getting player inventory
  public GameObject player;
  private Inventory inventory;
  private PlayerController pc;
  public bool attackable = false;
  private int health = 200;

  public Button btn1, btn2;
  private bool done = false;

  private float originalX, maxX;
  private float moving_speed;

	// Use this for initialization
	void Start () {
    dm = dialog.GetComponent<DialogManager>();
    inventory = player.GetComponent<Inventory>();
    pc = player.GetComponent<PlayerController>();
    btn1.onClick.AddListener(Fight);
    btn2.onClick.AddListener(Convince);
    originalX = gameObject.transform.position.x;
    maxX = originalX + 15;
    moving_speed = 0.08f;
	}

	// Update is called once per frame
	void Update () {
    if(!dm.started && done) {
      btn2.gameObject.SetActive(true);
      btn1.gameObject.SetActive(true);

      btn2.GetComponentInChildren<Text>().text = "Convince";
      btn1.GetComponentInChildren<Text>().text = "Fight";
    }

    if(attackable) {
      gameObject.transform.position = new Vector2(gameObject.transform.position.x + moving_speed, gameObject.transform.position.y);
      if(gameObject.transform.position.x >= maxX || gameObject.transform.position.x <= originalX) {
        moving_speed = -moving_speed;
      }
    }
	}

  void OnCollisionEnter2D(Collision2D coll) {
    if(coll.gameObject.CompareTag("Player") && !attackable){

      if(!pc.isConvinced[2]) {
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
      } else if (pc.isConvinced[2] && !attackable) {
        pc.DialogCharacterSwitch(2);
        dm.StartDialog(convinced, con_panel);
        done = true;
      }
    }

    if(attackable && coll.gameObject.CompareTag("Player")) {
      pc.TakeDamage(20);
    }
  }

  private void Convince() {
    done = false;
    pc.isConvinced[1] = true;
    btn2.gameObject.SetActive(false);
    btn1.gameObject.SetActive(false);
    dm.StartDialog(convicing, ing_panel);
    Destroy(gameObject);
  }

  private void Fight() {
    done = false;
    attackable = true;
    btn2.gameObject.SetActive(false);
    btn1.gameObject.SetActive(false);
    gameObject.AddComponent<Enemy>();
    gameObject.GetComponent<Enemy>().AlterHealth(400);
  }
}
