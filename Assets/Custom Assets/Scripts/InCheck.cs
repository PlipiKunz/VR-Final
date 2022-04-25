using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class InCheck : MonoBehaviour {

    public Vector3 diff;
    public float scaleFactor;
    public float scale;
    public Quaternion rot;

    public GameObject Marker;
    public Vector3 sumPositions;
    public Vector3 averagePos = new Vector3(-100,-100, - 100);
    public List<int> palettePos;
    public List<GameObject> removeConeWhenLookedAtList;
    public int SelectedObjects;
    public ConeCollider cone;
    public List<GameObject> ConeCollisions;
    public List<GameObject> ConeCollisionDuplicates;
    public List<GameObject> menuPositions;
    public int moveSpeed;
    public bool hasInitialDistScale;
    public float initialDistanceScale;
    public GameObject PrimaryHand;
    public GameObject SecondaryHand;
    
    [Range(-4.0f, 4.0f)] public float ScatterGather = 1;

    void Awake()
    {
    }

	// Use this for initialization
	void Start () {
        hasInitialDistScale = false;
        moveSpeed = 30;
        diff = Vector3.zero;
        rot = Quaternion.identity;
        scale = 0;
        scaleFactor = 10f;
        SelectedObjects = 0;
        cone = GetComponent<ConeCollider>();
        foreach (int index in Enumerable.Range(0, 20))
        {
            palettePos.Add(index);
        }
        ConeCollisions = new List<GameObject>();
        ConeCollisionDuplicates = new List<GameObject>();
	}
	
	// Update is called once per frame
	void Update () {
        SelectedObjects = 0;
        foreach(GameObject ob in ConeCollisionDuplicates){
            if (ob.GetComponent<OnHoverInteractable>() != null && ob.GetComponent<OnHoverInteractable>().selected) SelectedObjects++;
        }

        if(diff != Vector3.zero){
            foreach(GameObject ob in ConeCollisionDuplicates){
                //Debug.Log(ob.name);
                if(ob.GetComponent<OnHoverInteractable>().selected) {
                    var p = ob.GetComponent<MoveCopyToPlayer>().parent;
                    p.transform.position = p.GetComponent<MoveCopyToPlayer>().initialPos + diff * moveSpeed;
                }
            }
        }
        if(scale != 0){
            foreach(GameObject ob in ConeCollisionDuplicates){
                if(ob.GetComponent<OnHoverInteractable>().selected) {
                    var p = ob.GetComponent<MoveCopyToPlayer>().parent;
                    p.transform.localScale = p.GetComponent<MoveCopyToPlayer>().initialScale + new Vector3(scale * scaleFactor, scale * scaleFactor, scale * scaleFactor);
                }
            }
        }

        if (rot != Quaternion.identity)
        {
            foreach (GameObject ob in ConeCollisionDuplicates)
            {
                if (ob.GetComponent<OnHoverInteractable>().selected)
                {
                    var p = ob.GetComponent<MoveCopyToPlayer>().parent;
                    p.transform.rotation = rot;
                }
            }
        }

        if (ScatterGather != 0){
            sumPositions = Vector3.zero;
            int c = 0;
            for(int x = 0; x < ConeCollisionDuplicates.Count; x ++){
                if(ConeCollisionDuplicates[x].GetComponent<OnHoverInteractable>().selected) {
                    sumPositions += ConeCollisionDuplicates[x].GetComponent<MoveCopyToPlayer>().parent.transform.position;
                    c++;
                }
            }
            averagePos = sumPositions / (c);
            // Marker.transform.position = averagePos;

            for(int x = 0; x < ConeCollisionDuplicates.Count; x ++){
                if(ConeCollisionDuplicates[x].GetComponent<OnHoverInteractable>().selected) {
                    Vector3 Temp = averagePos - ConeCollisionDuplicates[x].GetComponent<MoveCopyToPlayer>().parent.transform.position;
                    Temp = Temp * ScatterGather * Time.deltaTime;
                    ConeCollisionDuplicates[x].GetComponent<MoveCopyToPlayer>().parent.transform.position += Temp;
                }
            }
        }
	}

    void OnTriggerEnter(Collider other)
    {
        //Debug.Log(other.name);
        if(other.gameObject.tag == "Selectable" && !ConeCollisions.Contains(other.gameObject)) {
            ConeCollisions.Add(other.gameObject);
            // GameObject duplicate = Instantiate(other.gameObject);
            // duplicate.AddComponent<MoveCopyToPlayer>();
            // duplicate.transform.position = other.gameObject.transform.position;
            // duplicate.tag = "Untagged";
            // ConeCollisionDuplicates.Add(duplicate);
            // if(other.gameObject.TryGetComponent(out Rigidbody temp)){
            //     temp.useGravity = false;
            //     temp.velocity = Vector3.zero;
            // }
        }
        
    }

    void OnTriggerExit(Collider other)
    {
        ConeCollisions.Remove(other.gameObject);
    }

    public void getScale(){
         if(!hasInitialDistScale){
             initialDistanceScale = Vector3.Distance(PrimaryHand.transform.position, SecondaryHand.transform.position);
             hasInitialDistScale = true;
         }
        scale = Vector3.Distance(PrimaryHand.transform.position, SecondaryHand.transform.position) - initialDistanceScale;
    }
}
