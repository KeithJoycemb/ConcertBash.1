using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    public class AudioStarter : MonoBehaviour
    {
        AudioSource typeAudio;
        bool typePlay;
        bool mute;
        public GameObject interactable;
        public Renderer cubeMaterial;
        bool inContact;
        private void Start()
        {
            typeAudio = GetComponent<AudioSource>();
            typePlay = false;
            mute = typeAudio.mute;
            interactable = GetComponent<GameObject>();
            cubeMaterial = interactable.GetComponent<Renderer>();
            inContact = false;

        }
        private void Update()
        {
            if (typePlay == true && mute == false)
            {
                typeAudio.mute = false;
            }
            if (inContact)
                cubeMaterial.material.SetColor("_Color", Color.red);
            else
                cubeMaterial.material.SetColor("_Color", Color.cyan);
        }
        private void OnTriggerEnter(Collider other)
        {
            typePlay = true;
            mute = false;
            inContact = true;

        }
    }
}
