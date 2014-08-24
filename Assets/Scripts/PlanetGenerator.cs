using UnityEngine;
using System.Collections;

public class PlanetGenerator : MonoBehaviour {

	public GameObject planetPrefab;
	public GameObject sunPrefab;

	public int sunCount;
	
	public float generationSize;
	public float minPlanetSpacing;
	public float minClusterSpacing;
	
	public float clusterDimension;
	public float planetsPerCluster;


	private int timeout = 250;

	private ArrayList celestialBodys = new ArrayList();

	// Use this for initialization
	void Start () {

		Generate();
	}
	
	public void Generate() {
		
		GameObject planet;
		GameObject sun;
		Vector2 planetPos;
		Vector2 sunPos;

		for(int i = 0; i < sunCount; i++) {
			
			sunPos = getValidPos(Vector2.zero, minClusterSpacing, generationSize - clusterDimension);
			if(sunPos == Vector2.zero) {
				continue;
			}

			sun = (GameObject) GameObject.Instantiate(sunPrefab);

			sun.transform.position = sunPos;
			sun.transform.parent = this.transform;
			sun.transform.tag = "Sun";
			celestialBodys.Add(sun);

			for(int j = 0; j < planetsPerCluster; j++) {

				planetPos = getValidPos(sunPos, minPlanetSpacing, clusterDimension);
				if(planetPos == Vector2.zero) {
					continue;
				}

				planet = (GameObject) GameObject.Instantiate(planetPrefab);

				planet.transform.position = planetPos;
				planet.transform.parent = sun.transform;
				planet.transform.tag = "Planet";
				celestialBodys.Add(planet);
				
			}


		}
	}

	private Vector2 getValidPos(Vector2 origin, float minSpacing, float maxSize) {
		int loops = 0;

		Vector2 result = Vector2.zero;

		bool posIsValid = false;
		while(!posIsValid) {

				//Abort if no position can be found to prevent infinite loops.
				if(loops >= timeout) {
					return Vector2.zero;
				}
				
				//The position is assumed innocent unless prooven otherwise.
				result = Random.insideUnitCircle * maxSize;
				result += origin;
				posIsValid = true;

				foreach(GameObject g in celestialBodys) {
					if(Vector2.Distance(g.transform.position, result) < minSpacing) {
						posIsValid = false;

						break;
					}
				}
				
				if(celestialBodys.Count <= 0) {
					return result;
				}

				loops ++;
			}
			
			return result;
		}
	}