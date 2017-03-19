using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightController : MonoBehaviour
{
    [SerializeField] private GameObject orbitCentre;

    private OrbitController orbitController;

    // Use this for initialization
    void Start()
    {
        orbitController = orbitCentre.GetComponent<OrbitController>();
    }

    // Update is called once per frame
    void Update()
    {
        float orbitArc = orbitController.OrbitArc;
        if (orbitArc >= 180)
        {
            GetComponent<Light>().intensity = 0;
            Debug.Log(string.Format("OrbitArc = {0}", orbitArc));

        }
        if (orbitArc >= 359)
        {
            GetComponent<Light>().intensity = 1;
            Debug.Log(string.Format("OrbitArc = {0}", orbitArc));
            //28666
        }


    }
}