using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeatCube : MonoBehaviour
{
    public float myOffset = 0f;
    public ForwardBeat beat;
    public GameObject myCube;

    public void Hide()
    {
        myCube.SetActive(false);
    }
}
