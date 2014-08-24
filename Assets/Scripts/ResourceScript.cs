using UnityEngine;
using System.Collections;

public class ResourceScript : MonoBehaviour {
	
	public float resourceAmount;
	
	public float rotationSpeed;
	
	public float drainSpeed;
	
	
	public bool connected;
	// Use this for initialization
	void Start () {
		rotationSpeed = Random.Range(15.0f,22.0f);
		
		if(Random.Range(0,1) > 0.5) {
			rotationSpeed *= -1;
		}
	}
	
	// Update is called once per frame
	void Update () {
	 
	 if(connected) {
	 	float drainAmount = Time.deltaTime * drainSpeed;
		if(resourceAmount >=  drainAmount) {
			HUD.getHUD().bottomPanel.waterValue +=  drainAmount;
			resourceAmount -=  drainAmount;
		} else {
			HUD.getHUD().bottomPanel.waterValue += drainAmount;
			Destroy(gameObject);
		}
		
		}
		this.transform.RotateAround(transform.position, Vector3.forward,rotationSpeed * Time.deltaTime); 
		//this.transform.Rotate(0.0f, 0.0f, rotationSpeed * Time.deltaTime);
	}
	
	public void Connect() {
		connected = true;
	}
}
