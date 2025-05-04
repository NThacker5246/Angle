using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MSCamera : MonoBehaviour
{
    public Transform player;
    public float smoothTime = 0f;
    public float rx;
    public float ry;
    public float rz;
    public float px;
    public float py;
    public float pz;
    private Vector3 velocity = Vector3.zero;

    void Update(){
        Vector3 targetPosition = player.position + new Vector3(px, py, pz);
        transform.position = targetPosition;
        transform.eulerAngles = player.eulerAngles;
    }

/*
    void LateUpdate()
    {
        Vector3 targetRotation = player.eulerAngles + new Vector3(rx, ry, rz);
        transform.eulerAngles = Vector3.SmoothDamp(transform.eulerAngles, targetRotation, ref velocity, smoothTime);
    }
*/
}
