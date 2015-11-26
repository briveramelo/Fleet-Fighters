using UnityEngine;
using System.Collections;

public class ShieldOff : MonoBehaviour {

	public CircleCollider2D shield;
	public SpriteRenderer shieldSprite;

	public bool shielding;
	public float onTime;
	public float rechargeTime;
	// Use this for initialization
	void Awake () {
		shield = GetComponent<CircleCollider2D> ();
		shieldSprite = GetComponent<SpriteRenderer> ();
		onTime = 1f;
		rechargeTime = 5f;
	}
	
	public IEnumerator Deflect(){
		if (!shielding){
			shielding = true;
			shield.enabled = true;
			shieldSprite.enabled = true;
			yield return new WaitForSeconds(onTime);
			if (shield.enabled){
				StartCoroutine (DeActivate ());
			}
		}
		yield return null;
	}

	public IEnumerator DeActivate(){
		shield.enabled = false;
		shieldSprite.enabled = false;
		yield return new WaitForSeconds (rechargeTime);
		shielding = false;
		yield return null;
	}
}
