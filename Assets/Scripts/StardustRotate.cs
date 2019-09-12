using UnityEngine;
using System.Collections;

public class StardustRotate : MonoBehaviour 
{
	
	void Update () 
	{
		// rotates stardust every frame
		transform.Rotate (new Vector3 (0, 0, 100) * Time.deltaTime);
	}
}
