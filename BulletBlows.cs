using UnityEngine;
using System.Collections;

public class BulletBlows : MonoBehaviour {

	// Use this for initialization
	void Awake () {
		Physics2D.IgnoreLayerCollision (9, 10);
	}
	
	void OnCollisionEnter2D(Collision2D col){
		if (col.gameObject.CompareTag ("Ship")) {
			Health health = col.gameObject.transform.parent.GetComponent<Health>();
			StartCoroutine (health.TakeDamage());
			Destroy (gameObject);
		}
		else if (col.gameObject.CompareTag("Bullet")){
			Destroy(gameObject);
		}
		else if (col.gameObject.CompareTag("Shield")){
			ShieldOff shieldOff = col.gameObject.GetComponent<ShieldOff>();
			StartCoroutine (shieldOff.DeActivate());
			Destroy(gameObject);
		}
	}
	
	/*void OnDestroy(){

	}*/
}
