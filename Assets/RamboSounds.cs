using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RamboSounds : MonoBehaviour
{
    public AudioClip[] clips;
    public AudioSource audioSource;

    public float soundInterval;
    float nextSoundTime;

    // Start is called before the first frame update
    void Start()
    {
        nextSoundTime = Time.time + soundInterval;
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time > nextSoundTime)
        {
            Debug.Log("Play");
            nextSoundTime += soundInterval;
            audioSource.clip = clips[Random.Range(0, clips.Length - 1)];
            audioSource.Play();
        }
    }
}
