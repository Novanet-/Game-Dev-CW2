using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
	public Transform currentTarget;
	public float travelTime;
	public float cameraDistance;

	void Start()
	{
	}

	// Update is called once per frame
	void Update()
	{
		Vector3 newPosition = new Vector3(currentTarget.transform.position.x, currentTarget.transform.position.y + 6, cameraDistance);
		transform.position = Vector3.Lerp(transform.position, newPosition, 1/travelTime * Time.deltaTime);
//		transform.position = newPosition;
	}
}