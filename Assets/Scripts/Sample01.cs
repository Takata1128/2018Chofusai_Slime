using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sample01 : MonoBehaviour {
  NtUnity.Kinect nt;
  void Start () {
    nt = new NtUnity.Kinect();
  }
  void Update () {
    nt.setRGB();
    nt.setSkeleton();
    nt.setFace();
    nt.imshowBlack();
    Debug.Log("n = " + nt.skeleton.Count);
  }
  void OnApplicationQuit() {
    nt.stopKinect();
  }
}
