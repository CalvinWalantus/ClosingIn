using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class oe : MonoBehaviour {
    public Rigidbody rb;
    // Use this for initialization
    void Start () {
        rb = GetComponent<Rigidbody>();
        rb.AddForce(0, 5000, 0);
    }
	
	// Update is called once per frame
	void Update () {
        
    }
}
