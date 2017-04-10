using System;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.Serialization;

public class GameController : MonoBehaviour
{
	#region Public Fields

	public float GravityStrength;
	public Hooks Hooks = new Hooks();

	#endregion Public Fields

	#region Private Fields

	private int _currentGoatIndex;
	private PlayerMovementController[] _goatControllerArray;
	private float _timeStart;
	private bool _followingEnabled;

	#endregion Private Fields

	#region Private Properties

	[NotNull] private PlayerMovementController CurrentGoat
	{
		get { return _goatControllerArray[_currentGoatIndex]; }
	}

	#endregion Private Properties

	#region Private Methods

	private void ChangeCurrentTarget(bool isRight)
	{
		int newIndex = (isRight ? _currentGoatIndex + 1 : _currentGoatIndex + 2) % _goatControllerArray.Length;
		SetCurrentTarget(newIndex);
	}

	private void SetCurrentTarget(int newIndex)
	{
		DisableFollowing ();
		CurrentGoat.IsActivePlayer = false;
		_currentGoatIndex = newIndex;
		CurrentGoat.IsActivePlayer = true;
		Hooks.Camera.GetComponent<CameraController>().CurrentTarget = CurrentGoat.transform;
		Debug.Log(_goatControllerArray[0].IsActivePlayer + " " + _goatControllerArray[1].IsActivePlayer + " " + _goatControllerArray[2].IsActivePlayer);
		if (_followingEnabled) {
			EnableFollowing ();
		}
	}

	private void Start()
	{
		Physics.gravity = new Vector3(0f, -GravityStrength, 0f);
		_goatControllerArray = new[]
		{
			Hooks.GoatSmall.GetComponent<PlayerMovementController>(),
			Hooks.GoatMed.GetComponent<PlayerMovementController>(),
			Hooks.GoatLarge.GetComponent<PlayerMovementController>()
		};
		CurrentGoat.IsActivePlayer = true;
		EnableFollowing();
		_timeStart = Time.time;
	}

	private void ToggleFollowing()
	{
		_followingEnabled = !_followingEnabled;
		if (_followingEnabled) {
			EnableFollowing();
		} else {
			DisableFollowing ();
		}
	}

	private void EnableFollowing() {
		for (int indexCount = 0; indexCount < _goatControllerArray.Length; indexCount++) {
			if (indexCount != _currentGoatIndex) {
				_goatControllerArray[indexCount].EnableFollowing(CurrentGoat);
			}
		}
	}

	private void DisableFollowing() {
		for (int indexCount = 0; indexCount < _goatControllerArray.Length; indexCount++) {
			if (indexCount != _currentGoatIndex) {
				_goatControllerArray[indexCount].DisableFollowing();
			}
		}
	}

	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.E))
		{
			ChangeCurrentTarget(true);
		}
		else if (Input.GetKeyDown(KeyCode.Q))
		{
			ChangeCurrentTarget(false);
		}
		if (Input.GetKeyDown(KeyCode.F))
		{
			ToggleFollowing();
		}
		if (Math.Abs((Time.time - _timeStart) % 5) < 0.1)
		{
			Debug.Log("Current Goat = " + CurrentGoat.gameObject.name + ", Is Flying = " + CurrentGoat.IsFlying + ", Is Wall Climbing = " +
					  CurrentGoat.IsWallClimbing);
		}
	}

	#endregion Private Methods
}

[Serializable]
public class Hooks
{
	#region Public Fields

	public GameObject Camera;
	public GameObject GoatLarge;
	public GameObject GoatMed;
	public GameObject GoatSmall;
	public GameObject Troll;

	#endregion Public Fields
}