using UnityEngine;
using System.Collections;

public class ScreenRotation : MonoBehaviour {

	void Update(){
		Screen.orientation = ScreenOrientation.Portrait;
	}
}
