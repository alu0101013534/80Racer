using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class checkpoint : MonoBehaviour {


	public bool is_active = false;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	void OnTriggerExit(Collider other){
		if (other.tag == "Player" && is_active ) {
			
			Debug.Log("exit");
			FindObjectOfType<racecontrol>().Checkpoint();
		}
		if (other.tag == "AI" && is_active ) {
			
			Debug.Log("AI exit");
			FindObjectOfType<racecontrol>().Checkpoint();
		}
	}
	
}
