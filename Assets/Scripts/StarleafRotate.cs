using UnityEngine;
using System.Collections;

public class StarleafRotate : MonoBehaviour 
{
	void Update () 
	{
		// rotates starleaf every frame 
		transform.Rotate (new Vector3 (8 * Time.deltaTime, 10, 5));
	}
}
