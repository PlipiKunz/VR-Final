using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuScript : MonoBehaviour
{
    public List<GameObject> currentMinis;
    public List<GameObject> futureMinis;
    public GameObject cone;
    public GameObject movingObject; // on grab save moving objects position, get offset, and add to selected objects

    // Start is called before the first frame update
    void Start()
    {
        var inConeScript = cone.GetComponent<InCheck>();
    }

    // Update is called once per frame
    void Update()
    {

        
        foreach(GameObject ob in cone.GetComponent<InCheck>().ConeCollisionDuplicates){
            if(ob.GetComponent<OnHoverInteractable>().selected){
                
            }
        }
        
        // // Manipulate current minis here

        // foreach(GameObject ob in cone.GetComponent<InCheck>().ConeCollisionDuplicates){
        //     if(!ob.GetComponent<OnHoverInteractable>().toBeDestroyed) {
        //         futureMinis.Add(ob);
        //     }
        // }

        // cone.GetComponent<InCheck>().ConeCollisionDuplicates = futureMinis;
        // futureMinis.Clear();
    }
}
