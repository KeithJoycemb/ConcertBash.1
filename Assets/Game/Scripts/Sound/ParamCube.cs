using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParamCube : MonoBehaviour
{

    public int band;
    public float startScale, scaleMultiplier;
    public bool useBuffer;
    Material material;
    public float red, green, blue;





    // Start is called before the first frame update
    void Start()
    {
        material = GetComponent<MeshRenderer>().materials[0];
    }

    // Update is called once per frame
    void Update()
    {
        

        if (useBuffer) 
        {
            transform.localScale = new Vector3(transform.localScale.x, (AudioPeer.audioBandBuffer[band] *scaleMultiplier)+ startScale, transform.localScale.z);
            //Color color = new Color(red*AudioPeer.audioBandBuffer[band], green*AudioPeer.audioBandBuffer[band], blue*AudioPeer.audioBandBuffer[band]);
            //material.SetColor("EmissionColor", color);
            

            Color _color = new Color(red * AudioPeer.amplitudeBuffer, green * AudioPeer.amplitudeBuffer, blue * AudioPeer.amplitudeBuffer);
            material.SetColor("_EmissionColor", _color);
        }
        if (!useBuffer)
        {
            transform.localScale = new Vector3(transform.localScale.x, (AudioPeer.audioBand[band] * scaleMultiplier) + startScale, transform.localScale.z);
            //Color color = new Color(red*AudioPeer.audioBand[band], green*AudioPeer.audioBand[band], blue*AudioPeer.audioBand[band]);


            //material.SetColor("_EmissionColor", color * AudioPeer.audioBand[0]);
            
            Color _color = new Color(red * AudioPeer.amplitude, green * AudioPeer.amplitude, blue * AudioPeer.amplitude);
            material.SetColor("_EmissionColor", _color);


        }
    }
}
