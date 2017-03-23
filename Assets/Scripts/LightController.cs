using UnityEngine;

public class LightController : MonoBehaviour
{
    #region Private Fields

    [SerializeField] private GameObject _orbitCentre;

    private OrbitController orbitController;

    #endregion Private Fields

    #region Private Methods

    // Use this for initialization
    private void Start()
    {
        orbitController = _orbitCentre.GetComponent<OrbitController>();
    }

    // Update is called once per frame
    private void Update()
    {
        float orbitArc = orbitController.OrbitArc;
        if (orbitArc >= 180)
        {
            GetComponent<Light>().intensity = 0;
            //            Debug.Log(string.Format("OrbitArc = {0}", orbitArc));
        }
        if (orbitArc >= 359)
        {
            GetComponent<Light>().intensity = 1;
            //            Debug.Log(string.Format("OrbitArc = {0}", orbitArc));
            //28666
        }
    }

    #endregion Private Methods
}