using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class BadCollectables : MonoBehaviour {

	public GameObject asteroidPrefab;
	public List<GameObject> asteroid;
	public int maxAsteroids = 10;

	public GameObject cometPrefab;
	public List<GameObject> comet;
	public int maxComets = 10;
    float currCountdownValue;


	// initialization
	void Start ()
    {
        // start countdown to spawn objects
        StartCoroutine(StartCountdown());

	}

    public IEnumerator StartCountdown(float countdownValue = 10)
    {
        currCountdownValue = countdownValue;
        while(currCountdownValue >= 0)
        {
            Debug.Log("Countdown: " + currCountdownValue);
            yield return new WaitForSeconds(1.0f);
            currCountdownValue--;

            if(currCountdownValue < 0)
            {
                CreateAsteroids();
                CreateComets();
            }
        }
    }

    void CreateAsteroids()
	{
		asteroid = new List<GameObject> ();

		// Spawning asteroids randomly on the screen within the screen's boundaries
		// Quaternion.identity - no rotation
		for (int i = 0; i < maxAsteroids; i++) 
		{
			Vector3 screenPosition = new Vector3 (Random.Range (-8.29f, 8.29f), 
				Random.Range (-4.1f, 3.8f), 1);
			GameObject myAsteroids = asteroidPrefab;
			Instantiate (myAsteroids, screenPosition, Quaternion.identity);
			asteroid.Add (myAsteroids);

		} 

	}

	void CreateComets()
	{
		comet = new List<GameObject> ();

		// Spawning comets randomly on the screen within the screen's boundaries
		for (int j = 0; j < maxComets; j++) 
		{
			Vector3 screenPosition = new Vector3 (Random.Range (-8.29f, 8.29f), 
				Random.Range (-4.1f, 3.8f), 1);
			GameObject myComets = cometPrefab;
			Instantiate (myComets, screenPosition, Quaternion.identity);
			comet.Add (myComets);
		}
	}
}

