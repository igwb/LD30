using UnityEngine;
using System.Collections;

public class StemPositioner : MonoBehaviour {

	public GameObject stemPrefab;

	public float waterCostStem;
	public float energyCostStem;

	public float waterCostRoot;
	public float energyCostRoot;

	public Vector2 startPos;
	public Vector2 endPos;

	public float maxLength;

	private float minYScale = 0.25f;
	private float maxYScale;
	
	private bool rootMode;
	private bool canPlace;

	private float waterCost;
	private float energyCost;

	ArrayList objectsColliding = new ArrayList();
	ArrayList resourcesColliding = new ArrayList();
	ArrayList planetsColliding = new ArrayList();

	// Use this for initialization
	void Start () {
		endPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		maxYScale = maxLength / GetComponent<SpriteRenderer>().sprite.bounds.size.y;
	}

	void OnEnable() {

		objectsColliding.Clear();
		resourcesColliding.Clear();
		planetsColliding.Clear();
		
		startPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
	}

// Update is called once per frame
	void Update () {
		
		string hudText;

        //Abort if Escape, Space or Delete is pressed.
        if(Input.GetKeyDown(KeyCode.Escape) | Input.GetKeyDown(KeyCode.Space) | Input.GetKeyDown(KeyCode.Delete)) {
        	Abort();
        }
		
		//Calculate the cost
		float length = Mathf.Min(Vector2.Distance(startPos, endPos), maxLength);
		if(rootMode) {
			waterCost = length * waterCostRoot;
			energyCost = length * energyCostRoot;
		} else {
			waterCost = length * waterCostStem;
			energyCost = length * energyCostRoot;
		}

		//Check if placing the object is affordable
		bool affordable;
		affordable = (HUD.getHUD().bottomPanel.energyValue >= energyCost) && (HUD.getHUD().bottomPanel.waterValue >= waterCost);
		
		hudText = "Water: " + Mathf.Ceil(waterCost) + "\n" + "Energy: " + Mathf.Ceil(energyCost);
		
		if(!affordable) {
			hudText += "\nYou need more resources!";
			
		}

		canPlace = affordable;

		//Check if object can be placed at it's current position
		rootMode = false;
        if(canPlace) {
			foreach(Collider2D c in planetsColliding) {
				bool startPosInPlanet = c.bounds.Contains(startPos);
				bool endPosInPlanet = c.bounds.Contains(endPos);
				
				Collider2D rootCollider = c.gameObject.transform.Find("RootZone").GetComponent<CircleCollider2D>();
				bool startPosInRootZone = rootCollider.bounds.Contains(startPos);
				bool endPosInRootZone = rootCollider.bounds.Contains(endPos);

				if((startPosInPlanet && endPosInRootZone) | (startPosInRootZone && endPosInPlanet) | (startPosInRootZone && endPosInRootZone)) {
					rootMode = true;
				} else if((!startPosInPlanet && endPosInRootZone) | (startPosInRootZone && !endPosInPlanet)) {
					
					hudText = "You need to be closer to the border of the planet to enter or exit it.";
					canPlace = false;
					rootMode = false;
				} else {
					canPlace = true;
					rootMode = false;
				}
			}
			canPlace = canPlace && objectsColliding.Count == 0;
		}

        //Place if possible when left mouse button is released
        if(Input.GetMouseButtonUp(0)) {

            if(canPlace) {
           		EndPlacing();
        	} else {
            	Abort();
       		}
        }

		//Update the graphics
		if(canPlace) {
			if(rootMode) {
				GetComponent<SpriteRenderer>().color = new Color32(98,56,56,255);
			} else {
				GetComponent<SpriteRenderer>().color = Color.white;
			}
		} else {
			GetComponent<SpriteRenderer>().color = Color.red;
		}


        endPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

		ScaleTowardsEndpos();
		RotateTowardsEndpos();

		//Update the tooltip
        HUD.getHUD().getTextHandler("Tool Tip").setText(hudText);
    }

    private void EndPlacing() {

        GameObject stem;
        stem = (GameObject) GameObject.Instantiate(stemPrefab);

        stem.transform.position = (Vector2) this.transform.position;
        stem.transform.localScale = this.transform.localScale;
        stem.transform.rotation = this.transform.rotation;
        
		stem.GetComponent<SpriteRenderer>().color = GetComponent<SpriteRenderer>().color;

		//Use resources
		HUD.getHUD().bottomPanel.energyValue -= energyCost;
		HUD.getHUD().bottomPanel.waterValue -= waterCost;

        HUD.getHUD().getTextHandler("Tool Tip").setText("");

		//Connect resources
		if(rootMode) {
			foreach(Collider2D c in resourcesColliding) {
				if(c != null) {
					c.GetComponent<ResourceScript>().Connect();
				}
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
				planetsColliding.Add(other);
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
				planetsColliding.Remove(other);
				break;
			default:
				break;
		}
	}
}