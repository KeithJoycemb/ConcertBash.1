using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    public class Grab : MonoBehaviour
    {
        public GameObject grabbedObject;
        public Transform grabTransform;
        public float grabRadius = 0.1f;
        public LayerMask grabMask;
        public KeyCode grabKey = KeyCode.Mouse0;
        public LightEffect flicker;

        void Update()
        {
            if (Input.GetKeyDown(grabKey))
            {
                if (grabbedObject == null)
                {
                    TryGrabObject();
                }
                else
                {
                    ReleaseObject();
                }
            }
        }

        void TryGrabObject()
        {
            Collider[] hits = Physics.OverlapSphere(grabTransform.position, grabRadius, grabMask);

            if (hits.Length > 0)
            {
                grabbedObject = hits[0].gameObject;
                flicker.OnGrab();
            }
        }

        void ReleaseObject()
        {
            grabbedObject = null;
            flicker.OnRelease();
        }
    }
}
