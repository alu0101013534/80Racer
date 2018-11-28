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


/* 	void OnTriggerEnter(Collider other){
		if (other.tag == "Player") {
			FindObjectOfType<racecontrol>().Checkpoint();
		}
	}
*/
	void OnTriggerEnter(Collider other){
		if (other.tag == "Player") {
			
			Debug.Log("enter");
		}
	}
	void OnTriggerStay(Collider other){
		if (other.tag == "Player") {
			Debug.Log("within");
		}
	}
	void OnTriggerExit(Collider other){
		if (other.tag == "Player") {
			
			Debug.Log("exit");
			FindObjectOfType<racecontrol>().Checkpoint();
		}
	}
	
}
