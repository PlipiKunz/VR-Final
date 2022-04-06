using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCopyToPlayer : MonoBehaviour
{
    public float speed = 1.0f;
    public Transform target;

    public Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        rb.velocity = new Vector3(0, 0, 0);
        float step = speed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, target.position, step);
    }
}
