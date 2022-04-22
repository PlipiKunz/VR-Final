using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SphereColors { 
    white,
    blue,
    red,    
    green,
}
public class SphereScript : MonoBehaviour
{
    public static System.Random r = new System.Random();
    //0 = white
    public SphereColors sphereColor;

    // Start is called before the first frame update
    void Start()
    {
        sphereColor = (SphereColors)r.Next(0,4);
        var material = gameObject.GetComponent<Renderer>().material;
        switch (sphereColor) { 
            case SphereColors.white: {
                material.color = Color.white;
                break;
            }
            case SphereColors.blue:
                {
                    material.color = Color.blue;
                    break ;
            }
            case SphereColors.red:
                {
                    material.color = Color.red;
                    break;
            }
            case SphereColors.green:
                {
                    material.color = Color.green;
                    break ;
            }
            default: break;

        }
    }
}
