using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
	public Transform currentTarget;

    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
		Vector3 newPosition = new Vector3(currentTarget.transform.position.x, currentTarget.transform.position.y + 6, transform.position.z);
		transform.position = newPosition;
    }
}