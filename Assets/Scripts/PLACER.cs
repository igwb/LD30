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
		
		mode = PlacementMode.Stem;
		
		HUD.getHUD().leftPanel.addButton("stem","Stem");
		HUD.getHUD().leftPanel.addButton("leaf","Leaf");
		
		HUD.getHUD().getButtonHandler("stem").ButtonPressEvent += () => setMode(PlacementMode.Stem);
		HUD.getHUD().getButtonHandler("leaf").ButtonPressEvent += () => setMode(PlacementMode.Leaf);

	}
	
	public void OnEnable() {
		
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
