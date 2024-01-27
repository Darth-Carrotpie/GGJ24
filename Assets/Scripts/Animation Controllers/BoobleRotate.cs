using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoobleRotate : MonoBehaviour
{
    float rotSpeed;
    float currentRot;

    private void Start()
    {
        rotSpeed = Random.Range(-30f, 30f);
    }

    void Update()
    {
        currentRot += Time.deltaTime * rotSpeed;
        transform.eulerAngles = Vector3.forward * currentRot;
    }
}
