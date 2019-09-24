using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
[RequireComponent(typeof(AudioSource))]

public class PlayerMove : MonoBehaviour {

	// for respawning stardust
	public GameObject stardustPrefab;
	public List<GameObject> stardusts;
	public int maxStardusts = 10;

	// for respawning starleaves
	public GameObject starleafPrefab;
	public List<GameObject> starleaves;
	public int maxStarleaves = 5;

	// speed at which tardigrade moves
	public float speed = 2.5f;
	// variables to store the 
	private float minX, maxX, minY, maxY;


    //public int startTime;
    public float currentTime;
    public Text timeText;

    //audio variables
    public AudioClip pickupSound1;
    public AudioClip pickupSound2;
    public AudioClip injurySound1;
    public AudioClip injurySound2;
    public AudioClip gameoverSound;

    private Animator animator;

    // invulnerability
    bool damaged = true;
    bool invul = false;
    public float invulTime = 3f;
    public float counter = 0;


    // get health component
    public GetHealth getHP;

    // initialization
    void Start () {

        animator = this.GetComponent<Animator>();
        StartCoroutine(StartCountdown());

        // viewport tow world point is relative to the camera
        // getting the distance between the gameObject to camera
        float distance = Vector3.Distance(transform.position, Camera.main.transform.position);
		Vector2 bottom = Camera.main.ViewportToWorldPoint(new Vector3(0,0, distance));
		Vector2 top = Camera.main.ViewportToWorldPoint(new Vector3(1,1, distance));

		// min and max values for the size of screen 
		minX = bottom.x;
		maxX = top.x;
		minY = bottom.y;
		maxY = top.y;

        // get health script
        getHP = GameObject.Find("GetHealth").GetComponent<GetHealth>();
	}
	
	void Update()
	{
		Vector3 mov = new Vector3(Input.GetAxis ("Horizontal"), Input.GetAxis ("Vertical"), 0);
		transform.position += mov * speed * Time.deltaTime;

		move();

		// Get current position
		Vector3 pos = transform.position;

		// horizontal bounds wrap around
		// if tardigrade's position is less than minX (-8.88), then it wraps back around from x = 8.88
		if (pos.x < minX) 
		{
			pos.x = -minX;
		}

		if (pos.x > maxX) 
		{
			pos.x = -maxX;
		}
			
		// vertical bounds wrap around
		// if tardigrade's position is less than minY (-5), then it wraps back around from y = 5
		if (pos.y < minY) 
		{
			pos.y = -minY;
		}

		if (pos.y > maxY) 
		{
			pos.y = -maxY;
		}

		// Update position
		transform.position = pos;

        // grants invulnerability
        if (invul)
        {
            counter += Time.deltaTime;
            if(counter > invulTime)
            {
                counter = 0;
                invul = false;
                damaged = true;
            }
        }
	}

    public IEnumerator StartCountdown(float startTime = 10)
    {
        currentTime = startTime;
        while (currentTime >= 0)
        {
            yield return new WaitForSeconds(1.0f);
            currentTime--;

            if (currentTime < 0)
            {

            }
        }
    }

    // create stardust for respawning
    void CreateStardust()
	{
		stardusts = new List<GameObject> ();

		for (int i = 0; i < maxStardusts; i++) 
		{
			Vector3 screenPosition = new Vector3 (Random.Range (-8.29f, 8.29f), 
				Random.Range (-4.1f, 3.8f), 1);
			GameObject myStardusts = stardustPrefab;
			Instantiate (myStardusts, screenPosition, Quaternion.identity);
			stardusts.Add (myStardusts);

		}
	}

	// create starleaves for respawning
	void CreateStarLeaves()
	{
		starleaves = new List<GameObject> ();

		for (int j = 0; j < maxStarleaves; j++) 
		{
			Vector3 screenPosition = new Vector3 (Random.Range (-8.29f, 8.29f), 
				Random.Range (-4.1f, 3.8f), 1);
			GameObject myStarLeaves = starleafPrefab;
			Instantiate (myStarLeaves, screenPosition, Quaternion.identity);
			stardusts.Add (myStarLeaves);
		}
	}

	void move()
	{
		bool left, right, up, down;
		left = false;
		right = false;
		up = false;
		down = false;

		// Input default player movements either WASD, or left,right,down,up keys
		if(Input.GetAxis("Horizontal") > 0)
		{
			right = true;
            animator.SetInteger("Direction", 0);
		}

		else if(Input.GetAxis("Horizontal") < 0)
		{
			left = true;
            animator.SetInteger("Direction", 1);
        }

		if(Input.GetAxis("Vertical") >0)
		{
			up = true;
		}

		else if(Input.GetAxis("Vertical") < 0)
		{
			down = true;
		}

	}

	// collision/pick up detection
	// if a collider hits another collider trigger
	void OnTriggerEnter2D(Collider2D other) 
	{
		// if the game object is a stardust by comparing tags at time of collision
		if (other.gameObject.CompareTag ("Stardust")) {
			// make the stardust disappear
			other.gameObject.SetActive (false);
			// respawn stardust
			CreateStardust ();

            //play audio if hit stardust
            SoundManager.instance.RandomizeSfx(pickupSound1, pickupSound2);



		} 
		// if the game object is a starleaf by comparing tags at time of collision
		else if (other.gameObject.CompareTag ("Starleaf")) 
		{
			// make starleaf disappear
			other.gameObject.SetActive (false);
			// respawn starleaves
			CreateStarLeaves ();

            //play audio if hit star leaf
            SoundManager.instance.RandomizeSfx(pickupSound1, pickupSound2);
        }
		// if the game object is an asteroid by comparing tags at time of collision
		else if (other.gameObject.CompareTag ("Asteroid") && damaged & !invul) 
		{

            //play audio if hit asteroid
            SoundManager.instance.RandomizeSfx(injurySound1, injurySound2);

            // activate invulnerablity
            invul = true;
            // only get hit once
            damaged = false;

            // get health variable from GetHealth script and subtract it
            getHP.health--;

            // if you're dead...
            if (getHP.health == 0)
            {
                // load the game over screen & play game over audio
                animator.SetInteger("Direction", 2);
                Application.LoadLevel(3);
                SoundManager.instance.PlaySingle(gameoverSound);

            }

        }
		// if the game object is a comet by comparing tags at time of collision
		else if (other.gameObject.CompareTag ("Comet") && damaged && !invul) 
		{

            //play audio if hit comet
            SoundManager.instance.RandomizeSfx(injurySound1, injurySound2);

            // activate invulnerablity
            invul = true;
            // only get hit once
            damaged = false;

            // get health variable from GetHealth script and subtract it
            getHP.health--;

            // if you're dead...
            if (getHP.health == 0)
            {
              // load the game over screen & play game over audio
             animator.SetInteger("Direction", 2);
             Application.LoadLevel(3);
            SoundManager.instance.PlaySingle(gameoverSound);

            }
        }

    }

}
