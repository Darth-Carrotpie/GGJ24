using GenericEventSystem;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

namespace Audio
{
    public class CharacterSoundSpawner : MonoBehaviour
    {
        [FormerlySerializedAs("characterName")] public string characterFolderName;
        public AudioSource airySource;
        public AudioSource openSource;
        public AudioSource closedSource;
        public AudioSource sharpSource;
        private AudioClip[][][] clips;
    
        // Start is called before the first frame update
        void Start()
        {
            //Listen to event
            EventCoordinator.StartListening(EventName.Beats.BeatHitResult(), OnBeatHit);
            clips = new AudioClip[3][][];

            var miniClips = new AudioClip[4][];
            var miniAiryClips = new AudioClip[4];
            miniAiryClips[0] = Resources.Load<AudioClip>($"Audio/Characters/{characterFolderName}/Mini/Airy/1");
            miniAiryClips[1] = Resources.Load<AudioClip>($"Audio/Characters/{characterFolderName}/Mini/Airy/2");
            miniAiryClips[2] = Resources.Load<AudioClip>($"Audio/Characters/{characterFolderName}/Mini/Airy/3");
            miniAiryClips[3] = Resources.Load<AudioClip>($"Audio/Characters/{characterFolderName}/Mini/Airy/4");
            miniClips[0] = miniAiryClips;
        
            var miniClosedClips = new AudioClip[4];
            miniClosedClips[0] = Resources.Load<AudioClip>($"Audio/Characters/{characterFolderName}/Mini/Closed/1");
            miniClosedClips[1] = Resources.Load<AudioClip>($"Audio/Characters/{characterFolderName}/Mini/Closed/2");
            miniClosedClips[2] = Resources.Load<AudioClip>($"Audio/Characters/{characterFolderName}/Mini/Closed/3");
            miniClosedClips[3] = Resources.Load<AudioClip>($"Audio/Characters/{characterFolderName}/Mini/Closed/4");
            miniClips[1] = miniClosedClips;
        
            var miniOpenClips = new AudioClip[4];
            miniOpenClips[0] = Resources.Load<AudioClip>($"Audio/Characters/{characterFolderName}/Mini/Open/1");
            miniOpenClips[1] = Resources.Load<AudioClip>($"Audio/Characters/{characterFolderName}/Mini/Open/2");
            miniOpenClips[2] = Resources.Load<AudioClip>($"Audio/Characters/{characterFolderName}/Mini/Open/3");
            miniOpenClips[3] = Resources.Load<AudioClip>($"Audio/Characters/{characterFolderName}/Mini/Open/4");
            miniClips[2] = miniOpenClips;
        
            var miniSharpClips = new AudioClip[4];
            miniSharpClips[0] = Resources.Load<AudioClip>($"Audio/Characters/{characterFolderName}/Mini/Sharp/1");
            miniSharpClips[1] = Resources.Load<AudioClip>($"Audio/Characters/{characterFolderName}/Mini/Sharp/2");
            miniSharpClips[2] = Resources.Load<AudioClip>($"Audio/Characters/{characterFolderName}/Mini/Sharp/3");
            miniSharpClips[3] = Resources.Load<AudioClip>($"Audio/Characters/{characterFolderName}/Mini/Sharp/4");
            miniClips[3] = miniSharpClips;
        
            clips[0] = miniClips;
        
            var mediumClips = new AudioClip[4][];
            var mediumAiryClips = new AudioClip[4];
            mediumAiryClips[0] = Resources.Load<AudioClip>($"Audio/Characters/{characterFolderName}/Medium/Airy/1");
            mediumAiryClips[1] = Resources.Load<AudioClip>($"Audio/Characters/{characterFolderName}/Medium/Airy/2");
            mediumAiryClips[2] = Resources.Load<AudioClip>($"Audio/Characters/{characterFolderName}/Medium/Airy/3");
            mediumAiryClips[3] = Resources.Load<AudioClip>($"Audio/Characters/{characterFolderName}/Medium/Airy/4");
            mediumClips[0] = mediumAiryClips;
        
            var mediumClosedClips = new AudioClip[4];
            mediumClosedClips[0] = Resources.Load<AudioClip>($"Audio/Characters/{characterFolderName}/Medium/Closed/1");
            mediumClosedClips[1] = Resources.Load<AudioClip>($"Audio/Characters/{characterFolderName}/Medium/Closed/2");
            mediumClosedClips[2] = Resources.Load<AudioClip>($"Audio/Characters/{characterFolderName}/Medium/Closed/3");
            mediumClosedClips[3] = Resources.Load<AudioClip>($"Audio/Characters/{characterFolderName}/Medium/Closed/4");
            mediumClips[1] = mediumClosedClips;
        
            var mediumOpenClips = new AudioClip[4];
            mediumOpenClips[0] = Resources.Load<AudioClip>($"Audio/Characters/{characterFolderName}/Medium/Open/1");
            mediumOpenClips[1] = Resources.Load<AudioClip>($"Audio/Characters/{characterFolderName}/Medium/Open/2");
            mediumOpenClips[2] = Resources.Load<AudioClip>($"Audio/Characters/{characterFolderName}/Medium/Open/3");
            mediumOpenClips[3] = Resources.Load<AudioClip>($"Audio/Characters/{characterFolderName}/Medium/Open/4");
            mediumClips[2] = mediumOpenClips;
        
            var mediumSharpClips = new AudioClip[4];
            mediumSharpClips[0] = Resources.Load<AudioClip>($"Audio/Characters/{characterFolderName}/Medium/Sharp/1");
            mediumSharpClips[1] = Resources.Load<AudioClip>($"Audio/Characters/{characterFolderName}/Medium/Sharp/2");
            mediumSharpClips[2] = Resources.Load<AudioClip>($"Audio/Characters/{characterFolderName}/Medium/Sharp/3");
            mediumSharpClips[3] = Resources.Load<AudioClip>($"Audio/Characters/{characterFolderName}/Medium/Sharp/4");
            mediumClips[3] = mediumSharpClips;
        
            clips[1] = mediumClips;
        
            var longClips = new AudioClip[4][];
            var longAiryClips = new AudioClip[4];
            longAiryClips[0] = Resources.Load<AudioClip>($"Audio/Characters/{characterFolderName}/Long/Airy/1");
            longAiryClips[1] = Resources.Load<AudioClip>($"Audio/Characters/{characterFolderName}/Long/Airy/2");
            longAiryClips[2] = Resources.Load<AudioClip>($"Audio/Characters/{characterFolderName}/Long/Airy/3");
            longAiryClips[3] = Resources.Load<AudioClip>($"Audio/Characters/{characterFolderName}/Long/Airy/4");
            longClips[0] = longAiryClips;
        
            var longClosedClips = new AudioClip[4];
            longClosedClips[0] = Resources.Load<AudioClip>($"Audio/Characters/{characterFolderName}/Long/Closed/1");
            longClosedClips[1] = Resources.Load<AudioClip>($"Audio/Characters/{characterFolderName}/Long/Closed/2");
            longClosedClips[2] = Resources.Load<AudioClip>($"Audio/Characters/{characterFolderName}/Long/Closed/3");
            longClosedClips[3] = Resources.Load<AudioClip>($"Audio/Characters/{characterFolderName}/Long/Closed/4");
            longClips[1] = longClosedClips;
        
            var longOpenClips = new AudioClip[4];
            longOpenClips[0] = Resources.Load<AudioClip>($"Audio/Characters/{characterFolderName}/Long/Open/1");
            longOpenClips[1] = Resources.Load<AudioClip>($"Audio/Characters/{characterFolderName}/Long/Open/2");
            longOpenClips[2] = Resources.Load<AudioClip>($"Audio/Characters/{characterFolderName}/Long/Open/3");
            longOpenClips[3] = Resources.Load<AudioClip>($"Audio/Characters/{characterFolderName}/Long/Open/4");
            longClips[2] = longOpenClips;
        
            var longSharpClips = new AudioClip[4];
            longSharpClips[0] = Resources.Load<AudioClip>($"Audio/Characters/{characterFolderName}/Long/Sharp/1");
            longSharpClips[1] = Resources.Load<AudioClip>($"Audio/Characters/{characterFolderName}/Long/Sharp/2");
            longSharpClips[2] = Resources.Load<AudioClip>($"Audio/Characters/{characterFolderName}/Long/Sharp/3");
            longSharpClips[3] = Resources.Load<AudioClip>($"Audio/Characters/{characterFolderName}/Long/Sharp/4");
            longClips[3] = longSharpClips;
        
            clips[2] = longClips;
        
        
        }

        // Update is called once per frame
        void Update()
        {
        
        }

        // Before everything is created
        private void Awake()
        {
        
        }

        void OnBeatHit(GameMessage msg)
        {
            int lengthSection = 0;
            switch (msg.strMessage)
            {
                case "mini":
                    lengthSection = 0;
                    break;
                case "medium":
                    lengthSection = 1;
                    break;
                case "long":
                    lengthSection = 2;
                    break;
            }

            int typeSection = 0;
            AudioSource sourceToPlay = airySource;
            switch (msg.fBeatType)
            {
                case ForwardBeatType.EverydayLife:
                    typeSection = 0;
                    sourceToPlay = airySource;
                    break;
                case ForwardBeatType.SelfDeprecation:
                    typeSection = 1;
                    sourceToPlay = closedSource;
                    break;
                case ForwardBeatType.SocialComementary:
                    typeSection = 2;
                    sourceToPlay = openSource;
                    break;
                case ForwardBeatType.ObservationalHumor:
                    typeSection = 3;
                    sourceToPlay = sharpSource;
                    break;
            }

        
            sourceToPlay.clip = clips[lengthSection][typeSection][Random.Range(0, 4)];
            sourceToPlay.Play();
        
        }
    }
}