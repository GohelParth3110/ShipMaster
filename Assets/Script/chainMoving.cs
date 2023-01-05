using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class chainMoving : MonoBehaviour
{
    private float minRotationSpeed = 1.5f;
    private float maxRotationSpeed = 3f;

    private float minAngleToRotate = 8f;
    private float maxAngleToRotate = 15f;

    private float rotationSpeed = 5;
    private float angleTORotate;

    private void Start()
    {
        rotationSpeed = Random.Range(minRotationSpeed, maxRotationSpeed);
        angleTORotate = Random.Range(minAngleToRotate, maxAngleToRotate);
    }

    private void Update()
    {
        transform.rotation = Quaternion.Euler(0f, 0f, angleTORotate * Mathf.Sin(Time.time * rotationSpeed));
    }


}
