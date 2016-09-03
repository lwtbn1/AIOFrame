using UnityEngine;
using System.Collections;

public class Test : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        float f = Input.GetAxis("Mouse ScrollWheel");
        if(f != 0)
            Debug.Log(f.ToString());
	}
}
