using UnityEngine;
using System.Collections;

public class PlanetGenerator : MonoBehaviour {

	public GameObject planetPrefab;

	public float generationSize;
	public int planetCount;
	public float minSpacing;
	
	
	private int timeout = 250;

	// Use this for initialization
	void Start () {

		Generate();
	}
	
	public void Generate() {
		
		ArrayList planets = new ArrayList();
		GameObject planet;

		Vector2 pos = Vector2.zero;
		bool posIsValid = false;
		
		for(int i = 0; i <= planetCount; i++) {
			
			planet = (GameObject) GameObject.Instantiate(planetPrefab);

			int loops = 0;
			posIsValid = false;
			while(!posIsValid) {

				//Abort if no position can be found to prevent infinite loops.
				if(loops >= timeout) {
					break;
				}
				
				//The position is assumed innocent unless prooven otherwise.
				pos = Random.insideUnitCircle * generationSize;
				posIsValid = true;

				foreach(GameObject g in planets) {
					if(Vector2.Distance(g.transform.position, pos) < minSpacing) {
						posIsValid = false;
		
						break;
					}
				}
				
				if(planets.Count <= 0) {
					posIsValid = true;
				}

				loops ++;
			}
			
			planet.transform.position = pos;
			planet.transform.parent = this.transform;
			planets.Add(planet);
		}
	}
}