using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR;

public class MoveCopyToPlayer : MonoBehaviour
{
    public float speed;
    public Transform target;
    public int palettePosNum;
    public InCheck inCheck;
    public bool copyExists = false;
    private GameObject duplicate;
    public bool iAmCopy = false;
    public bool inMenu = false;
    public GameObject parent;
    public GameObject menu;
    public bool grabbed;
    public Vector3 initialPos;
    public Vector3 initialScale;
    public Rigidbody rb;
    public Rigidbody rbDuplicate;

    // Start is called before the first frame update
    void Start()
    {
        initialScale = Vector3.zero;
        grabbed = false;
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (inCheck.ConeCollisions.Contains(gameObject))
        {
            // create copy if copy doesn't exist
            if (!copyExists && inCheck.palettePos.Count > 0)
            {
                Debug.Log("I am duplicating" + gameObject.name + inCheck.palettePos.Count.ToString());
                rb.velocity = new Vector3(0, 0, 0);
                rb.isKinematic = false;
                rb.useGravity = false;
                duplicate = Instantiate(gameObject,transform.position,transform.rotation);
                Destroy(duplicate.GetComponent<XRGrabInteractable>());
                duplicate.layer = 7;
                duplicate.tag = "Duplicate";
                var duplicateScript = duplicate.GetComponent<MoveCopyToPlayer>();
                duplicateScript.copyExists = true;
                duplicateScript.iAmCopy = true;
                duplicateScript.parent = gameObject;
                rbDuplicate = duplicate.GetComponent<Rigidbody>();
                copyExists = true;
            }
            
        }

        if (iAmCopy)
        {
            // if(target == null) {
            //     GetComponent<MeshRenderer>().enabled = false;
            //     transform.position = parent.transform.position;
            // }
            // else GetComponent<MeshRenderer>().enabled = true;
            rb.velocity = new Vector3(0, 0, 0);
            rb.isKinematic = false;
            rb.useGravity = false;
            if (inMenu)
            {
                transform.position = target.position;
            }
            else
            {
                float step = speed * Time.deltaTime;

                if(target != null){
                    Vector3 moveTo = Vector3.MoveTowards(transform.position, target.position, step);
                    if (transform.position == target.position)
                    {
                        inMenu = true;
                        gameObject.AddComponent<OnHoverInteractable>();
                    }
                    else
                    {
                        transform.position = moveTo;
                    }                
                }

            }
        }
        
    }
    public void setInitialPos(){
        initialPos = transform.position;
    }
    public void setInitialScale(){
        initialScale = transform.localScale;
    }
}
