using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class InCheck : MonoBehaviour {

    private Text text;
    public Vector3 diff;
    public GameObject Marker;
    public Vector3 sumPositions;
    public Vector3 averagePos = new Vector3(-100,-100, - 100);
    public List<int> palettePos;
    public List<GameObject> removeConeWhenLookedAtList;
    public int lookedAtObjectCount;
    public ConeCollider cone;
    public List<GameObject> ConeCollisions;
    public List<GameObject> ConeCollisionDuplicates;
    public List<GameObject> menuPositions;

    [Range(-4.0f, 4.0f)] public float ScatterGather = 1;

    void Awake()
    {
        text = GameObject.Find("Text").GetComponent<Text>();
    }

	// Use this for initialization
	void Start () {
        diff = Vector3.zero;
        lookedAtObjectCount = 0;
        cone = GetComponent<ConeCollider>();
        foreach (int index in Enumerable.Range(0, 20))
        {
            palettePos.Add(index);
        }
        ConeCollisions = new List<GameObject>();
        ConeCollisionDuplicates = new List<GameObject>();
        text.color = Color.red;
	}
	
	// Update is called once per frame
	void Update () {

        if(diff != Vector3.zero){
            foreach(GameObject ob in ConeCollisionDuplicates){
                Debug.Log(ob.name);
                if(ob.GetComponent<OnHoverInteractable>().selected) ob.GetComponent<MoveCopyToPlayer>().parent.transform.position = ob.GetComponent<MoveCopyToPlayer>().parent.GetComponent<MoveCopyToPlayer>().initialPos + diff * 10;
            }
        }

        if (lookedAtObjectCount > 0) cone.enabled = false;
        else cone.enabled = true;
        
        if(ScatterGather != 0){
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
        Debug.Log(other.name);
        foreach(GameObject ob in removeConeWhenLookedAtList){
            if (other.gameObject.name == ob.name) lookedAtObjectCount++;
        }
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
        if(other.gameObject.tag == "Duplicate" && !ConeCollisionDuplicates.Contains(other.gameObject) && palettePos.Count > 0) {
            palettePos.Sort();
            other.gameObject.GetComponent<MoveCopyToPlayer>().target = menuPositions[palettePos[0]].transform;
            other.gameObject.GetComponent<MoveCopyToPlayer>().palettePosNum = palettePos[0];
            palettePos.Remove(palettePos[0]);
            other.gameObject.transform.localScale = other.gameObject.transform.localScale/10;
            float scaleMagnitude = other.gameObject.transform.localScale.magnitude;
            Debug.Log($"This sphere is scaled to: {scaleMagnitude}");
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
        foreach(GameObject ob in removeConeWhenLookedAtList){
            if (other.gameObject.name == ob.name) lookedAtObjectCount--;
        }
        ConeCollisions.Remove(other.gameObject);
        if(other.gameObject.TryGetComponent(out Rigidbody temp)){
            temp.useGravity = true;
        }

    }
}
