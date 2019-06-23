using UnityEngine;
using System.Collections;
using System.IO.Ports;
using System.Threading;

public class SendIsTouching : MonoBehaviour {
    SerialHandler serialHandler;
    protected GameObject refObj;
    protected Expression expression;

	// Use this for initialization
	void Start () {
        serialHandler = this.GetComponent<SerialHandler>();
        refObj = GameObject.Find("Face");
        expression = refObj.GetComponent<Expression>();

	}
	
	// Update is called once per frame
	void Update () {
        serialHandler.Write(expression.isTouched.ToString());
	}
}
