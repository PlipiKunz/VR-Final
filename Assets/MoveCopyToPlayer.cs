using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class MoveCopyToPlayer : MonoBehaviour
{
    public float speed;
    public Transform target;
    public InCheck inCheck;
    public bool copyExists = false;
    private GameObject duplicate;
    public bool iAmCopy = false;
    public bool inMenu = false;
    public GameObject parent;
    public GameObject menu;
    public bool grabbed;
    public Vector3 initialPos;
    public Rigidbody rb;
    public Rigidbody rbDuplicate;

    // Start is called before the first frame update
    void Start()
    {
        grabbed = false;
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (inCheck.ConeCollisions.Contains(gameObject))
        {
            // create copy if copy doesn't exist
            if (!copyExists)
            {
                Debug.Log("I am duplicating" + gameObject.name);
                rb.velocity = new Vector3(0, 0, 0);
                rb.isKinematic = false;
                rb.useGravity = false;
                duplicate = Instantiate(gameObject,transform.position,transform.rotation);
                duplicate.tag = "Duplicate";
                var duplicateScript = duplicate.GetComponent<MoveCopyToPlayer>();
                duplicateScript.copyExists = true;
                duplicateScript.iAmCopy = true;
                duplicateScript.parent = gameObject;
                duplicate.AddComponent<OnHoverInteractable>();
                rbDuplicate = duplicate.GetComponent<Rigidbody>();
                copyExists = true;
            }
            
        }

        if (iAmCopy)
        {
            // if(!menu.activeSelf && inMenu) 
            // {
            //     gameObject.SetActive(false);
            // }
            // else 
            // {
            //     gameObject.SetActive(true);
            // }
            //Rigidbody myRB = GetComponent<Rigidbody>();
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
}
