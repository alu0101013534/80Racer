using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class menufloor : MonoBehaviour {


public float ScrollX= 0.5f;
public float ScrollY= 0.5f;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
		float OffsetX =Time.time *this.ScrollX;
		float OffsetY =Time.time *this.ScrollY;

		GetComponent<Renderer>().material.mainTextureOffset=new Vector2(OffsetX,OffsetY);

	}
}
