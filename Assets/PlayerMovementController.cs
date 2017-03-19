using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementController : MonoBehaviour
{
    private bool isFalling;
    public float HorizontalForceMagnitude;
    public float VerticalForceMagnitude;
    // Use this for initialization
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
//        var x = Input.GetAxis("Horizontal") * Time.deltaTime * 10.0f;
        //        var y = transform.position.y <= -19 ? Input.GetAxis("Vertical") * Time.deltaTime * 400.0f : transform.position.y;

        if (Input.GetKeyDown("space")){
            GetComponent<Rigidbody>().AddForce(new Vector3(0, 20, 0), ForceMode.Impulse);
        }
        GetComponent<Rigidbody>().AddForce(Input.GetAxis("Horizontal") * transform.right * HorizontalForceMagnitude, ForceMode.VelocityChange);
        GetComponent<Rigidbody>().AddForce(Input.GetAxis("Vertical") * transform.up * HorizontalForceMagnitude, ForceMode.Impulse);

        //        isFalling = true;

//        transform.Translate(x, 0, 0);
    }

}