using UnityEngine;
using System.Collections;

public class AsteroidMove : MonoBehaviour {

	// variables to hold random values for x and y
	public int x;
	public int y;

	// variables to hold values of the screen width and screen height
	float screenWidth = 8.49f;
	float screenHeight = 4.1f;

	// Update is called once per frame
	void Update () {

		// set random values using Random.Range, 
		x = Random.Range (-6, 7);
		y = Random.Range (-6, 7);

		// if the x position is less than the left side of the screen minus 1/2
		if (transform.position.x < -screenWidth-0.5) 
		{
			// set the position of the x to the positive screenwidth to wrap the asteroid to the other side
			transform.position = new Vector3 (screenWidth, transform.position.y, 0); 
		} 
		// if the x position is greater than the right side of the screen plus 1/2
		else if (transform.position.x > screenWidth+0.5) 
		{
			// set the position of x to the left side of the screen to wrap the asteroid 
			transform.position = new Vector3 (-screenWidth, transform.position.y, 0); 
		} 
		// if the y position is less than the left side of the screen minus 1/2
		else if (transform.position.y < -screenHeight-0.5) 
		{
			// set the y position to the right side of the screen to wrap the asteroid 
			transform.position = new Vector3 (transform.position.x, screenHeight, 0); 
		} 
		// if the y position is greater than the right side of the screen plus 1/2 
		else if (transform.position.y > screenHeight+0.5) 
		{
			// set the y position to the left side of the screen to wrap the asteroid 
			transform.position = new Vector3 (transform.position.x, -screenHeight, 0); 
		}
		// if not
		else 
		{
			// continue randomly moving the asteroid around the screen by using random x and y values
			transform.Translate (new Vector3 (x, y, 0) * Time.deltaTime);
		}
	}
}
