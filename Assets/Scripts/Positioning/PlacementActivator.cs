﻿using UnityEngine;
using System.Collections;

public class PlacementActivator : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void OnMouseDown() {

		PLACER.getPLACER().startPlacing();
	}
}
