using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundtrackManager : MonoBehaviour
{
    GameManager gameManager;

    // Sound variables
    public AudioSource audioSource;
    public AudioClip[] soundtracks;

    void Start()
    {
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();

        // Start looping through the soundtracks
        StartCoroutine(PlaySoundtracks());   
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator PlaySoundtracks()
    {
        while (true)
        {
            foreach (AudioClip soundtrack in soundtracks)
            {
                // Play the current soundtrack
                audioSource.clip = soundtrack;
                audioSource.Play();
                // Wait for the soundtrack to finish playing
                yield return new WaitForSeconds(audioSource.clip.length);
            } 
        }
    }
}
