using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioClipManager : Singleton<AudioClipManager>
{
    public AudioSource audioSource;

    public AudioClip clickSound;
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StopSound()
    {
        audioSource.Stop();
    }

    public void ButtonClickedSound()
    {
        audioSource.PlayOneShot(clickSound);
    }

}
