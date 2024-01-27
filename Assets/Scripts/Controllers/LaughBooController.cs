using GenericEventSystem;
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
    [SerializeField] AudioMixerSnapshot[] snapshots;
    [SerializeField] float[] weights;

    [SerializeField] bool[] isTrackPlayingArray = new bool[5];

    [Range(0, 1f)]
    [SerializeField] float laughBooSlider = 0.5f;

    [SerializeField] float timeToTransition = 0.5f;

    private void Start()
    {
        //EventCoordinator.StartListening(EventName.World.CrowdStateChange(), Test);

        //if(instance == null)
        //    instance = this;

        //track01 = GetComponent<AudioSource>();
        //track02 = GetComponent<AudioSource>();

        //track01 = gameObject.AddComponent<AudioSource>();
        //track02 = gameObject.AddComponent<AudioSource>();


        //for (int i = 0; i < mainAudioSourceArray.Length; i++)
        //{
        //    mainAudioSourceArray[i] = gameObject.AddComponent<AudioSource>();
        //    for (int p = 0; p < mainLaughArray.Length; p++)
        //    {
        //        if(i == p)
        //            mainAudioSourceArray[i].clip = mainLaughArray[p];
        //    }   
        //}

        //audioMixer = GetComponent<AudioMixer>();

        for (int i = 0; i < mainAudioSourceArray.Length; i++)
        {
            mainAudioSourceArray[i] = gameObject.AddComponent<AudioSource>();
        }

        for (int i = 0; i < isTrackPlayingArray.Length; i++)
        {
            isTrackPlayingArray[i] = false;
        }

        //audioMixer.TransitionToSnapshots(snapshots, 0.1f, 0.5f);

        snapshots[2].TransitionTo(timeToTransition);
        laughBooLevel = LaughBooLevel.Boring;
        
       
    }

    void Update()
    {

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

        if (laughBooLevel == LaughBooLevel.VeryFunny && !isTrackPlayingArray[0])
        {
            ResetWeights();
            isTrackPlayingArray[0] = true;
            //snapshots[0].TransitionTo(timeToTransition);
            weights[0] = 1f;
            audioMixer.TransitionToSnapshots(snapshots, weights, timeToTransition);
        }
        else if (laughBooLevel == LaughBooLevel.Funny && !isTrackPlayingArray[1])
        {
            ResetWeights();
            isTrackPlayingArray[1] = true;
            //snapshots[1].TransitionTo(timeToTransition);
            weights[1] = 1f;
            audioMixer.TransitionToSnapshots(snapshots, weights, timeToTransition);
        }
        else if (laughBooLevel == LaughBooLevel.Boring && !isTrackPlayingArray[2])
        {
            ResetWeights();
            isTrackPlayingArray[2] = true;
            //snapshots[2].TransitionTo(timeToTransition);
            weights[2] = 1f;
            audioMixer.TransitionToSnapshots(snapshots, weights, timeToTransition);
        }
        else if (laughBooLevel == LaughBooLevel.NotFunny && !isTrackPlayingArray[3])
        {
            ResetWeights();
            isTrackPlayingArray[3] = true;
            //snapshots[3].TransitionTo(timeToTransition);
            weights[3] = 1f;
            audioMixer.TransitionToSnapshots(snapshots, weights, timeToTransition);
        }
        else if (laughBooLevel == LaughBooLevel.VeryNotFunny && !isTrackPlayingArray[4])
        {
            ResetWeights();
            isTrackPlayingArray[4] = true;
            //snapshots[4].TransitionTo(timeToTransition);
            weights[4] = 1f;
            audioMixer.TransitionToSnapshots(snapshots, weights, timeToTransition);
        }
    }

    void ResetWeights()
    {
        for (int i = 0; i < weights.Length; i++)
        {
            weights[i] = 0;
        }

        for (int i = 0; i < isTrackPlayingArray.Length; i++)
        {
            isTrackPlayingArray[i] = false;
        }
    }

    void Test(GameMessage msg)
    {
        Debug.Log(msg.intMessage);
    }

    //public void SwapTrack(AudioClip newAudioClip)
    //{


    //    StopAllCoroutines();

    //    StartCoroutine(FadeTrack(newAudioClip));

    //    //if (isPlayingTrack01)
    //    //{
    //    //    track02.clip = newAudioClip;
    //    //    track01.Stop();
    //    //    track02.Play();

    //    //}
    //    //else
    //    //{
    //    //    track01.clip = newAudioClip;
    //    //    track02.Stop();
    //    //    track01.Play();

    //    //}

    //    isPlayingTrack01 = !isPlayingTrack01; 
    //}

    //private IEnumerator FadeTrack(AudioClip newAudioClip)
    //{
    //    float timeToFade = 1.0f;
    //    float timeElapsed = 0;

    //    if (isPlayingTrack01)
    //    {
    //        //isPlayingTrack01 = !isPlayingTrack01;

    //        track02.clip = newAudioClip;
    //        track02.Play();


    //        while(timeElapsed < timeToFade)
    //        {
    //            track02.volume = Mathf.Lerp(0, 1, timeElapsed / timeToFade);
    //            track01.volume = Mathf.Lerp(1, 2, timeElapsed / timeToFade);
    //            timeElapsed += Time.deltaTime;
    //            yield return null;
    //        }

    //        track01.Stop();


    //    }
    //    else
    //    {
    //        //isPlayingTrack01 = !isPlayingTrack01;

    //        track01.clip = newAudioClip;
    //        track01.Play();

    //        while (timeElapsed < timeToFade)
    //        {
    //            track01.volume = Mathf.Lerp(0, 1, timeElapsed / timeToFade);
    //            track02.volume = Mathf.Lerp(1, 2, timeElapsed / timeToFade);
    //            timeElapsed += Time.deltaTime;
    //            yield return null;
    //        }

    //        track02.Stop();
    //    }
    //}
}
