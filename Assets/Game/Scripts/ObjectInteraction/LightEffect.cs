using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    public class LightEffect : MonoBehaviour
    {
        public Light lightSource;
        public float minIntensity = 0.5f;
        public float maxIntensity = 1.5f;
        public float minGrabIntensity = 0.2f;
        public float maxGrabIntensity = 0.5f;
        public float speed = 2.0f;
        public bool isGrabbed = false;

        void Update()
        {
            if (isGrabbed)
            {
                lightSource.intensity = Mathf.Lerp(lightSource.intensity, Random.Range(minGrabIntensity, maxGrabIntensity), Time.deltaTime * speed);
            }
            else
            {
                lightSource.intensity = Mathf.Lerp(lightSource.intensity, Random.Range(minIntensity, maxIntensity), Time.deltaTime * speed);
            }
        }

        public void OnGrab()
        {
            isGrabbed = true;
        }

        public void OnRelease()
        {
            isGrabbed = false;
        }
    }
}
