using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(AudioSource))]
public class AudioPeer : MonoBehaviour
{

    AudioSource _audioSource;
    public static float[] _samples = new float[512];
    float[] freqBand = new float[8];
    float[] bandBuffer = new float[8];
    float[] bufferDecrease = new float[8];

    float[] freqBandHighest = new float[8];
    public static float[] audioBand = new float[8];
    public static float[] audioBandBuffer = new float[8];

    public static float amplitude, amplitudeBuffer;
    float amiplitudeHighest;
    public float audioProfile;

    // Start is called before the first frame update
    void Start()
    {
        _audioSource = GetComponent<AudioSource>();
        AudioProfile(audioProfile);
    }

    // Update is called once per frame
    void Update()
    {
        GetSpectrumAudioSource();
        MakeFrequencyBands();
        BandBuffer();
        CreateAudioBands();
        GetAmplitude();
    }

    void AudioProfile(float audioProfile)
    {
        for (int i = 0; i < 8; i++)
        {
            freqBandHighest[i] = audioProfile;
        }
    }

    void GetAmplitude()
    {
        float currentAmplitude = 0;
        float currentAmplitudeBuffer = 0;
        for (int i = 0; i < 8; i++)
        {
            currentAmplitude += audioBand[i];
            currentAmplitudeBuffer += audioBandBuffer[i];
        }

        if(currentAmplitude>amiplitudeHighest)
        {
            amiplitudeHighest = currentAmplitude;
        }
        amiplitudeHighest = currentAmplitude / amiplitudeHighest;
        amplitudeBuffer = currentAmplitudeBuffer / amiplitudeHighest;
    }
    void CreateAudioBands()
    {
        for(int i=0;i<8; i++)
        {
            if (freqBand[i] > freqBandHighest[i])
            {
                freqBandHighest[i] = freqBand[i];
            }
            audioBand[i] = (freqBand[i] / freqBandHighest[i]);
            audioBandBuffer[i] = (bandBuffer[i] / freqBandHighest[i]);

            //Color _tempDiffuseColor = new Color(color.r * AudioPeer.audioBand[i], color.g * AudioPeer.audioBand[i], color.b * AudioPeer.audioBand[i], 1);

            //material.SetColor("_Color", _tempDiffuseColor);
        }
    }

    void GetSpectrumAudioSource()
    {
        _audioSource.GetSpectrumData(_samples, 0,FFTWindow.Blackman);
    }

    void BandBuffer()
    {
        for (int g=0; g<8; g++)
        {
            if(freqBand[g]>bandBuffer[g])
            {
                bandBuffer[g] = freqBand[g];
                bufferDecrease[g] = 0.005f;
            }
            if (freqBand[g] < bandBuffer[g])
            {
                bandBuffer[g] -= bufferDecrease[g];
                bufferDecrease[g] *= 1.2f;

            }

            
        }
    }

    void MakeFrequencyBands()
    {
        int count = 0;

        for (int i = 0;i<8;i++)
        {
            float average = 0;
            int sampleCount = (int)Mathf.Pow(2, i) * 2;

            if (i == 7)
            {
                sampleCount += 2;
            }
            for(int j = 0;j<sampleCount;j++)
            {
                average += _samples[count] * (count + 1);
                count++;
            }
            average /= count;

            freqBand[i] = average * 10;
        }

    }
}
