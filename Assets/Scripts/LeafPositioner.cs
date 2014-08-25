using UnityEngine;
using System.Collections;

public class LeafPositioner : MonoBehaviour {

    public GameObject leafPrefab;

    public Vector2 startPos;
    public Vector2 endPos;
    
	private bool canPlace;
    
    ArrayList objectsColliding = new ArrayList();
    ArrayList lightsColliding = new ArrayList();

    // Use this for initialization
    void Start () {

    }

	void OnEnable() {
        objectsColliding.Clear();
        lightsColliding.Clear();
		startPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
	}

    // Update is called once per frame
    void Update () {

        endPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);


        //Abort if Escape, Space or Delte is pressed.
        if(Input.GetKeyDown(KeyCode.Escape) | Input.GetKeyDown(KeyCode.Space) | Input.GetKeyDown(KeyCode.Delete)) {
            Abort();
        }

        //Check if object can be placed at it's current position

        if(objectsColliding.Count == 0) {
            if(lightsColliding.Count != 0) {
                foreach (Collider2D c in lightsColliding) {
                    
                }
            } else {
                canPlace = false;
            }
        } else {
            canPlace = false;
        }

        //Place if possible when left mouse button is released
        if(Input.GetMouseButtonUp(0)) {
            if(canPlace) {
                EndPlacing();
            } else {
                Abort();
            }
        }

        if(canPlace) {
                GetComponent<SpriteRenderer>().color = Color.white;
            } else {
                GetComponent<SpriteRenderer>().color = Color.red;
            }

         RotateTowardsEndpos();
    }

    private void EndPlacing() {

        GameObject leaf;
        leaf = (GameObject) GameObject.Instantiate(leafPrefab);

        leaf.transform.position = (Vector2) this.transform.position;
        leaf.transform.localScale = this.transform.localScale;
        leaf.transform.rotation = this.transform.rotation;
        
        this.gameObject.SetActive(false);
    }

    private void Abort() {

        this.gameObject.SetActive(false);
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

        if(other.tag == "Sunlight") {
            lightsColliding.Add(other);
        } else {
            objectsColliding.Add(other);
        }
    }

    void OnTriggerExit2D(Collider2D other) {

        if(other.tag == "Sunlight") {
			lightsColliding.Remove(other);
        } else {
            objectsColliding.Remove(other);
        }
    }
}