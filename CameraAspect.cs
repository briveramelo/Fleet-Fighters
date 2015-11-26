using UnityEngine;
using System.Collections;

public class CameraAspect : MonoBehaviour {

	public float targetAspect;
	private float windowAspect;
	private float scaleHeight;
	private Camera cam;
	private float scaleWidth;
	private Rect rect;

	// Use this for initialization
	/*void Awake () {
		// set the desired aspect ratio (the values in this example are
		// hard-coded for 16:9, but you could make them into public
		// variables instead so you can set them at design time)
		targetAspect = 9f / 16f;
		
		// determine the game window's current aspect ratio
		windowAspect = (float)Screen.width / (float)Screen.height;
		
		// current viewport height should be scaled by this amount
		scaleHeight = windowAspect / targetAspect;
		
		// obtain camera component so we can modify its viewport
		cam = GetComponent<Camera>();
		
		// if scaled height is less than current height, add letterbox
		if (scaleHeight < 1.0f)
		{
			rect = cam.rect;
			
			rect.width = 1.0f;
			rect.height = scaleHeight;
			rect.x = 0;
			rect.y = (1.0f - scaleHeight) / 2.0f;
			
			cam.rect = rect;
		}
		else // add pillarbox
		{
			scaleWidth = 1.0f / scaleHeight;
			
			rect = cam.rect;
			
			rect.width = scaleWidth;
			rect.height = 1.0f;
			rect.x = (1.0f - scaleWidth) / 2.0f;
			rect.y = 0;
			
			cam.rect = rect;
		}
	}*/
	
	
}
