using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
public class rotateToPlayer : XRGrabInteractable
{

    public bool selected = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    void Update()
    {
        if (selected) gameObject.transform.LookAt(gameObject.transform.position - ( Camera.main.transform.position - gameObject.transform.position));
        //gameObject.transform.eulerAngles = gameObject.transform.eulerAngles + 180 * Vector3.up;
        gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
    }

    // Update is called once per frame
    protected override void Grab()
    {
        selected = true;
        base.Grab();
    }
    protected override void Drop()
    {
        selected = false;
        base.Drop();
    }
}
