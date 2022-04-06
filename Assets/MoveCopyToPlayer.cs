using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class MoveCopyToPlayer : MonoBehaviour
{
    public float speed = 1.0f;
    public Transform target;
    public InCheck inCheck;
    public bool copyExists = false;
    private GameObject duplicate;
    public bool iAmCopy = false;
    public bool inMenu = false;
    public GameObject parent;

    public Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
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
                rb.velocity = new Vector3(0, 0, 0);
                rb.isKinematic = false;
                rb.useGravity = false;
                duplicate = Instantiate(gameObject,transform.position,transform.rotation);
                var duplicateScript = duplicate.GetComponent<MoveCopyToPlayer>();
                duplicateScript.copyExists = true;
                duplicateScript.iAmCopy = true;
                duplicateScript.parent = gameObject;
                rb = duplicate.GetComponent<Rigidbody>();
                copyExists = true;
            }
            
        }

        if (iAmCopy)
        {
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
