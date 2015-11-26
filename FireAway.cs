using UnityEngine;
using System.Collections;

public class FireAway : MonoBehaviour {

	public string bulletString;
	public Transform fireSpot;
	public GameObject bullet;
	public Vector2 bulletVel;

	public float bulletSpeed;

	public bool firing;
	public float timeBetweenShots;

	// Use this for initialization
	void Awake () {
		bulletString = "Prefabs/Bullets/RedBullet2";
		bulletSpeed = 3f;
		timeBetweenShots = .25f;
		if (name.Contains("P1")){
			bulletVel = Vector2.up * bulletSpeed;
		}
		else if (name.Contains("P2")){
			bulletVel = -Vector2.up * bulletSpeed;
		}
		fireSpot = transform.GetChild(0);
	}

	public IEnumerator Fire(){
		if (!firing){
			firing = true;
			bullet = Instantiate (Resources.Load (bulletString), fireSpot.position, Quaternion.identity) as GameObject;
			bullet.rigidbody2D.velocity = bulletVel;
			yield return new WaitForSeconds (timeBetweenShots);
			firing = false;
		}
		yield return null;
	}


}
