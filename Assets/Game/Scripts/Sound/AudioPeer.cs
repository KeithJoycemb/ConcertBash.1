using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(AudioSource))]
public class AudioPeer : MonoBehaviour
{

    AudioSource _audioSource;
    public static float[] _samplesLeft = new float[512];
    public static float[] _samplesRight = new float[512];
    private float[] freqBand = new float[8];
    private float[] bandBuffer = new float[8];
    private float[] bufferDecrease = new float[8];

    private float[] freqBandHighest = new float[8];

    //audio 64
    private float[] freqBand64 = new float[64];
    private float[] bandBuffer64 = new float[64];
    private float[] bufferDecrease64 = new float[64];
    private float[] freqBandHighest64 = new float[64];
    public float[] audioBand64;
    public float[] audioBandBuffer64;


    public static float[] audioBand;
    public static float[] audioBandBuffer;

    public static float amplitude, amplitudeBuffer;
    private float amiplitudeHighest;
    public float audioProfile;

    public enum _channel { Stereo, Left, Right };
    public _channel channel = new _channel();


    // Start is called before the first frame update
    void Start()
    {
        _audioSource = GetComponent<AudioSource>();
        AudioProfile(audioProfile);
        audioBand = new float[8];
        audioBandBuffer = new float[8];
        audioBand64 = new float[64];
        audioBandBuffer64 = new float[64];
    }

    // Update is called once per frame
    void Update()
    {
        GetSpectrumAudioSource();
        MakeFrequencyBands();
        BandBuffer();
        CreateAudioBands();
        GetAmplitude();
        MakeFrequencyBands64();
        BandBuffer64();
        CreateAudioBands64();
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

           
        }
    }

    void GetSpectrumAudioSource()
    {
        _audioSource.GetSpectrumData(_samplesLeft, 0,FFTWindow.Blackman);
        _audioSource.GetSpectrumData(_samplesRight, 1, FFTWindow.Blackman);
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
                if(channel== _channel.Stereo)
                { 
                average += _samplesLeft[count] + _samplesRight[count] * (count + 1);
                }
                if (channel == _channel.Left)
                {
                    average += _samplesLeft[count]  * (count + 1);
                }
                if (channel == _channel.Right)
                {
                    average +=  _samplesRight[count] * (count + 1);
                }
                count++;
            }
            average /= count;

            freqBand[i] = average * 10;
        }

    }

    void BandBuffer64()
    {
        for (int g = 0; g < 8; g++)
        {
            if (freqBand64[g] > bandBuffer64[g])
            {
                bandBuffer64[g] = freqBand64[g];
                bufferDecrease64[g] = 0.005f;
            }
            if (freqBand64[g] < bandBuffer64[g])
            {
                bandBuffer64[g] -= bufferDecrease64[g];
                bufferDecrease64[g] *= 1.2f;

            }


        }
    }

    void CreateAudioBands64()
    {
        for (int i = 0; i < 64; i++)
        {
            if (freqBand64[i] > freqBandHighest64[i])
            {
                freqBandHighest64[i] = freqBand64[i];
            }
            audioBand64[i] = (freqBand64[i] / freqBandHighest64[i]);
            audioBandBuffer64[i] = (bandBuffer64[i] / freqBandHighest64[i]);


        }
    }
        void MakeFrequencyBands64()
    {
        int count = 0;
        int sampleCount = 1;
        int power = 0;

        for (int i = 0; i < 64; i++)
        {

            
           
            float average = 0;
           
            if (i == 16 ||i==32 ||i==40 ||i==48 ||i==56)
            {
                power++;
                sampleCount = (int)Mathf.Pow(2, power);
                if (power ==3)
                {
                    sampleCount -= 2;
                }
            }
            for (int j = 0; j < sampleCount; j++)
            {
                if (channel == _channel.Stereo)
                {
                    average += _samplesLeft[count] + _samplesRight[count] * (count + 1);
                }
                if (channel == _channel.Left)
                {
                    average += _samplesLeft[count] * (count + 1);
                }
                if (channel == _channel.Right)
                {
                    average += _samplesRight[count] * (count + 1);
                }
                count++;
            }
            average /= count;

            freqBand64[i] = average * 80;
        }

    }
}
