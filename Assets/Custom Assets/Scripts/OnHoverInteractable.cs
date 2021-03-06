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
    public XRNode secondaryDevice;
    private bool primaryTrigger;
    private bool secondaryTrigger;
    private Vector3 initialPos;
    private bool grabbed;
    public bool primaryGrip;
    public bool secondaryGrip;
    bool changed;

    void Start()
    {
        changed = false;
        primaryGrip = false;
        secondaryGrip = false;
        selected = false;
        toBeDestroyed = false;
        grabbed = false;
        Color c = gameObject.GetComponent<MeshRenderer>().material.color;
        c = Color.Lerp(c, Color.black, .8f);

        outline = gameObject.AddComponent<Outline>();
        outline.enabled = false;
        outline.OutlineMode = Outline.Mode.OutlineVisible;
        outline.OutlineColor = c;
        outline.OutlineWidth = 8f;
    }

    void Update()
    {
        InputDevice primaryDevice = InputDevices.GetDeviceAtXRNode(XRNode.RightHand);
        InputDevice secondaryDevice = InputDevices.GetDeviceAtXRNode(XRNode.LeftHand);

        outline.enabled = selected;
        primaryDevice.TryGetFeatureValue(CommonUsages.triggerButton, out primaryTrigger);
        secondaryDevice.TryGetFeatureValue(CommonUsages.triggerButton, out secondaryTrigger);
        primaryDevice.TryGetFeatureValue(CommonUsages.gripButton, out primaryGrip);
        secondaryDevice.TryGetFeatureValue(CommonUsages.gripButton, out secondaryGrip);
        if (toBeDestroyed) {
            gameObject.GetComponent<MoveCopyToPlayer>().parent.GetComponent<MoveCopyToPlayer>().copyExists = false;
            Destroy(gameObject);
        }

        if (grabbed){
            Vector3 diff = transform.position - initialPos;
            GetComponent<MoveCopyToPlayer>().inCheck.diff = diff;
            GetComponent<MoveCopyToPlayer>().inCheck.rot = transform.rotation;
        }
        if (primaryGrip && secondaryGrip){
            GetComponent<MoveCopyToPlayer>().inCheck.getScale();
        }else{
            GetComponent<MoveCopyToPlayer>().inCheck.hasInitialDistScale = false;
            foreach(GameObject ob in GetComponent<MoveCopyToPlayer>().inCheck.ConeCollisionDuplicates){
                ob.GetComponent<MoveCopyToPlayer>().parent.GetComponent<MoveCopyToPlayer>().setInitialScale();
            }
            GetComponent<MoveCopyToPlayer>().inCheck.scale = 0;

        }
        
        if(selected) {
            GetComponent<MoveCopyToPlayer>().parent.GetComponent<Rigidbody>().velocity = Vector3.zero;
            GetComponent<MoveCopyToPlayer>().parent.GetComponent<Rigidbody>().useGravity = false;
        }
        else GetComponent<MoveCopyToPlayer>().parent.GetComponent<Rigidbody>().useGravity = true;
        if(!primaryTrigger){
            changed = false;
        }
        if(isHovered){
            if (primaryTrigger && !changed) {
                selected = !selected;
                changed = true;
            }
            //if(primaryTrigger) Debug.Log("Poof");
            if(secondaryTrigger) {
                selected = false;
                GetComponent<MoveCopyToPlayer>().inCheck.palettePos.Add(GetComponent<MoveCopyToPlayer>().palettePosNum);
                toBeDestroyed = true;
                gameObject.GetComponent<MoveCopyToPlayer>().inCheck.ConeCollisionDuplicates.Remove(gameObject);
            }
        }

    }




    protected override void Grab(){
        selected = true;
        initialPos = transform.position;
        GetComponent<MoveCopyToPlayer>().parent.GetComponent<MoveCopyToPlayer>().grabbed = true;
        foreach(GameObject ob in GetComponent<MoveCopyToPlayer>().inCheck.ConeCollisionDuplicates){
            ob.GetComponent<MoveCopyToPlayer>().parent.GetComponent<MoveCopyToPlayer>().setInitialPos();

        }
        grabbed = true;
        base.Grab();
    }
    protected override void Drop(){
        GetComponent<MoveCopyToPlayer>().inCheck.diff = Vector3.zero;
        GetComponent<MoveCopyToPlayer>().inCheck.scale = 0;
        GetComponent<MoveCopyToPlayer>().parent.GetComponent<MoveCopyToPlayer>().grabbed = false;

        grabbed =  false;
        base.Drop();
    }

}
