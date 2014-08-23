using UnityEngine;
using System.Collections;

public class ResourceScript : MonoBehaviour {
	
	public float resourceAmount;
	
	public float rotationSpeed;
	// Use this for initialization
	void Start () {
		rotationSpeed = Random.Range(15.0f,22.0f);
		
		if(Random.Range(0,1) > 0.5) {
			rotationSpeed *= -1;
		}
	}
	
	// Update is called once per frame
	void Update () {
		this.transform.Rotate(0.0f, 0.0f, rotationSpeed * Time.deltaTime);
	}
}
