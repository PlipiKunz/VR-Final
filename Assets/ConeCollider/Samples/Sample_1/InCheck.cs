using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class InCheck : MonoBehaviour {

    private Text text;
    public List<GameObject> ConeCollisions;

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
        text.text = "";
        for(int x = 0; x < ConeCollisions.Count; x ++){
            text.text += ConeCollisions[x].name + '\n';
        }
	}

    void OnTriggerEnter(Collider other)
    {
        ConeCollisions.Add(other.gameObject);
    }

    void OnTriggerExit(Collider other)
    {
        text.text = "OUT";
        ConeCollisions.Remove(other.gameObject);

    }
}
