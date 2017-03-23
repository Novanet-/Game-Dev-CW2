using System;
using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

[System.Serializable]
public class Hooks {
	public GameObject camera;
	public GameObject goatSmall;
	public GameObject goatMed;
	public GameObject goatLarge;
	public GameObject troll;
}

public class GameController : MonoBehaviour {

	public float gravityStrength;
	public Hooks hooks = new Hooks();
	private PlayerMovementController[] goatControllerArray;
	private int currentGoatIndex = 0;
    private float _timeStart
        ;

    [NotNull] private PlayerMovementController currentGoat { get { return goatControllerArray[currentGoatIndex]; } }

	void Start () {
		Physics.gravity = new Vector3(0f, -gravityStrength, 0f);
		goatControllerArray = new PlayerMovementController[] {
			hooks.goatSmall.GetComponent<PlayerMovementController>(),
			hooks.goatMed.GetComponent<PlayerMovementController>(),
			hooks.goatLarge.GetComponent<PlayerMovementController>() };
		currentGoat.isActivePlayer = true;

	    _timeStart = Time.time;
	}

	void Update() {
		if (Input.GetKeyDown(KeyCode.E)) {
			Debug.Log("E Pressed.");
			ChangeCurrentTarget(true);
		} else if (Input.GetKeyDown(KeyCode.Q)) {
			Debug.Log("Q Pressed.");
			ChangeCurrentTarget(false);
		}
		if (Input.GetKeyDown(KeyCode.F)) {
			ToggleFollowing();
		}

	    if (Math.Abs((Time.time - _timeStart) % 5) < 0.1)
	    {
	        Debug.Log("Current Goat = " + currentGoat.gameObject.name + ", Is Flying = " + currentGoat.IsFlying + ", Is Wall Climbing = " + currentGoat.IsWallClimbing);
	    }
	}

	void ChangeCurrentTarget(bool isRight) {
		int newIndex = (isRight ? currentGoatIndex + 1 : currentGoatIndex + 2) % goatControllerArray.Length;
		SetCurrentTarget(newIndex);
	}

	void SetCurrentTarget(int newIndex) {
		currentGoat.isActivePlayer = false;
		currentGoatIndex = newIndex;
		currentGoat.isActivePlayer = true;
		hooks.camera.GetComponent<CameraController>().currentTarget = currentGoat.transform;
		Debug.Log(goatControllerArray[0].isActivePlayer + " " + goatControllerArray[1].isActivePlayer + " " + goatControllerArray[2].isActivePlayer);
	}

	void ToggleFollowing() {

	}


}