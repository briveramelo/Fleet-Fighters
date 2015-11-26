using UnityEngine;
using System.Collections;

public class FingerMove : MonoBehaviour {

	private Touch[] fingers;
	public Vector3[] trails;
	private int set;
	private int seak;
	private int maxTrails;
	private float shipSpeed;
	private float shipNum;
	public float screenWidth;
	public float screenHeight;
	private float distAway;
	private float fastThresh;
	private int i;
	private int bufferSize;

	// Use this for initialization
	void Awake () {
		shipSpeed = 1f;
		if (name == "P1"){
			shipNum = 1;
		}
		else if (name == "P1"){
			shipNum = 2;
		}
		maxTrails = 1000;
		bufferSize= 50;
		trails = new Vector3[maxTrails];
		fastThresh = .05f;
		trails [0] = transform.position;
	}
	
	// Update is called once per frame
	void Update () {
		fingers = Input.touches;
		foreach (Touch finger in fingers){
			if (shipNum == 1 && finger.position.y<0){
				set++;
				trails[set] = finger.position;
			}
			else if (shipNum == 2 && finger.position.y>0){
				set++;
				trails[set] = finger.position;
			}
		}

		transform.position = Vector2.Lerp(transform.position,trails[seak],shipSpeed * Time.deltaTime);
		distAway = Vector2.Distance (transform.position, trails [seak]);
		if (distAway<fastThresh){
			seak++;
			if (seak>maxTrails){
				seak = 0;
			}
		}
		if (set>maxTrails){
			set=0;
			i=0;
			while (i<bufferSize){
				trails[i]= trails[bufferSize - i + 1];
				i++;
			}
			set = i;
			//trails
		}

	}
}
