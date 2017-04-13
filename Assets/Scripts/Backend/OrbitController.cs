using UnityEngine;

namespace Backend
{
    public class OrbitController : MonoBehaviour
    {
        #region Private Fields

        [SerializeField] private float _timeForFullRotation;
        private float _timestamp;
        private Vector3 _vOffset;

        #endregion Private Fields

        #region Public Properties

        [HideInInspector] public float OrbitArc { get; set; }

        #endregion Public Properties

        #region Private Methods

        private void Start()
        {
            _vOffset = transform.position;
            _timestamp = Time.time;
        }

        private void Update()
        {
            float elapsedTime = (Time.time - _timestamp) % _timeForFullRotation;
            OrbitArc = elapsedTime / _timeForFullRotation * 360.0f;

            float angle = elapsedTime / _timeForFullRotation * 360.0f;
            var q = Quaternion.AngleAxis(angle, Vector3.down);
            transform.position = q * _vOffset;
            transform.rotation = q;
        }

        #endregion Private Methods
    }
}