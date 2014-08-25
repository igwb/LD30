using UnityEngine;
using System.Collections;

public class ResourceScript : MonoBehaviour {
	
	public float resourceAmount;
	public float resourceDrainSpeed;


	public float rotationSpeed;
	
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
	 
		 if(!connected) {
			return;
		 }

	 	float drainAmount = Time.deltaTime * resourceDrainSpeed;

		if(resourceAmount >=  drainAmount) {
			HUD.getHUD().bottomPanel.waterValue +=  drainAmount;
			resourceAmount -=  drainAmount;
		} else {
			HUD.getHUD().bottomPanel.waterValue += drainAmount;
			Destroy(gameObject);
		}
	
		this.transform.RotateAround(transform.position, Vector3.forward,rotationSpeed * Time.deltaTime); 
	}
	
	public void Connect() {
		
		connected = true;
	}
}
