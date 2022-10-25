using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour
{   
    private AudioSource audioSource; 

    public AudioClip bgAudio;

    public AudioClip winAudio;

    public AudioClip loseAudio;
    // Start is called before the first frame update
    void Start()
    {
        //play bg music on start
       audioSource = GetComponent<AudioSource>();
       audioSource.clip = bgAudio; 
       //audioSource.Play();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ChangeMusic (string music)
    {
        if (music == "win") {
            audioSource.Stop();
            audioSource.clip = winAudio;
            audioSource.Play();
        }

        else if (music == "lose") {
            audioSource.Stop();
            audioSource.clip = loseAudio;
            audioSource.Play();
        }
    }
}
