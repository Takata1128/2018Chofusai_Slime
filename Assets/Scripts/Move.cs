using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour {
    public float speed = 0.03f;
    protected GameObject FaceCameraObj;
	// Use this for initialization
	void Start () {
        FaceCameraObj = GameObject.Find("Face Camera");
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKey(KeyCode.UpArrow))
        {
            transform.Translate(0, 0, speed);
            FaceCameraObj.transform.Translate(0, 0, speed);
        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            transform.Translate(0, 0, -speed);
            FaceCameraObj.transform.Translate(0, 0, -speed);
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            transform.Translate(speed, 0, 0);
            FaceCameraObj.transform.Translate(speed, 0, 0);
        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            transform.Translate(-speed, 0, 0);
            FaceCameraObj.transform.Translate(-speed, 0, 0);
        }
        if (Input.GetKey(KeyCode.U))
        {
            transform.Translate(0, speed, 0);
            FaceCameraObj.transform.Translate(0, speed, 0);
        }
        if (Input.GetKey(KeyCode.D))
        {
            transform.Translate(0, -speed, 0);
            FaceCameraObj.transform.Translate(0, -speed, 0);
        }

    }
}
