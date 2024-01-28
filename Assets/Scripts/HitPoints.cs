using System.Collections.Generic;
using GenericEventSystem;
using UnityEngine;

// Dummy class to easily find from where to throw items
public class HitPoints : MonoBehaviour
{
    public int maxHp = 5;
    public Transform firstHeart;
    public Transform secondHeart;
    int hp = 0;

    List<Transform> hearts = new List<Transform>();

    void Start()
    {
        EventCoordinator.StartListening(EventName.World.GameStateChange(), OnGameStateChange);
        EventCoordinator.StartListening(EventName.Item.Hit(), OnHit);

        hearts.Add(firstHeart);
        var offset = secondHeart.position - firstHeart.position;

        for (int i = 1; i < maxHp; i++)
        {
            var newHeart = Instantiate(firstHeart, firstHeart.position + offset * i, firstHeart.rotation);
            hearts.Add(newHeart);
        }

        SetVisibleHearts(hp);
    }

    void SetVisibleHearts(int count)
    {
        for (int i = 0; i < hearts.Count; i++)
        {
            hearts[i].gameObject.SetActive(i < count);
        }
    }

    void OnGameStateChange(GameMessage msg)
    {
        if (msg.gameState == GameState.BeatRun)
        {
            hp = maxHp;
        }
        else
        {
            hp = 0;
        }
        SetVisibleHearts(hp);
    }

    void OnHit(GameMessage msg)
    {
        if (hp > 0)
        {
            hp--;
            if (hp == 0)
            {
                EventCoordinator.TriggerEvent(EventName.World.GameStateChange(), new GameMessage().WithNewGameState(GameState.PostLevelLose));
            }
        }
        SetVisibleHearts(hp);
    }

}
