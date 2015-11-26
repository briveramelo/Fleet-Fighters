using UnityEngine;
using System.Collections;

public class NatesFirstScript : MonoBehaviour {

	private Vector2 startPosition;
	private Vector2 moveDir;
	private Touch finger1;
	public float shipSpeedSlow;
	public float shipSpeedFast;
	public float slowThresh;
	public float fastThresh;
	private float fingerDist;
	private float currentSpeed;

	// Use this for game object initialization
	void Awake () {
		shipSpeedSlow = 0.5f;
		shipSpeedFast = 1f;
		slowThresh = 15f;
		fastThresh = 30f;
	}

	/*void OnDrawGizmos(){
		Gizmos.DrawWireSphere (startPosition, fastThresh/100);
	}*/

	// Update is called once per frame
	void Update () {
		if (Input.touchCount>0){
			finger1 = Input.touches[0];
			if (finger1.phase == TouchPhase.Began) {
				startPosition = finger1.position;
			}
			else if (finger1.phase == TouchPhase.Moved) {
				moveDir = finger1.position - startPosition;
				fingerDist = moveDir.magnitude;
				moveDir = moveDir.normalized;
				if (fingerDist <= slowThresh){
					currentSpeed = 0;
				}
				else if (fingerDist <= fastThresh && fingerDist > slowThresh) {
					currentSpeed = shipSpeedSlow;
				}
				else if (fingerDist > fastThresh) {
					currentSpeed = shipSpeedFast;
				}
			}
			else if (finger1.phase == TouchPhase.Canceled || finger1.phase == TouchPhase.Ended) {
				currentSpeed = 0;
			}
			rigidbody2D.velocity = moveDir * currentSpeed;
		}
	}
}
