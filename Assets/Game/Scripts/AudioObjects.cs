using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioObjects : MonoBehaviour
{
    public string stemName;

   
    public int weight;
    public bool stackable;
    public GameObject item;
   


    public AudioObjects(string name, int weight, bool stackable, GameObject item)
    { 
        
        this.stemName = name;
        this.weight = weight;
        this.stackable = stackable;
        this.item = item;
     

    }
}
