using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour {
  private float timeBtwAtt;
  public float startTimeBtwAtt;
  public Transform attackPos;
  public float attRange;
  public LayerMask whatIsEnemy;
  public int damage;

	// Update is called once per frame
	void Update () {

    if(timeBtwAtt	<= 0) {
      // Player attacks
      if(Input.GetKey(KeyCode.Space)) {
        Collider2D[] enemiesToDamage = Physics2D.OverlapCircleAll(attackPos.position, attRange, whatIsEnemy);
        for(int i = 0; i < enemiesToDamage.Length; i++) {
          enemiesToDamage[i].GetComponent<Enemy>().TakeDamage(damage);
        }
      }

      timeBtwAtt = startTimeBtwAtt;
    } else {
      timeBtwAtt -= Time.deltaTime;
    }

	}

  void OnDrawGizmosSelected() {
    Gizmos.color = Color.red;
    Gizmos.DrawWireSphere(attackPos.position, attRange);
  }
}
