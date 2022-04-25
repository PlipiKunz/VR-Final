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
    public bool copyExists = false;
    private GameObject duplicate;
    public bool iAmCopy = false;
    public bool inMenu = false;
    public bool hasFallen;
    public float fallAfter;

    public GameObject parent;
    public InCheck inCheck;
    public GameObject menu;

    public bool grabbed;
    public Vector3 initialPos;
    public Vector3 initialScale;
    public Rigidbody rb;
    public Rigidbody rbDuplicate;

    // Start is called before the first frame update
    void Start()
    {
        speed = 5f;
        hasFallen = false;
        fallAfter = 8f;
        menu = GameObject.Find("menu");

        //Debug.Log(menu);
        inCheck = GameObject.Find("Cone").GetComponent<InCheck>();

        initialScale = Vector3.zero;
        grabbed = false;
        rb = GetComponent<Rigidbody>();


    }

    // Update is called once per frame
    void Update()
    {
        if(!hasFallen && fallAfter - Time.realtimeSinceStartup < 0){
            rb.useGravity = true;
            hasFallen = true;
        }
        if(rb.useGravity == false){
            rb.angularVelocity = Vector3.zero;
            rb.velocity = Vector3.zero;
        }
        if (inCheck.ConeCollisions.Contains(gameObject))
        {
            // create copy if copy doesn't exist
            if (!copyExists && inCheck.palettePos.Count > 0)
            {
                inCheck.palettePos.Sort();
                target = inCheck.menuPositions[inCheck.palettePos[0]].transform;
                palettePosNum = inCheck.palettePos[0];
                inCheck.palettePos.Remove(inCheck.palettePos[0]);
                // Debug.Log("I am duplicating" + gameObject.name + inCheck.palettePos.Count.ToString());
                duplicate = Instantiate(gameObject,transform.position,transform.rotation);
                Destroy(duplicate.GetComponent<XRGrabInteractable>());
                duplicate.transform.localScale = new Vector3(3f,3f,3f);
                duplicate.GetComponent<Renderer>().material = GetComponent<Renderer>().material;
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
                    Debug.Log(transform.name);
                    Vector3 moveTo = Vector3.MoveTowards(transform.position, target.position, step);
                    Debug.Log(moveTo);
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
