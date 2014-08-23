using UnityEngine;
using System.Collections;

public class StemPositioner : MonoBehaviour {

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

        //Abort if Escape, Space or Delte is pressed.
        if(Input.GetKeyDown(KeyCode.Escape) | Input.GetKeyDown(KeyCode.Space) | Input.GetKeyDown(KeyCode.Delete)) {
        	GameObject.Find("StemCreator").GetComponent<StemCreator>().EndPlacing();
        	Destroy(gameObject);
        }

        //Lock the stem in it's current position when the left mouse button is released.
        if(Input.GetMouseButtonUp(0)) {

        	GameObject.Find("StemCreator").GetComponent<StemCreator>().EndPlacing();

        	if(objectsColliding.Count > 0 && !rootMode) {

        		Destroy(gameObject);
        	}

        	locked = true;
        }

        if(objectsColliding.Count > 0) {
			if(Vector2.Distance(((Collider2D) objectsColliding[0]).transform.position,startPos) <= maxRootPlanetDistance && Vector2.Distance(((Collider2D) objectsColliding[0]).transform.position,endPos) <= maxRootPlanetDistance) {
				GetComponent<SpriteRenderer>().color = new Color32(98,56,56,255);
        		rootMode = true;
        	} else {
        		rootMode = false;
        		GetComponent<SpriteRenderer>().color = Color.red;
        	}
        } else {
        	rootMode = false;
        	GetComponent<SpriteRenderer>().color = Color.white;
        }

        endPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

		//Figure out the scaling
		Vector3 scale;
		float yScale;

		yScale = Vector2.Distance(startPos, endPos) / GetComponent<SpriteRenderer>().sprite.bounds.size.y;
		yScale = Mathf.Min(maxYScale, Mathf.Max(yScale, minYScale));

		scale = new Vector3(transform.localScale.x, yScale, transform.localScale.z);
		transform.localScale = scale;
		
	    //Figure out the rotation
	    float deltaX = endPos.x - startPos.x;
	    float deltaY = endPos.y - startPos.y;

	    float rotation = Mathf.Atan2(deltaX, deltaY) * (180 / Mathf.PI);
	    rotation *= -1;

        //Position the first end at the startPos first.
        this.transform.position = new Vector2(startPos.x, startPos.y + ((GetComponent<SpriteRenderer>().sprite.bounds.size.y * yScale) / 2));

        //Apply the rotation
        this.transform.eulerAngles = new Vector3(0,0,0);
        this.transform.RotateAround(startPos,Vector3.forward,rotation);
    }

    void OnMouseDown() {
    	GameObject.Find("StemCreator").GetComponent<StemCreator>().StartPlacing();
    }

    void OnTriggerEnter2D(Collider2D other) {
		if(other.tag != "Resource") {
    	 	objectsColliding.Add(other);
    	} else {
			resourcesColliding.Add(other);
		}

    }

    void OnTriggerExit2D(Collider2D other) {
		if(other.tag != "Resource") {
    		objectsColliding.Remove(other);
		} else {
			resourcesColliding.Remove(other);
		}
	}
	
}
