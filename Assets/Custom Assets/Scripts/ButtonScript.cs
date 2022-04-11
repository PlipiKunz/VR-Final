using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR;
using Unity.XR.CoreUtils;

public class ButtonScript : XRGrabInteractable
{
    public InCheck InCheck;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    protected override void Grab(){
        if (gameObject.name == "Gather Button") InCheck.ScatterGather = 3;
    }
    protected override void Drop(){
        if (gameObject.name == "Gather Button") InCheck.ScatterGather = 0;
    }
}