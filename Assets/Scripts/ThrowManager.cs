using System.Collections;
using System.Collections.Generic;
using GenericEventSystem;
using Unity;
using UnityEngine;

public class ThrowManager : MonoBehaviour
{
    public List<Transform> throwItemPrefabs;
    public float prepDuration = 1.5f;
    public float throwDelay = 1.0f;
    public float throwDuration = 2.0f;

    Thrower thrower;
    ThrowTarget throwTarget;

    // Start is called before the first frame update
    void Start()
    {
        EventCoordinator.StartListening(EventName.Item.Throw(), Throw);

        thrower = FindObjectOfType<Thrower>();
        throwTarget = FindObjectOfType<ThrowTarget>();

        // TODO: remove
        StartCoroutine(ThrowLoop());
    }

    void Throw(GameMessage msg)
    {
        StartCoroutine(ThrowAnimation());
    }

    IEnumerator ThrowAnimation()
    {
        float startTime = Time.time;

        var itemPrefab = throwItemPrefabs[Random.Range(0, throwItemPrefabs.Count)];
        var itemInstance = Instantiate(itemPrefab, thrower.transform.position, Quaternion.identity, transform);
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

                EventCoordinator.TriggerEvent(EventName.Item.CheckHit(), new GameMessage());
                Destroy(itemInstance.gameObject);
                break;
            }
            yield return null;
        }

    }

    // TODO: remove
    IEnumerator ThrowLoop()
    {
        yield return new WaitForSeconds(4);

        Throw(new GameMessage());
        StartCoroutine(ThrowLoop());
    }
}