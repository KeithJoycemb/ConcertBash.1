using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Game
{
public class Collectable : MonoBehaviour
{
    public AudioSource audioSource;

    public void Collect()
    {
        // Disable the object's renderer
        GetComponent<Renderer>().enabled = false;
        // Add the audio clip to a list or array
        SoundManager.instance.sounds.Add(audioSource.clip);
        // Play the sound
        audioSource.Play();
        // Destroy the object
        Destroy(gameObject);
    }
}
}

