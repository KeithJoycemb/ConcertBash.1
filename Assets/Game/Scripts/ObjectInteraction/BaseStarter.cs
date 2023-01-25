using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    public class BaseStarter : MonoBehaviour
    {
        AudioSource baseAudio;
        bool basePlay;
        bool toggle;

        private void Start()
        {
            baseAudio = GetComponent<AudioSource>();
            basePlay = false;
            toggle = false;
        }
        private void Update()
        {
            if (basePlay == true && toggle == true)
            {
                baseAudio.Play();
                toggle = false;
            }
            if (basePlay == false && toggle == true)
            {
                baseAudio.Stop();
                toggle = false;
            }
        }
        private void OnTriggerEnter(Collider other)
        {
            basePlay = true;
            toggle = true;

        }
    }
}
