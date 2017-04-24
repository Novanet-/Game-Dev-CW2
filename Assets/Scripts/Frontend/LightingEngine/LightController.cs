using UnityEngine;

namespace Frontend
{
    public class LightController : MonoBehaviour
    {
        #region Private Fields

		[SerializeField] private GameObject _cameraObject;
		private Camera _cameraHook;
        [SerializeField] private float _daylength;
        [SerializeField] private float _daylightStrengthMult = 2.0f;
        private Light[] _lights;
        [SerializeField] private float _nightlightStrengthMult = 2.0f;
		[SerializeField] private float _nightClampVal = 0.5f;
        private float _timeOfDay;
		private float Daytime { get { return (_timeOfDay > 0.5f) ? 0 : _timeOfDay * 2f;} }
		private float Nighttime { get { return (_timeOfDay < 0.5f) ? 0 : (_timeOfDay - 0.5f) * 2f;} }

        #endregion Private Fields

        #region Private Methods

        // Use this for initialization
        private void Start()
        {
            _lights = GetComponentsInChildren<Light>();
			_cameraHook = _cameraObject.GetComponent<Camera>();
        }

        // Update is called once per frame
        private void Update()
        {
			float H, S, V;
			_timeOfDay = (Time.time % _daylength) / _daylength;
            transform.rotation = Quaternion.Euler(new Vector3(0, 0, _timeOfDay * 360));
			_lights[0].intensity = Mathf.Sin(Daytime * Mathf.PI) * _daylightStrengthMult;
			_lights[1].intensity = Mathf.Clamp(Mathf.Sin(Nighttime * Mathf.PI) * _nightlightStrengthMult, 0, _nightClampVal);

			Color currentColour = _cameraHook.backgroundColor;
			Color.RGBToHSV(currentColour, out H, out S, out V);
			V = 0.6f + Mathf.Clamp(0.8f * Mathf.Sin(_timeOfDay * 2 * Mathf.PI), -0.4f, 0.4f);
			_cameraHook.backgroundColor = Color.HSVToRGB(H, S, V);
        }

        #endregion Private Methods
    }
}