using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class InCheck : MonoBehaviour {

    private Text text;
    public List<GameObject> ConeCollisions;
    public List<GameObject> ConeCollisionDuplicates;
    public GameObject Marker;
    public Vector3 sumPositions;
    public Vector3 averagePos = new Vector3(-100,-100, - 100);
    public List<GameObject> menuPositions;
    //private Stack menuPositionStack;
    private int counter = 0;
    [Range(-1.0f, 1.0f)] public float ScatterGather = 1;

    void Awake()
    {
        text = GameObject.Find("Text").GetComponent<Text>();
    }

	// Use this for initialization
	void Start () {
        ConeCollisions = new List<GameObject>();
        ConeCollisionDuplicates = new List<GameObject>();
        text.color = Color.red;
        //Stack temp = new Stack();
        // foreach(var ob in menuPositions){
        //     temp.Push((GameObject)ob);
        //     //Debug.Log(ob.name);
        // }
        // int c = temp.Count;
        // for (int x = 0; x < c; x++){
        //     GameObject Temp2 = (GameObject)temp.Pop();
        //     menuPositionStack.Push(Temp2);
        // }
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
            // GameObject duplicate = Instantiate(other.gameObject);
            // duplicate.AddComponent<MoveCopyToPlayer>();
            // duplicate.transform.position = other.gameObject.transform.position;
            // duplicate.tag = "Untagged";
            // ConeCollisionDuplicates.Add(duplicate);
            if(other.gameObject.TryGetComponent(out Rigidbody temp)){
                temp.useGravity = false;
                temp.velocity = Vector3.zero;
            }
        }
        if(other.gameObject.tag == "Duplicate" && !ConeCollisionDuplicates.Contains(other.gameObject)) {
            other.gameObject.GetComponent<MoveCopyToPlayer>().target = menuPositions[counter].transform;
            other.gameObject.transform.localScale = other.gameObject.transform.localScale/10;
            float scaleMagnitude = other.gameObject.transform.localScale.magnitude;
            Debug.Log($"This sphere is scaled to: {scaleMagnitude}");
            counter++;
            ConeCollisionDuplicates.Add(other.gameObject);
            // GameObject duplicate = Instantiate(other.gameObject);
            // duplicate.AddComponent<MoveCopyToPlayer>();
            // duplicate.transform.position = other.gameObject.transform.position;
            // duplicate.tag = "Untagged";
            // ConeCollisionDuplicates.Add(duplicate);
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
