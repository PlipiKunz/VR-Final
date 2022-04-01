using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class InCheck : MonoBehaviour {

    private Text text;

    void Awake()
    {
        text = GameObject.Find("Text").GetComponent<Text>();
    }

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter(Collider other)
    {
        text.text = "IN";
        text.color = Color.red;
        text.text = other.gameObject.name;//.GetComponent<Collider>().transform.parent.ToString();
    }

    void OnTriggerExit(Collider other)
    {
        text.text = "OUT";
        text.color = Color.blue;
    }
}
