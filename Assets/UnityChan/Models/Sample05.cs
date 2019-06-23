using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sample05 : MonoBehaviour
{
    public GameObject humanoid;
    public bool mirror = true;
    public bool move = true;
    public bool headMove = true;

    NtUnity.Kinect nt;
    NtUnity.UnityChanSkeleton cs;
    public int rhState;
    public int lhState;

    void Start()
    {
        nt = new NtUnity.Kinect();
        cs = new NtUnity.UnityChanSkeleton(humanoid);
    }

    void Update()
    {
        nt.setRGB();
        nt.setSkeleton();
        //nt.setFace();
        nt.imshowBlack();
        int n = nt.getSkeleton();
        if (n > 0)
        {
            cs.set(nt, 0, mirror, move, headMove);
            rhState = nt.handState(false);
            lhState = nt.handState(true);
        }
        
        Debug.Log("rh:" + rhState + "lh:" + lhState);
    }

    void OnApplicationQuit()
    {
        nt.stopKinect();
    }
}