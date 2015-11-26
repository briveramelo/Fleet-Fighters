using UnityEngine;
using System.Collections;

public class HotBoxControls : MonoBehaviour {
	
	public Vector2 p1A;
	public Vector2 p1B;
	public Vector2 p2A;
	public Vector2 p2B;
	public float shipSpeed;
	private Camera cam;

	private float midline;
	private float miniBox;
	private int ship;
	private float screenWidth;
	private float screenHeight;

	private Vector2 p1Input;
	private Vector2 p2Input;

	private float p1Xfrac;
	private float p1Yfrac;
	private float p2Xfrac;
	private float p2Yfrac;

	private Vector2 p1Output;
	private Vector2 p2Output;


	// Use this for initialization
	void Awake () {
		if (name == "P1") {
			ship = 1;
		}
		else if (name == "P2") {
			ship = 2;
		}
		cam = GameObject.Find ("Main Camera").camera;
		screenWidth = 2.66667f;
		screenHeight = 4f;

		shipSpeed = .3f;
		miniBox = screenWidth / 4;
		midline = screenHeight / 2;
		p1A = new Vector2 ( (screenWidth - miniBox) / 2, 0);
		p1B = new Vector2 ( (screenWidth + miniBox) / 2, miniBox);

		p2A = new Vector2 ( (screenWidth - miniBox) / 2, screenHeight-miniBox);
		p2B = new Vector2 ( (screenWidth + miniBox) / 2, screenHeight);
	}

	void OnDrawGizmos(){
		Gizmos.DrawLine ( new Vector3 (p1A.x,p1A.y,0f), new Vector3 (p1B.x,p1B.y,0f));
		Gizmos.DrawLine ( new Vector3 (p2A.x,p2A.y,0f), new Vector3 (p2B.x,p2B.y,0f));

		Gizmos.DrawSphere (new Vector3 (p1Output.x, p1Output.y, 0), .25f);
		Gizmos.DrawSphere (new Vector3 (p2Output.x, p2Output.y, 0), .25f);
	}

	// Update is called once per frame
	void Update () {

		foreach (Touch touche in Input.touches) {
			if (touche.position.y<midline && ship == 1){ //player1
				p1Input = Input.touches[touche.fingerId].position;
				if (p1Input.y>p1B.y){
					p1Input = new Vector2 (p1Input.x,p1B.y);
				}
				if (p1Input.x>p1B.x){
					p1Input = new Vector2 (p1B.x,p1Input.y);
				}
				else if (p1Input.x<p1A.x){
					p1Input = new Vector2 (p1A.x,p1Input.y);
				}
				p1Xfrac = (p1Input.x-p1A.x)/miniBox;
				p1Yfrac = (p1Input.y-p1A.y)/(miniBox*2);
				p1Output = new Vector2 ( p1Xfrac * screenWidth, p1Yfrac * screenHeight);
				transform.position = Vector2.Lerp(transform.position,p1Output, shipSpeed * Time.deltaTime);
			}

			else if (touche.position.y>midline  && ship == 2){ //player2
				p2Input = Input.touches[touche.fingerId].position;
				if (p2Input.y<p2B.y){
					p2Input = new Vector2 (p2Input.x,p2B.y);
				}
				if (p2Input.x>p2B.x){
					p2Input = new Vector2 (p2B.x,p2Input.y);
				}
				else if (p2Input.x<p2A.x){
					p2Input = new Vector2 (p2A.x,p2Input.y);
				}
				p2Xfrac = (p2Input.x-p2A.x)/miniBox;
				p2Yfrac = (p2Input.y-p2A.y)/(miniBox*2);
				p2Output = new Vector2 ( p2Xfrac * screenWidth, screenHeight * (p2Yfrac + 0.5f));
				transform.position = Vector2.Lerp(transform.position,p2Output, shipSpeed * Time.deltaTime);
			}
		}
	}
}
