using Backend;
using UnityEngine;

namespace Frontend
{
    public class LightController : MonoBehaviour
    {
        #region Private Fields

		[SerializeField] private float _cycleSpeed;

        #endregion Private Fields

        #region Private Methods

        // Use this for initialization
        private void Start()
        {
			
        }

        // Update is called once per frame
        private void Update()
        {
//            float orbitArc = _orbitController.OrbitArc;
//            if (orbitArc >= 180)
//            {
//                GetComponent<Light>().intensity = 0;
//            }
//            if (orbitArc >= 359)
//            {
//                GetComponent<Light>().intensity = 1;
//            }
        }

        #endregion Private Methods
    }
}