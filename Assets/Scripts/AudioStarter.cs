using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioStarter : MonoBehaviour
{
    AudioSource typeAudio;
    bool typePlay;
    bool mute;

    private void Start()
    {
        typeAudio = GetComponent<AudioSource>();
        typePlay = false;
        mute = true;
    }
    private void Update()
    {
        if (typePlay == true && mute == false)
        {
            typeAudio.mute = !typeAudio.mute;
        }

    }
    private void OnTriggerEnter(Collider other)
    {
        typePlay = true;
        mute = false;

    }
}
