using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class LaughBooController : MonoBehaviour
{
    //public static LaughBooController instance;

    enum LaughBooLevel { VeryFunny = 0, Funny = 1, Boring = 2, NotFunny = 3, VeryNotFunny = 4 };
    LaughBooLevel laughBooLevel;

    [SerializeField] AudioSource[] mainAudioSourceArray;
    [SerializeField] AudioClip[] mainLaughArray;
    [SerializeField] AudioMixer audioMixer;

    [Range(0, 1f)]
    [SerializeField] float laughBooSlider = 0.5f;

    private bool isPlayingTrack01;

    private void Start()
    {
        //if(instance == null)
        //    instance = this;

        //track01 = GetComponent<AudioSource>();
        //track02 = GetComponent<AudioSource>();

        //track01 = gameObject.AddComponent<AudioSource>();
        //track02 = gameObject.AddComponent<AudioSource>();

    
        for (int i = 0; i < mainAudioSourceArray.Length; i++)
        {
            mainAudioSourceArray[i] = gameObject.AddComponent<AudioSource>();
            for (int p = 0; p < mainLaughArray.Length; p++)
            {
                if(i == p)
                    mainAudioSourceArray[i].clip = mainLaughArray[p];
            }   
        }

        audioMixer = GetComponent<AudioMixer>();

        laughBooLevel = LaughBooLevel.Boring;
        
       
    }

    void Update()
    {
        Debug.Log(isPlayingTrack01);

        if (laughBooSlider > 0.8f)
            laughBooLevel = LaughBooLevel.VeryFunny;
        else if (laughBooSlider < 0.8f && laughBooSlider > 0.6f)
            laughBooLevel = LaughBooLevel.Funny;
        else if (laughBooSlider < 0.6f && laughBooSlider > 0.4f)
            laughBooLevel = LaughBooLevel.Boring;
        else if (laughBooSlider < 0.4f && laughBooSlider > 0.2f)
            laughBooLevel = LaughBooLevel.NotFunny;
        else if (laughBooSlider < 0.2f)
            laughBooLevel = LaughBooLevel.VeryNotFunny;

        //if (laughBooLevel == LaughBooLevel.VeryFunny && track01.clip != mainLaughArray[0])
        if (laughBooLevel == LaughBooLevel.VeryFunny)
        {
            //track01.Stop();
            //track01.clip = mainLaughArray[0];
            //track01.Play();
            SwapTrack(mainLaughArray[0]);

        }
        else if (laughBooLevel == LaughBooLevel.Funny && track01.clip != mainLaughArray[1])
        {
            //track01.Stop();
            //track01.clip = mainLaughArray[1];
            //track01.Play();
            SwapTrack(mainLaughArray[1]);
        }
        else if (laughBooLevel == LaughBooLevel.Boring && track01.clip != mainLaughArray[2])
        {
            //track01.Stop();
            //track01.clip = mainLaughArray[2];
            //track01.Play();
            SwapTrack(mainLaughArray[2]);
        }
        else if (laughBooLevel == LaughBooLevel.NotFunny && track01.clip != mainLaughArray[3])
        {
            //track01.Stop();
            //track01.clip = mainLaughArray[3];
            //track01.Play();
            SwapTrack(mainLaughArray[3]);
        }
        else if (laughBooLevel == LaughBooLevel.VeryNotFunny && track01.clip != mainLaughArray[4])
        {
            //track01.Stop();
            //track01.clip = mainLaughArray[4];
            //track01.Play();
            SwapTrack(mainLaughArray[4]);
        }
    }

    public void SwapTrack(AudioClip newAudioClip)
    {
        

        StopAllCoroutines();

        StartCoroutine(FadeTrack(newAudioClip));

        //if (isPlayingTrack01)
        //{
        //    track02.clip = newAudioClip;
        //    track01.Stop();
        //    track02.Play();
            
        //}
        //else
        //{
        //    track01.clip = newAudioClip;
        //    track02.Stop();
        //    track01.Play();
            
        //}

        isPlayingTrack01 = !isPlayingTrack01; 
    }
    
    private IEnumerator FadeTrack(AudioClip newAudioClip)
    {
        float timeToFade = 1.0f;
        float timeElapsed = 0;

        if (isPlayingTrack01)
        {
            //isPlayingTrack01 = !isPlayingTrack01;

            track02.clip = newAudioClip;
            track02.Play();
            

            while(timeElapsed < timeToFade)
            {
                track02.volume = Mathf.Lerp(0, 1, timeElapsed / timeToFade);
                track01.volume = Mathf.Lerp(1, 2, timeElapsed / timeToFade);
                timeElapsed += Time.deltaTime;
                yield return null;
            }

            track01.Stop();

            
        }
        else
        {
            //isPlayingTrack01 = !isPlayingTrack01;

            track01.clip = newAudioClip;
            track01.Play();

            while (timeElapsed < timeToFade)
            {
                track01.volume = Mathf.Lerp(0, 1, timeElapsed / timeToFade);
                track02.volume = Mathf.Lerp(1, 2, timeElapsed / timeToFade);
                timeElapsed += Time.deltaTime;
                yield return null;
            }

            track02.Stop();
        }
    }
}
