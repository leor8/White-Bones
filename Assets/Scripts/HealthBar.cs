using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour {
  Image healthBar;
  float maxHealth;
  float health;
  public GameObject Player;
  private PlayerController pc;

	// Use this for initialization
	void Start () {
    pc = Player.GetComponent<PlayerController>();
    healthBar = GetComponent<Image>();
    maxHealth = pc.MAX_HEALTH;
	}

	// Update is called once per frame
	void Update () {
    health = pc.health;
    healthBar.fillAmount = health / maxHealth;
	}
}
