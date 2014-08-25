using UnityEngine;
using System.Collections;

public class PLACER : MonoBehaviour {

	private static PLACER placer;
	
	public enum PlacementMode {
		Leaf,
		Stem
	};
	
	private PlacementMode mode;

	// Use this for initialization
	void Start () {
		placer = this;
		
		mode = PlacementMode.Leaf;
	}
	
	public static PLACER getPLACER() {
		return placer;
	}
	
	public void setMode(PlacementMode placementMode) {
		mode = placementMode;
		

	}
	
	public void startPlacing() {
	
		switch (mode) {
		case PlacementMode.Leaf:
			this.GetComponentsInChildren<StemPositioner>(true)[0].gameObject.SetActive(false);
			this.GetComponentsInChildren<LeafPositioner>(true)[0].gameObject.SetActive(true);
			break;
		case PlacementMode.Stem:
			this.GetComponentsInChildren<StemPositioner>(true)[0].gameObject.SetActive(true);
			this.GetComponentsInChildren<LeafPositioner>(true)[0].gameObject.SetActive(false);
			break;
			
		}
	}
	
	public PlacementMode getMode() {
		return mode;
	}
}
