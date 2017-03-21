using Assets.Scripts;
using JetBrains.Annotations;
using UnityEngine;

public class GameController : MonoBehaviour
{
    #region Public Fields

    public Hooks Hooks = new Hooks();

    #endregion Public Fields

    #region Private Fields

    private int _currentGoatIndex;
    private PlayerMovementController[] _goatControllerArray;

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
        CurrentGoat.IsActivePlayer = false;
        _currentGoatIndex = newIndex;
        CurrentGoat.IsActivePlayer = true;
        Hooks.Camera.GetComponent<CameraController>().CurrentTarget = CurrentGoat.transform;
        Debug.Log(_goatControllerArray[0].IsActivePlayer + " " + _goatControllerArray[1].IsActivePlayer + " " + _goatControllerArray[2].IsActivePlayer);
    }

    private void Start()
    {
        _goatControllerArray = new[]
        {
            Hooks.GoatSmall.GetComponent<PlayerMovementController>(),
            Hooks.GoatMed.GetComponent<PlayerMovementController>(),
            Hooks.GoatLarge.GetComponent<PlayerMovementController>()
        };
        CurrentGoat.IsActivePlayer = true;
    }

    private void ToggleFollowing()
    {
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            Debug.Log("E Pressed.");
            ChangeCurrentTarget(true);
        }
        else if (Input.GetKeyDown(KeyCode.Q))
        {
            Debug.Log("Q Pressed.");
            ChangeCurrentTarget(false);
        }
        if (Input.GetKeyDown(KeyCode.F))
            ToggleFollowing();
    }

    #endregion Private Methods
}