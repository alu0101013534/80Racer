using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class racecontrol : MonoBehaviour {


	public List<checkpoint> checkpoints;
	private int currentCheckpoint=0;
	public int laps=2;
	private int currentlaps=1;


	public TextMeshProUGUI lapsText;
	public TextMeshProUGUI racersText;
	public TextMeshProUGUI speedText;
	
  	public Rigidbody body;

	public int racers=3;
	// Use this for initialization
	void Start () {
		checkpoints[0].is_active=true;
		
		racersText.text = "test2";
		lapsText.text = "Laps " +currentlaps.ToString()+"/"+laps.ToString();
	}
	
	// Update is called once per frame
	void Update () {
		
		int speed= (int)(body.velocity.magnitude * 3.6);
		speedText.text = (speed).ToString()+"kmh";
	}

	public void Checkpoint()
	{
		
			Debug.Log("Checkpoint");
		checkpoints[currentCheckpoint].is_active=false;
		if(currentCheckpoint>=checkpoints.Count-1)
			currentCheckpoint=0;
		else 
			currentCheckpoint++;
		
			Debug.Log(currentCheckpoint);	
		if(currentCheckpoint==0)
			currentlaps++;
			
			lapsText.text = "Laps " + currentlaps.ToString()+"/"+laps.ToString();
			if(currentlaps >laps)
				{
			Debug.Log("fin carrera");
				}
		checkpoints[currentCheckpoint].is_active=true;
	}

	public void racerDeath(){
		racers--;

		if(racers<=0)
		{
			Debug.Log("fin carrera");
				
		}

	}
}
