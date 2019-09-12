using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class GoodCollectables : MonoBehaviour {

	public GameObject stardustPrefab;
	public List<GameObject> stardusts;
	public int maxStardusts = 10;

	public GameObject starleafPrefab;
	public List<GameObject> starleaves;
	public int maxStarleaves = 5;

	 
	// initialization
	void Start () 
	{
		CreateStardust ();
		CreateStarLeaves ();
	}

	void CreateStardust()
	{
		stardusts = new List<GameObject> ();

		// Spawning stardusts randomly on the screen within the screen's boundaries
		// Quaternion.identity - no rotation
		for (int i = 0; i < maxStardusts; i++) 
		{
			Vector3 screenPosition = new Vector3 (Random.Range (-8.29f, 8.29f), 
				Random.Range (-4.1f, 3.8f), 1);
			GameObject myStardusts = stardustPrefab;
			Instantiate (myStardusts, screenPosition, Quaternion.identity);
			stardusts.Add (myStardusts);

		}
	}
		
	void CreateStarLeaves()
	{
		starleaves = new List<GameObject> ();

		// Spawning starleaves randomly on the screen within the screen's boundaries
		for (int j = 0; j < maxStarleaves; j++) 
		{
			Vector3 screenPosition = new Vector3 (Random.Range (-8.29f, 8.29f), 
				Random.Range (-4.1f, 3.8f), 1);
			GameObject myStarLeaves = starleafPrefab;
			Instantiate (myStarLeaves, screenPosition, Quaternion.identity);
			stardusts.Add (myStarLeaves);
		}
	}
}
	
