using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightController : MonoBehaviour
{
    [SerializeField] private Transform _orbitCentre;
    [SerializeField] private int _daySpeedMultiplier;

    private float _timeElapsed;

    private float _arc;

    // Use this for initialization
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        float angle = _daySpeedMultiplier * Time.deltaTime;
        transform.RotateAround(_orbitCentre.position, Vector3.down, angle);


        _arc += angle;

        if (_arc > 180)
        {
            GetComponent<Light>().intensity = 0;
            Debug.Log(string.Format("Arc = {0}", _arc));

        }
        if (_arc > 360)
        {
            _arc = 0;
            GetComponent<Light>().intensity = 1;
            Debug.Log(string.Format("Arc = {0}", _arc));
            Debug.Log(string.Format("Time for full rotation = {0}", _timeElapsed));
            //28666
            _timeElapsed = 0;
        }

        _timeElapsed++;

    }
}