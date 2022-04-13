using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR;
using Unity.XR.CoreUtils;

public class ButtonScript : XRGrabInteractable
{
    public InCheck InCheck;
    public GameObject slot;
    private Vector3 diff; 
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = slot.transform.position;
        transform.rotation = slot.transform.rotation;
    }

    protected override void Grab(){
        if (gameObject.name == "Gather Button") InCheck.ScatterGather = 3;
        else if (gameObject.name == "Scatter Button") InCheck.ScatterGather = -3;
        else if (gameObject.name == "1x Button") InCheck.moveSpeed = 1;
        else if (gameObject.name == "5x Button") InCheck.moveSpeed = 5;
        else if (gameObject.name == "10x Button") InCheck.moveSpeed = 10;
    }
    protected override void Drop(){
        if (gameObject.name == "Gather Button") InCheck.ScatterGather = 0;
        else if (gameObject.name == "Scatter Button") InCheck.ScatterGather = -3;
    }
}