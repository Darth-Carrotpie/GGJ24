using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LaughBooController : MonoBehaviour
{
    [SerializeField] AudioSource audioSource;
    [SerializeField] AudioClip[] mainLaughArray;

    [Range(0, 1f)]
    [SerializeField] float laughBooSlider = 0.5f;

    private bool changeTrack = false;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    void Start()
    {
        //audioSource = GetComponent<AudioSource>();

        //audioSource.clip = mainLaughArray[2];
        //audioSource.Play();

        //audioSource.clip = boringLaughArray[Random.Range(0, boringLaughArray.Length)];
    }

    void Update()
    {

        if (laughBooSlider > 0.8f && audioSource.clip != mainLaughArray[0])
        {
            audioSource.Stop();
            audioSource.clip = mainLaughArray[0];
            audioSource.Play();
        }
        else if (laughBooSlider < 0.8f && laughBooSlider > 0.6f && audioSource.clip != mainLaughArray[1])
        {
            audioSource.Stop();
            audioSource.clip = mainLaughArray[1];
            audioSource.Play();
        }
        else if (laughBooSlider < 0.6f && laughBooSlider > 0.4f && audioSource.clip != mainLaughArray[2])
        {
            audioSource.Stop();
            audioSource.clip = mainLaughArray[2];
            audioSource.Play();
        }
        else if (laughBooSlider < 0.4f && laughBooSlider > 0.2f && audioSource.clip != mainLaughArray[3])
        {
            audioSource.Stop();
            audioSource.clip = mainLaughArray[3];
            audioSource.Play();
        }
        else if (laughBooSlider < 0.2f && audioSource.clip != mainLaughArray[4])
        {
            audioSource.Stop();
            audioSource.clip = mainLaughArray[4];
            audioSource.Play();
        }
    }

    //private void AddAudioSource(AudioClip audioClip)
    //{
    //    AudioSource audioSource = new AudioSource();
    //    audioSource.clip = audioClip;
    //    //audioSourceList.Add(audioSource);
    //}

    //private void AudioFadeOut(List<AudioSource> audioSourceList)
    //{
    //    foreach (AudioSource audioSource in audioSourceList)
    //    {

    //    }
    //}
}
