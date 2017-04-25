using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleController : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frames
	void Update () {
		if (Input.anyKeyDown) {
			SceneManager.LoadSceneAsync("BaseScene", LoadSceneMode.Single);
		}
	}
}
