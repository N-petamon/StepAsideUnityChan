using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectDestroyController : MonoBehaviour {

    Camera cameraObject;



	// Use this for initialization
	void Start () {

        cameraObject = GameObject.Find("Main Camera").GetComponent <Camera > ();
		
	}
	
	// Update is called once per frame
	void Update () {

        if (this.transform.position.z < cameraObject.transform.position.z)
        {
            Debug.Log("破壊");

            Destroy(this.gameObject);
        }
		
	}
}
