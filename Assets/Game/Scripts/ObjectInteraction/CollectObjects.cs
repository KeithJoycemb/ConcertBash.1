using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
public class CollectObjects : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        // Check if the object has a Collectable script
        Collectable collectable = other.gameObject.GetComponent<Collectable>();
        if (collectable != null)
        {
            // Handle the object being collected
            collectable.Collect();
        }
    }
}
}

