using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    public class SoundManager : MonoBehaviour
    {
        public static SoundManager instance;

        public List<AudioClip> sounds;

        void Awake()
        {
            if (instance == null)
            {
                instance = this;
            }
            else
            {
                Destroy(gameObject);
                return;
            }

            DontDestroyOnLoad(gameObject);
        }
    }
}
