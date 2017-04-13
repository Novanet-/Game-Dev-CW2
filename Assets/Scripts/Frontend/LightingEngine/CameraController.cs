using UnityEngine;

namespace Frontend
{
    public class CameraController : MonoBehaviour
    {
        #region Public Fields

        public float CameraDistance;
        public Transform CurrentTarget;
        public float TravelTime;

        #endregion Public Fields

        #region Private Methods

        // Update is called once per frame
        private void Update()
        {
            var newPosition = new Vector3(CurrentTarget.transform.position.x, CurrentTarget.transform.position.y + 6, CameraDistance);
            transform.position = Vector3.Lerp(transform.position, newPosition, 1 / TravelTime * Time.deltaTime);
        }

        #endregion Private Methods
    }
}