using UnityEngine;
using System.Collections;

public class CometMove : MonoBehaviour {

	void Update () 
	{
		// creating random speed for comets change in y direction
		int randomYSpeed= Random.Range (0, 3);

		// creating random location for comets x location when wrapping 
		int randomXLoc= Random.Range (-7, 7);

		// moving the comets every frame in a downward direction
		transform.Translate (new Vector3 (0, -randomYSpeed, 5)* Time.deltaTime);

		// once the comets reach the end of the screen, wraps back around at the top
		// with a random x location
		if (transform.position.y < -5) 
		{
			transform.position = new Vector3 (randomXLoc, 5, 0);
		}
	}
}
