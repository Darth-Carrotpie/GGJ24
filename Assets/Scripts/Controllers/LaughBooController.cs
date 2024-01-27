using GenericEventSystem;
using UnityEngine;
using UnityEngine.Audio;

public class LaughBooController : MonoBehaviour
{
    //public static LaughBooController instance;

    enum LaughBooLevel { VeryFunny = 0, Funny = 1, Boring = 2, NotFunny = 3, VeryNotFunny = 4, Silent = 5 };
    LaughBooLevel laughBooLevel;

    [SerializeField] AudioSource[] mainAudioSourceArray;
    [SerializeField] AudioClip[] mainLaughArray;
    [SerializeField] AudioMixer audioMixer;
    [SerializeField] AudioMixerSnapshot[] snapshots;
    [SerializeField] float[] weights;
    [SerializeField] bool[] isTrackPlayingArray = new bool[5];

    [Range(0, 1f)]
    [SerializeField] float laughBooSlider = 0.5f;

    [SerializeField] AudioMixerGroup master;
    [SerializeField] float timeToTransition = 0.5f;
    int crowdReactionValue = 10;

    private void Awake()
    {
        for (int i = 0; i < mainAudioSourceArray.Length; i++)
        {
            mainAudioSourceArray[i] = gameObject.AddComponent<AudioSource>();
            //mainAudioSourceArray[i].Stop();
        }
    }

    private void Start()
    {
        EventCoordinator.StartListening(EventName.World.CrowdStateChange(), GetReactionValue);

        //if(instance == null)
        //    instance = this;



        for (int i = 0; i < isTrackPlayingArray.Length; i++)
        {
            isTrackPlayingArray[i] = false;
        }

        weights[5] = 1f;
        audioMixer.TransitionToSnapshots(snapshots, weights, 0);

        //snapshots[2].TransitionTo(timeToTransition);
        //crowdReactionValue = (int)LaughBooLevel.Boring;

        //audioMixer.SetFloat("MasterVolume", Mathf.Log10(1) * 20);
    }

    //production
    private void Update()
    {
        if (crowdReactionValue == (int)LaughBooLevel.VeryFunny && !isTrackPlayingArray[0])
        {
            ResetWeightsAndBools();
            isTrackPlayingArray[0] = true;
            weights[0] = 1f;
            audioMixer.TransitionToSnapshots(snapshots, weights, timeToTransition);
        }
        else if (crowdReactionValue == (int)LaughBooLevel.Funny && !isTrackPlayingArray[1])
        {
            ResetWeightsAndBools();
            isTrackPlayingArray[1] = true;
            weights[1] = 1f;
            audioMixer.TransitionToSnapshots(snapshots, weights, timeToTransition);
        }
        else if (crowdReactionValue == (int)LaughBooLevel.Boring && !isTrackPlayingArray[2])
        {
            ResetWeightsAndBools();
            isTrackPlayingArray[2] = true;
            weights[2] = 1f;
            audioMixer.TransitionToSnapshots(snapshots, weights, timeToTransition);
        }
        else if (crowdReactionValue == (int)LaughBooLevel.NotFunny && !isTrackPlayingArray[3])
        {
            ResetWeightsAndBools();
            isTrackPlayingArray[3] = true;
            weights[3] = 1f;
            audioMixer.TransitionToSnapshots(snapshots, weights, timeToTransition);
        }
        else if (crowdReactionValue == (int)LaughBooLevel.VeryNotFunny && !isTrackPlayingArray[4])
        {
            ResetWeightsAndBools();
            isTrackPlayingArray[4] = true;
            weights[4] = 1f;
            audioMixer.TransitionToSnapshots(snapshots, weights, timeToTransition);
        }
        
    }

    //For Testing
    //void Update()
    //{

    //    if (laughBooSlider > 0.8f)
    //        laughBooLevel = LaughBooLevel.VeryFunny;
    //    else if (laughBooSlider < 0.8f && laughBooSlider > 0.6f)
    //        laughBooLevel = LaughBooLevel.Funny;
    //    else if (laughBooSlider < 0.6f && laughBooSlider > 0.4f)
    //        laughBooLevel = LaughBooLevel.Boring;
    //    else if (laughBooSlider < 0.4f && laughBooSlider > 0.2f)
    //        laughBooLevel = LaughBooLevel.NotFunny;
    //    else if (laughBooSlider < 0.2f)
    //        laughBooLevel = LaughBooLevel.VeryNotFunny;

    //    if (laughBooLevel == LaughBooLevel.VeryFunny && !isTrackPlayingArray[0])
    //    {
    //        ResetWeightsAndBools();
    //        isTrackPlayingArray[0] = true;
    //        //snapshots[0].TransitionTo(timeToTransition);
    //        weights[0] = 1f;
    //        audioMixer.TransitionToSnapshots(snapshots, weights, timeToTransition);
    //    }
    //    else if (laughBooLevel == LaughBooLevel.Funny && !isTrackPlayingArray[1])
    //    {
    //        ResetWeightsAndBools();
    //        isTrackPlayingArray[1] = true;
    //        //snapshots[1].TransitionTo(timeToTransition);
    //        weights[1] = 1f;
    //        audioMixer.TransitionToSnapshots(snapshots, weights, timeToTransition);
    //    }
    //    else if (laughBooLevel == LaughBooLevel.Boring && !isTrackPlayingArray[2])
    //    {
    //        ResetWeightsAndBools();
    //        isTrackPlayingArray[2] = true;
    //        //snapshots[2].TransitionTo(timeToTransition);
    //        weights[2] = 1f;
    //        audioMixer.TransitionToSnapshots(snapshots, weights, timeToTransition);
    //    }
    //    else if (laughBooLevel == LaughBooLevel.NotFunny && !isTrackPlayingArray[3])
    //    {
    //        ResetWeightsAndBools();
    //        isTrackPlayingArray[3] = true;
    //        //snapshots[3].TransitionTo(timeToTransition);
    //        weights[3] = 1f;
    //        audioMixer.TransitionToSnapshots(snapshots, weights, timeToTransition);
    //    }
    //    else if (laughBooLevel == LaughBooLevel.VeryNotFunny && !isTrackPlayingArray[4])
    //    {
    //        ResetWeightsAndBools();
    //        isTrackPlayingArray[4] = true;
    //        //snapshots[4].TransitionTo(timeToTransition);
    //        weights[4] = 1f;
    //        audioMixer.TransitionToSnapshots(snapshots, weights, timeToTransition);
    //    }
    //}

    void ResetWeightsAndBools()
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

    void GetReactionValue(GameMessage msg)
    {
        crowdReactionValue = msg.intMessage;
        Debug.Log(msg.intMessage);
    }
}
