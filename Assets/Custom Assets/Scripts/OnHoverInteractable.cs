using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR;
using Unity.XR.CoreUtils;

public class OnHoverInteractable : XRGrabInteractable
{
    public bool selected = false;
    public bool toBeDestroyed;
    private Outline outline;
    public XRNode primaryDevice;
    private bool primaryTrigger;

    // Start is called before the first frame update
    void Start()
    {
        toBeDestroyed = false;
        Color c = gameObject.GetComponent<MeshRenderer>().material.color;
        c = Color.Lerp(c, Color.black, .8f);

        outline = gameObject.AddComponent<Outline>();
        outline.enabled = false;
        outline.OutlineMode = Outline.Mode.OutlineVisible;
        outline.OutlineColor = c;
        outline.OutlineWidth = 8f;
    }

    // Update is called once per frame
    void Update()
    {
        InputDevice primaryDevice = InputDevices.GetDeviceAtXRNode(XRNode.RightHand);

        outline.enabled = selected;
        primaryDevice.TryGetFeatureValue(CommonUsages.triggerButton, out primaryTrigger);
        if (toBeDestroyed) Destroy(gameObject);
    }


    protected override void OnHoverEntered(HoverEnterEventArgs args)
    {
        selected = !selected;
        //if(primaryTrigger) Debug.Log("Poof");
        if(primaryTrigger) toBeDestroyed = true;
    }
    protected override void Grab(){
        base.Grab();
    }
    protected override void Drop(){
        base.Drop();
    }

}
