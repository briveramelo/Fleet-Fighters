using UnityEngine;
using System.Collections;

public class RelativeJoystick : MonoBehaviour {


	private SpriteRenderer startSprite;
	private SpriteRenderer fingerSprite;

	private FireAway fireAway;
	private ShieldOff shieldOff;

	private Vector2 startPosition;
	public Vector2 moveDir;
	private Vector2 rawMoveDir;
	private Vector2 endMoveDir;
	private Vector2 moveDir8;
	private Vector2 dispMoveDir;
	private Vector2 downRight;
	private Vector2 upLeft;
	private Vector2 lastMoveDir;
	private Vector2 correctionSpot;
	public Vector2 fingSpot;
	public Vector2 correctedSpot;

	public float shipSpeed;
	private float currentSpeed;
	private float fingerDist;
	public float distThresh;
	public float fingerThresh;
	private float vect225;
	private float vect675;
	private float vect45;
	private float dispDistAway;
	private float maxShipSpeed;

	private int shipNum;
	private int fingerNum;
	private int numFingers;

	public bool quantizedMovement;

	// Use this for initialization
	void Start () {
		if (name.Contains("P1")){
			shipNum = 1;
			startSprite = GameObject.Find ("StartFinger1").GetComponent<SpriteRenderer> ();
			fingerSprite = GameObject.Find ("Finger1").GetComponent<SpriteRenderer> ();
		}
		else if (name.Contains("P2")){
			shipNum = 2;
			startSprite = GameObject.Find ("StartFinger2").GetComponent<SpriteRenderer> ();
			fingerSprite = GameObject.Find ("Finger2").GetComponent<SpriteRenderer> ();
		}
		fireAway = GetComponent<FireAway> ();
		shieldOff = transform.GetChild (1).GetComponent<ShieldOff>();
		shipSpeed = 1.5f;
		maxShipSpeed = 1.5f;
		distThresh = 0.15f;
		fingerThresh = 0.01f;
		dispDistAway = 0.6f;

		vect225 = Mathf.Sin (22.5f * Mathf.Deg2Rad); //.3826
		vect675 = Mathf.Sin (67.5f * Mathf.Deg2Rad); //.9238
		vect45 = Mathf.Sin (45f * Mathf.Deg2Rad); //.707
		downRight = new Vector2 (vect45, -vect45);
		upLeft = new Vector2 (-vect45, vect45);

		correctionSpot = new Vector2 (1.25f, 2.1f);

		fingerNum = -1;
		numFingers = 1;
	}

	/*public Vector2 ConvertTo8s(Vector2 normMoveDir){
		if (normMoveDir.x >= vect675) {
			moveDir8 = Vector2.right;
		}
		else if (normMoveDir.x <= -vect675){
			moveDir8 = -Vector2.right;
		}
		else if (normMoveDir.y >=vect675){
			moveDir8 = Vector2.up;
		}
		else if (normMoveDir.y <= -vect675){
			moveDir8 = -Vector2.up;
		}
		else if (normMoveDir.x >= vect225 && normMoveDir.x < vect675 && normMoveDir.y >= vect225 && normMoveDir.y < vect675 ){
			moveDir8 = Vector2.one * vect45;
		}
		else if (normMoveDir.x >= vect225 && normMoveDir.x < vect675 && normMoveDir.y <= -vect225 && normMoveDir.y > -vect675 ){
			moveDir8 = downRight;
		}
		else if (normMoveDir.x <= -vect225 && normMoveDir.x > -vect675 && normMoveDir.y >= vect225 && normMoveDir.y < vect675 ){
			moveDir8 = upLeft;
		}
		else if (normMoveDir.x <= -vect225 && normMoveDir.x > -vect675 && normMoveDir.y <= -vect225 && normMoveDir.y > -vect675 ){
			moveDir8 = -Vector2.one * vect45;
		}

		return moveDir8;
	}
	*/
	
	void BeginMoving(Touch finger){
		numFingers++;
		fingerNum = finger.fingerId;
		startPosition = correctedSpot;
		startSprite.transform.position = correctedSpot;
		startSprite.enabled = true;
		fingerSprite.transform.position = correctedSpot;
		fingerSprite.enabled = true;
	}

	void Moving(){
		rawMoveDir = correctedSpot-startPosition;
		fingerDist = rawMoveDir.magnitude;
		moveDir = rawMoveDir.normalized;
		if (fingerDist <= distThresh){
			currentSpeed = 0;
		}
		else {
			currentSpeed = shipSpeed;
		}
	}

	void Move(){
		//moveDir8 = ConvertTo8s(moveDir);
		endMoveDir = rawMoveDir;
		dispMoveDir = rawMoveDir;
		if (rawMoveDir.magnitude>dispDistAway){
			dispMoveDir = Vector2.ClampMagnitude(rawMoveDir,dispDistAway);
			endMoveDir = Vector2.ClampMagnitude(rawMoveDir,maxShipSpeed);
		}
		
		rigidbody2D.velocity = endMoveDir * currentSpeed;
		fingerSprite.transform.position = startPosition + dispMoveDir;
		lastMoveDir = moveDir;
	}

	void StopMoving(){
		currentSpeed = 0;
		startSprite.enabled = false;
		fingerSprite.enabled = false;
		fingerNum = -1;
		numFingers--;
	}
	
	// Update is called once per frame
	void Update () {

		if (Input.touchCount>0) {
			foreach (Touch finger in Input.touches){
				fingSpot = finger.position;
				correctedSpot = fingSpot /100f - correctionSpot;
				if ((correctedSpot.y<0 && shipNum==1) || (correctedSpot.y>0 && shipNum==2)){
					if (finger.phase == TouchPhase.Began){// || (Mathf.Sign(lastMoveDir.x)!=Mathf.Sign(moveDir.x)) || (Mathf.Sign(lastMoveDir.y)!=Mathf.Sign(moveDir.y) ) ){
						if (numFingers == 1){
							BeginMoving(finger);
						}
						else if (numFingers == 2){
							StartCoroutine(fireAway.Fire());
						}
					}
				}
				if (finger.fingerId == fingerNum){
					if (finger.phase == TouchPhase.Moved){
						Moving();
					}
					else if (finger.phase == TouchPhase.Canceled || finger.phase == TouchPhase.Ended){
						StopMoving ();
					}
					Move ();
				}
			}
		}
	}

	void OnDestroy (){
		fingerSprite.enabled = false;
		startSprite.enabled = false;
	}
}
