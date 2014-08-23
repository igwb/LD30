using UnityEngine;
using System.Collections;

public class CameraMovement : MonoBehaviour {


	public float speed;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		this.transform.position = new Vector3(this.transform.position.x + (speed * Input.GetAxis("Horizontal") * Time.deltaTime), this.transform.position.y + (speed * Input.GetAxis("Vertical") * Time.deltaTime), transform.position.z);
		
	}
}
