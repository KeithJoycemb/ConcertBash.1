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
        public Material cubeMaterial;
        bool inContact;
        public float red, green, blue;
        private void Start()
        {
            typeAudio = GetComponent<AudioSource>();
            typePlay = false;
            mute = typeAudio.mute;
            cubeMaterial = GetComponent<MeshRenderer>().materials[0];
            inContact = false;

        }
        private void Update()
        {
            if (typePlay == true && mute == false)
            {
                typeAudio.mute = false;
            }
            if (inContact)
            {
                Color _color = new Color(red * AudioPeer.amplitudeBuffer, green * AudioPeer.amplitudeBuffer, blue * AudioPeer.amplitudeBuffer);
                cubeMaterial.SetColor("_EmissionColor", _color);
            }
                
            else
            {
                Color _color = Color.gray;
                cubeMaterial.SetColor("_EmissionColor", _color);
            }
                
        }
        private void OnTriggerEnter(Collider other)
        {
            typePlay = true;
            mute = false;
            inContact = true;

        }
    }
}
