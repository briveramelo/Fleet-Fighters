using UnityEngine;
using System.Collections;

public class Health : MonoBehaviour {
	
	public int lives;
	public int numUsed;
	public string shipName;
	public GameObject[] ships;
	public Vector3 startPos;
	public FireAway fireAway;
	public TextMesh lifeCount;
	public TextMesh again;

	// Use this for initialization
	void Awake () {
		lives = 5;
		numUsed = 0;
		ships = new GameObject[lives];
		if (name == "P1_Parent") {
			shipName = "Prefabs/Ships/P1";
			startPos = new Vector2 (0f,-1.5f);
			ships[0] = GameObject.Find ("P1");
			//fireAway = GameObject.Find ("P1_Button").GetComponent<FireAway>();
			lifeCount = GameObject.Find ("LifeCount1").GetComponent<TextMesh>();
		}
		else if (name == "P2_Parent") {
			shipName = "Prefabs/Ships/P2";
			startPos = new Vector2 (0f,1.5f);
			ships[0] = GameObject.Find ("P2");
			//fireAway = GameObject.Find ("P2_Button").GetComponent<FireAway>();
			lifeCount = GameObject.Find ("LifeCount2").GetComponent<TextMesh>();
		}
		again = GameObject.Find ("Again").GetComponent<TextMesh> ();

		again.color = Color.clear;
	}
	
	public IEnumerator TakeDamage(){
		Destroy (ships[numUsed]);
		//fireAway.enabled = false;
		lives--;
		numUsed++;
		lifeCount.text = lives.ToString();
		StartCoroutine (Respawn());
		yield return null;
	}

	public IEnumerator Respawn(){
		if (lives<1){ 
			again.color = Color.white;
			yield return new WaitForSeconds (2f);
			Application.LoadLevel(Application.loadedLevelName);
		}
		yield return new WaitForSeconds (2f);
		if (lives > 0) {
			ships[numUsed] = Instantiate (Resources.Load (shipName),startPos,Quaternion.identity) as GameObject;
			ships [numUsed].transform.SetParent (transform);
			//fireAway.fireSpot = ships [numUsed].transform.GetChild (0);
			//fireAway.enabled = true;
		}

		yield return null;
	}
}
