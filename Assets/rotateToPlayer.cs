using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rotateToPlayer : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        gameObject.transform.LookAt(gameObject.transform.position - ( Camera.main.transform.position - gameObject.transform.position));
        //gameObject.transform.eulerAngles = gameObject.transform.eulerAngles + 180 * Vector3.up;
        gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
    }
}
