using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class InCheck : MonoBehaviour {

    private Text text;
    public List<GameObject> ConeCollisions;
    public GameObject Marker;
    public Vector3 sumPositions;
    public Vector3 averagePos = new Vector3(-100,-100, - 100);
    [Range(-1.0f, 1.0f)] public float ScatterGather = 1;

    void Awake()
    {
        text = GameObject.Find("Text").GetComponent<Text>();
    }

	// Use this for initialization
	void Start () {
        ConeCollisions = new List<GameObject>();
        text.color = Color.red;
	}
	
	// Update is called once per frame
	void Update () {
        //averagePos = Vector3.zero;
        text.text = "";
        sumPositions = Vector3.zero;
        for(int x = 0; x < ConeCollisions.Count; x ++){
            text.text += ConeCollisions[x].name + '\n';
        }

        for(int x = 0; x < ConeCollisions.Count; x ++){
            if (ConeCollisions[x].name != "XR Origin") sumPositions += ConeCollisions[x].transform.position;
        }
        averagePos = sumPositions / (ConeCollisions.Count);
        // Marker.transform.position = averagePos;
        for(int x = 0; x < ConeCollisions.Count; x ++){
            if (ConeCollisions[x].name != "XR Origin") 
            {
                Vector3 Temp = averagePos - ConeCollisions[x].transform.position;
                Temp = Temp * ScatterGather * Time.deltaTime;
                ConeCollisions[x].transform.position += Temp;
            }
        }
        

	}

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Selectable" && !ConeCollisions.Contains(other.gameObject)) {
            ConeCollisions.Add(other.gameObject);
            if(other.gameObject.TryGetComponent(out Rigidbody temp)){
                temp.useGravity = false;
                temp.velocity = Vector3.zero;
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        // text.text = "OUT";
        //ConeCollisions.Remove(other.gameObject);
        if(other.gameObject.TryGetComponent(out Rigidbody temp)){
            temp.useGravity = true;
        }

    }
}
