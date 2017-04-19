using Backend;
using UnityEngine;

namespace Frontend
{
    public class LightController : MonoBehaviour
    {
        #region Private Fields

		[SerializeField] private float _daylength;
		private Light[] _lights;
		private float _timeOfDay;

        #endregion Private Fields

        #region Private Methods

        // Use this for initialization
        private void Start()
        {
			_lights = GetComponentsInChildren<Light>();
        }

        // Update is called once per frame
        private void Update()
        {
			_timeOfDay = Time.time / _daylength;
			transform.rotation = Quaternion.Euler(new Vector3(0, 0, _timeOfDay * 360));
			_lights[0].intensity = Mathf.Sin(_timeOfDay * Mathf.PI);
			_lights[1].intensity = 1 - Mathf.Sin(_timeOfDay * Mathf.PI);
        }

        #endregion Private Methods
    }
}