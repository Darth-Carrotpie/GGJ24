using GenericEventSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class PosterSpotlightController : MonoBehaviour
{
    Light2D light;

    float counter;
    bool doLerp;

    void Start()
    {
        light = GetComponent<Light2D>();
        EventCoordinator.StartListening(EventName.World.GameStateChange(), OnStateChange);
    }

    void OnStateChange(GameMessage msg)
    {
        if (msg.gameState == GameState.BeatRun)
        {
            doLerp = true;
        }
    }
    private void Update()
    {
        if (!doLerp)
            return;
        counter += Time.deltaTime * 1.5f;
        if(counter > 1f)
        {
            this.enabled = false;
        }
        light.intensity = Mathf.Lerp(1, 0.2f, counter);
    }
}
