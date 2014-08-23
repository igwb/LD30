using UnityEngine;
using System.Collections;

public class StemCreator : MonoBehaviour {

	public GameObject stemPrefab;
	GameObject stem;

	public bool currentlyPlacing;
	
	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		if(currentlyPlacing) {
			StemPositioner positioner = stem.GetComponent<StemPositioner>();
		
			positioner.endPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);		
		}
	}
	
	public void StartPlacing() {
		stem = (GameObject) GameObject.Instantiate(stemPrefab);
		stem.GetComponent<StemPositioner>().startPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		currentlyPlacing = true;
	}
	
	public void EndPlacing() {
		currentlyPlacing = false;
		stem = null;
	}
}
