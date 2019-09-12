using UnityEngine;
using System.Collections;

public class SoundManager : MonoBehaviour {

    public AudioSource efxSource;
    public static SoundManager instance = null;

    //+ or - 5% of original pitch of the audio source
    public float lowPitchRange = .95f;
    public float highPitchRange = 1.05f;



	// Use this for initialization
	void Awake () {

        if(instance == null)
        {instance = this;   }

        if(instance != this)
        { Destroy(gameObject);  }

        DontDestroyOnLoad(gameObject);
	}
	
    //public so this can be called by other scripts
    //this is for actions upon collision (sound effeccts)
    //doesnt execute the game logic
    public void PlaySingle(AudioClip clip)
    {
        efxSource.clip = clip;
        efxSource.Play();
    }

    //pass in list of arguments of the same type as specified by the parameter
    //allows for any number of audio cclips to be passed in
    public void RandomizeSfx(params AudioClip [] clips)
    {
        int randomIndex = Random.Range(0, clips.Length); //will choose random clip from array to play
        float randomPitch = Random.Range(lowPitchRange, highPitchRange);

        efxSource.pitch = randomPitch;
        efxSource.clip = clips[randomIndex];
        efxSource.Play();
    }

}
