using System.Collections;
using System.Linq;
using GenericEventSystem;
using UnityEngine;

public class ThrowManager : MonoBehaviour
{
    public ItemSet itemPrefabSet;
    static float prepDuration = 1.2f;
    static float throwDuration = 1.0f;
    static float throwDelay = 1.0f;
    Thrower thrower;
    ThrowTarget throwTarget;
    float hate = 0.0f;
    int currentCrowdState = 0;

    // Start is called before the first frame update
    void Start()
    {
        EventCoordinator.StartListening(EventName.Item.Throw(), OnThrow);
        EventCoordinator.StartListening(EventName.World.CrowdStateChange(), OnCrowdStateChanged);

        thrower = FindObjectOfType<Thrower>();
        throwTarget = FindObjectOfType<ThrowTarget>();
    }

    // call if adding a new throw target later
    public void SetThrowTarget(ThrowTarget newTarget)
    {
        throwTarget = newTarget;
    }

    float HateNeededToThrow()
    {
        switch (currentCrowdState)
        {
            case 2:
                return 30.0f;
            case 3:
                return 10.0f;
            case 4:
                return 3.0f;
            default:
                return float.PositiveInfinity;
        }
    }

    void Update()
    {
        hate += Time.deltaTime;
        if (hate > HateNeededToThrow())
        {
            var type = itemPrefabSet.throwItemPrefabs.ElementAt(Random.Range(0, itemPrefabSet.throwItemPrefabs.Length)).type;
            EventCoordinator.TriggerEvent(EventName.Item.Throw(), new GameMessage().WithRBeatType(type));
            hate = 0;
        }
    }

    void OnThrow(GameMessage msg)
    {
        StartCoroutine(ThrowAnimation(msg.rBeatType));
    }

    void OnCrowdStateChanged(GameMessage msg)
    {
        currentCrowdState = msg.intMessage;
    }

    IEnumerator ThrowAnimation(ReverseBeatType type)
    {
        float startTime = Time.time;
        var itemPrefab = itemPrefabSet.throwItemPrefabs.Single(i => i.type == type);

        var itemInstance = Instantiate(itemPrefab.prefab, thrower.transform.position, Quaternion.identity, transform);
        // Detach from thrower if the crowd moves?
        var throwPosition = thrower.transform.position;

        while (true)
        {
            float t = Time.time - startTime;
            float prepT = Mathf.Clamp01(t / prepDuration);
            float throwT = Mathf.Clamp01((t - throwDelay) / throwDuration);

            itemInstance.localScale = Vector3.one * (1 - Mathf.Pow(1 - prepT, 3));
            // Fixed throw start, follows the target?
            itemInstance.position = Vector3.Lerp(throwPosition, throwTarget.transform.position, Mathf.Pow(throwT, 3));

            if (throwT == 1)
            {
                // Show one last frame
                yield return null;

                EventCoordinator.TriggerEvent(EventName.Item.CheckHit(), new GameMessage().WithRBeatType(type));
                Destroy(itemInstance.gameObject);
                break;
            }
            yield return null;
        }
    }
}
