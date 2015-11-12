using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {

    //public float rotation;
    // Use this for initialization
    void Start () {
       // rotation = GetComponent<Transform>().parent.rotation.z;

    }
	
	// Update is called once per frame
	void Update () {
        transform.Rotate(new Vector3(0, 0, 360-GetComponent<Transform>().parent.rotation.z));
    }
}
