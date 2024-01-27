using GenericEventSystem;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Audio
{
    public class CharacterSoundSpawner : MonoBehaviour
    {
        public string characterFolderName;
        public float pitch = 1.5f;
        public AudioSource airySource;
        public AudioSource openSource;
        public AudioSource closedSource;
        public AudioSource sharpSource;
        private AudioClip[][][] clips;
    
        // Start is called before the first frame update
        private void Start()
        {
            airySource.pitch = pitch;
            openSource.pitch = pitch;
            closedSource.pitch = pitch;
            sharpSource.pitch = pitch;
            
            // Listen to event
            EventCoordinator.StartListening(EventName.Beats.BeatCreated(), OnBeatHit);

            // Load AudioClips
            string[] categories = { "Mini", "Medium", "Long" };
            string[] types = { "Airy", "Closed", "Open", "Sharp" };
            clips = new AudioClip[3][][];

            for (var i = 0; i < categories.Length; i++)
            {
                var categoryClips = new AudioClip[4][];

                for (var j = 0; j < types.Length; j++)
                {
                    var typeClips = new AudioClip[4];

                    for (var k = 0; k < 4; k++)
                    {
                        typeClips[k] = Resources.Load<AudioClip>($"Audio/Characters/{characterFolderName}/{categories[i]}/{types[j]}/{k + 1}");
                    }
                    categoryClips[j] = typeClips;
                }
                clips[i] = categoryClips;
            }
        }

        // Update is called once per frame
        private void Update()
        {
        
        }

        private void OnBeatHit(GameMessage msg)
        {
            var typeSection = GetTypeToPlay(msg.fBeatType);
            var lengthSection = GetLengthSection(msg.fBeat.beatLengthType);
            var sourceToPlay = GetSourceToPlay(msg.fBeatType);

            sourceToPlay.clip = clips[lengthSection][typeSection][Random.Range(0, 4)];
            sourceToPlay.Play();
        }
        
        private static int GetLengthSection(BeatLengthType length)
        {
            return length switch
            {
                BeatLengthType.mini => 0,
                BeatLengthType.medium => 1,
                BeatLengthType.big => 2,
                _ => 0, // Default to mini
            };
        }
        
        private AudioSource GetSourceToPlay(ForwardBeatType type)
        {
            return type switch
            {
                ForwardBeatType.EverydayLife => airySource,
                ForwardBeatType.SelfDeprecation => closedSource,
                ForwardBeatType.SocialComementary => openSource,
                ForwardBeatType.ObservationalHumor => sharpSource,
                _ => null, // Return null for unknown types
            };
        }
        
        private static int GetTypeToPlay(ForwardBeatType type)
        {
            return type switch
            {
                ForwardBeatType.EverydayLife => 0,
                ForwardBeatType.SelfDeprecation => 1,
                ForwardBeatType.SocialComementary => 2,
                ForwardBeatType.ObservationalHumor => 3,
                _ => 0, // Default
            };
        }
    }
}