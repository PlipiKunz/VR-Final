using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class OnHoverInteractable : XRBaseInteractable
{
    public bool selected = false;
    private Outline outline;

    // Start is called before the first frame update
    void Start()
    {
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
        outline.enabled = selected;
    }


    protected override void OnHoverEntered(HoverEnterEventArgs args)
    {
        selected = !selected;
    }

}
