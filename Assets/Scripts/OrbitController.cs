using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrbitController : MonoBehaviour {

    private Vector3 vOffset;
    private float timestamp;
    [SerializeField] private float _timeForFullRotation;
    [HideInInspector] public float OrbitArc { get; set; }

    void Start()
    {
        vOffset = transform.position;
        timestamp = Time.time;
    }

    void Update()
    {
        float elapsedTime = (Time.time - timestamp) % _timeForFullRotation;
        OrbitArc = elapsedTime / _timeForFullRotation * 360.0f;

        float angle = elapsedTime / _timeForFullRotation * 360.0f;
        Quaternion q = Quaternion.AngleAxis(angle, Vector3.down);
        transform.position = q * vOffset;
        transform.rotation = q;

        Debug.Log(elapsedTime);
    }
}
