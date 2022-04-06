using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class uiMoveScript : MonoBehaviour
{

    public GameObject camera;
    // Start is called before the first frame update
    void Start()
    {
        //transform.GetComponent(typeof(RectTransform)).Pos = camera.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        //transform.GetComponent(typeof(RectTransform)).Pos += 1 * camera.transform.forward;
    }
}
