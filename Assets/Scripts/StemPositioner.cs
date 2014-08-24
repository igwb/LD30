using UnityEngine;
using System.Collections;

public class StemPositioner : MonoBehaviour {


	public GameObject stemPrefab;

	public Vector2 startPos;
	public Vector2 endPos;

	public float maxLength;
	public float maxRootPlanetDistance;

	private float minYScale = 0.25f;
	private float maxYScale;

	public bool locked;
	
	public bool rootMode;

	ArrayList objectsColliding = new ArrayList();
	ArrayList resourcesColliding = new ArrayList();

// Use this for initialization
void Start () {
	endPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
	maxYScale = maxLength / GetComponent<SpriteRenderer>().sprite.bounds.size.y;
}

// Update is called once per frame
void Update () {
	
	if(locked) {
		return;
	}

        //Abort if Escape, Space or Delete is pressed.
        if(Input.GetKeyDown(KeyCode.Escape) | Input.GetKeyDown(KeyCode.Space) | Input.GetKeyDown(KeyCode.Delete)) {
        	Abort();
        }

        //Lock the stem in it's current position when the left mouse button is released.
        if(Input.GetMouseButtonUp(0)) {

        	EndPlacing();

        	if(objectsColliding.Count > 0 && !rootMode) {

        		Abort();
        	}
        }

        if(objectsColliding.Count > 0) {
        	float startPosToCollider = Vector2.Distance(((Collider2D) objectsColliding[0]).transform.position,startPos);
        	float endPosToCollider = Vector2.Distance(((Collider2D) objectsColliding[0]).transform.position,endPos);
			
			if(startPosToCollider <= maxRootPlanetDistance + 0.1f &&  endPosToCollider<= maxRootPlanetDistance + 0.1f) {
				
        		rootMode = true;
        	} else {
        		rootMode = false;
        		
        	}
        } else {
        	rootMode = false;
        	
        }



		//Update the graphics


		if(objectsColliding.Count != 0) {
			GetComponent<SpriteRenderer>().color = Color.red;
		} else {
			if(rootMode) {
				GetComponent<SpriteRenderer>().color = new Color32(98,56,56,255);
			} else {
				GetComponent<SpriteRenderer>().color = Color.white;
			}
		}


        endPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

		ScaleTowardsEndpos();
		RotateTowardsEndpos();
    }

    private void EndPlacing() {

        GameObject stem;
        stem = (GameObject) GameObject.Instantiate(stemPrefab);

        stem.transform.position = (Vector2) this.transform.position;
        stem.transform.localScale = this.transform.localScale;
        stem.transform.rotation = this.transform.rotation;
        
		//Connect resources
		if(rootMode) {
			foreach(Collider2D c in resourcesColliding) {
				c.GetComponent<ResourceScript>().Connect();
			}
		}

        this.gameObject.SetActive(false);
    }

    private void Abort() {

        this.gameObject.SetActive(false);
    }

	private void ScaleTowardsEndpos(){
		//Figure out the scaling
		Vector3 scale;
		float yScale;

		yScale = Vector2.Distance(startPos, endPos) / GetComponent<SpriteRenderer>().sprite.bounds.size.y;
		
		//Do not scale larger than allowed
		yScale = Mathf.Max(yScale, minYScale);
		yScale = Mathf.Min(yScale, maxYScale);

		scale = new Vector3(transform.localScale.x, yScale, transform.localScale.z);
		transform.localScale = scale;
	}

	private void RotateTowardsEndpos() {

        //Figure out the rotation
        float deltaX = endPos.x - startPos.x;
        float deltaY = endPos.y - startPos.y;

        float rotation = Mathf.Atan2(deltaX, deltaY) * (180 / Mathf.PI);
        rotation *= -1;

        //Position the first end at the startPos first.
        float height = GetComponent<SpriteRenderer>().sprite.bounds.size.y * transform.localScale.y;
        this.transform.position = new Vector2(startPos.x, startPos.y + (height / 2));

        //Apply the rotation
        this.transform.eulerAngles = new Vector3(0,0,0);
        this.transform.RotateAround(startPos,Vector3.forward,rotation);
    }

    void OnMouseDown() {
    	GameObject.Find("StemCreator").GetComponent<StemCreator>().StartPlacing();
    }

    void OnTriggerEnter2D(Collider2D other) {
		
		switch (other.tag) {
			case "Resource":
					resourcesColliding.Add(other);
				break;
			case "Sun":
					objectsColliding.Add(other);
				break;
			case "Sunlight":
				//Nothing to do here
				break;
			case "Planet":

				break;
			default:
				break;
		}
    }

    void OnTriggerExit2D(Collider2D other) {

    	switch (other.tag) {
			case "Resource":
				resourcesColliding.Remove(other);
				break;
			case "Sun":
				objectsColliding.Remove(other);
				break;
			case "Sunlight":
				//Nothing to do here
				break;
			case "Planet":

				break;
			default:
				break;
		}
	}
}
