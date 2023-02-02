using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Selections : MonoBehaviour
{
    public List<AudioObjects> items = new List<AudioObjects>();
    public int capacity = 20;

    public void AddAudioObjects(AudioObjects item)
    {
        if (items.Count >= capacity)
        {
            // Inventory is full
            return;
        }
        items.Add(item);
    }

    public void RemoveAudioObjects(AudioObjects item)
    {
        items.Remove(item);
    }

    public bool HasAudioObjects(AudioObjects item)
    {
        return items.Contains(item);
    }
    
    public int NumberOfAudioObjectss()
    {
        return items.Capacity;
    }
}
