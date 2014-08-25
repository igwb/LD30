using UnityEngine;
using System.Collections;

public class ResourceGenerator : MonoBehaviour {

	public GameObject resourcePrefab;
	
	public int resourceCount;
	public float minSpacing;
	public float generationSize;
	
	public float minResourceAmount;
	public float maxResourceAmount;
	
	private int timeout = 10;


	// Use this for initialization
	void Start () {
		Generate();
	}
	
	private void Generate() {
		
		ArrayList resources = new ArrayList();
		GameObject resource;
		
		Vector2 pos = Vector2.zero;
		bool posIsValid = false;
		
		for(int i = 0; i <= resourceCount; i++) {
			
			resource = (GameObject) GameObject.Instantiate(resourcePrefab);
			resource.GetComponent<ResourceController>().resourceAmount = Random.Range(minResourceAmount,maxResourceAmount);
			
			int loops = 0;
			posIsValid = false;
			while(!posIsValid) {
				
				//Abort if no position can be found to prevent infinite loops.
				if(loops >= timeout) {
					break;
				}
				
				//The position is assumed innocent unless prooven otherwise.
				pos = Random.insideUnitCircle * generationSize;
				pos += (Vector2)this.transform.position;
				posIsValid = true;
				
				foreach(GameObject g in resources) {
					if(Vector2.Distance(g.transform.position, pos) < minSpacing) {
						posIsValid = false;
						
						break;
					}
				}
				
				if(resources.Count <= 0) {
					posIsValid = true;
				}
				
				loops ++;
			}
			
			resource.transform.position = pos;
			resource.transform.parent = this.transform;
			resources.Add(resource);
		}
	}
}
