using System.Collections;
using System.Collections.Generic;
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
        Debug.Log(throwItemPrefabs);
        thrower = FindObjectOfType<Thrower>();
        throwTarget = FindObjectOfType<ThrowTarget>();
        StartCoroutine(ThrowLoop());
    }

    void Throw()
    {
        // TODO: trigger from event;
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

                // TODO: trigger event to dodge or be hit (based on keyboard?)
                Destroy(itemInstance.gameObject);
                break;
            }
            yield return null;
        }

    }

    IEnumerator ThrowLoop()
    {
        yield return new WaitForSeconds(4);

        Throw();
        StartCoroutine(ThrowLoop());
    }
}
