using GenericEventSystem;
using UnityEngine;

namespace Audio
{
    public class PlayAudioDuringGameplay : MonoBehaviour
    {
        public AudioSource musicSource;
        public float normalVolume = 0.5f;
        public float fadeOutDuration = 1.0f;
        
        private bool isFading = false;

        // Start is called before the first frame update
        private void Start()
        {
            EventCoordinator.StartListening(EventName.World.GameStateChange(), OnGameStateChange);
        }

        // Update is called once per frame
        void Update()
        {
        
        }

        private void OnGameStateChange(GameMessage msg)
        {
            if (msg.gameState == GameState.CharacterSelection)
            {
                isFading = false;
                musicSource.loop = true;
                musicSource.volume = normalVolume;
                musicSource.Play();
            }
            else
            {
                if(msg.gameState != GameState.BeatRun)
                    StartFadeOut();
            }
        }

        private void StartFadeOut()
        {
            if (!isFading && musicSource != null)
            {
                StartCoroutine(FadeOutRoutine());
            }
        }
        
        private System.Collections.IEnumerator FadeOutRoutine()
        {
            isFading = true;

            var startVolume = musicSource.volume;
            var timer = 0.0f;

            while (isFading && timer < fadeOutDuration)
            {
                timer += Time.deltaTime;
                musicSource.volume = Mathf.Lerp(startVolume, 0f, timer / fadeOutDuration);
                yield return null;
            }

            //In case someone stops the fade
            if (isFading) {
                // Ensure volume is completely faded out
                musicSource.volume = 0f;

                // Stop the audio playback
                musicSource.Stop();

                isFading = false;
            }
        }
    }
}