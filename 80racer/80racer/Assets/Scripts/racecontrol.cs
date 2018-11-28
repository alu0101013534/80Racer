using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

using TMPro;
public class racecontrol : MonoBehaviour {


	public List<checkpoint> checkpoints;
	private int currentCheckpoint=0;
	public int laps=2;
	private int currentlaps=1;

	public GameObject PausePanel;
	
	private bool IsPaused =false;

	public TextMeshProUGUI lapsText;
	public TextMeshProUGUI racersText;
	public TextMeshProUGUI speedText;

	
	public TextMeshProUGUI guiTextCountdown;

	public int countMax=3;  //max countdown number
	private int countDown;  //current countdown number
	
	bool isCountingDown;
	bool isRaceOver=false;
  	public Rigidbody body;

	public int racers=3;
	// Use this for initialization
	void Start () {
		checkpoints[0].is_active=true;
		
		racersText.text = "";
		lapsText.text = "Laps " +currentlaps.ToString()+"/"+laps.ToString();
		

		Cursor.lockState = CursorLockMode.Locked;
		PausePanel.SetActive(false);
		Time.timeScale = 0f;
		guiTextCountdown.enabled=true;
		StartCoroutine(CountdownFunction());
	}
	
	// Update is called once per frame
	void Update () {
		
		int speed= (int)(body.velocity.magnitude * 3.6);
		speedText.text = (speed).ToString()+"kmh";


		if (Input.GetKeyDown(KeyCode.Escape) )
        {
			if(!isCountingDown && !isRaceOver)
			{
				IsPaused = !IsPaused;
				PausePanel.SetActive (IsPaused);

				if (IsPaused)
				{
					Cursor.lockState = CursorLockMode.None;
					Time.timeScale = 0f;
				} 
				else
				{
					
					Cursor.lockState = CursorLockMode.Locked;
					
					guiTextCountdown.enabled=true;
					//Call the CountdownFunction
					
					isCountingDown=true;
					StartCoroutine(CountdownFunction());
				}
			}
		}


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
	public void Resume()
    {
		IsPaused = false;
		PausePanel.SetActive (IsPaused);
		
		guiTextCountdown.enabled=true;
		//Call the CountdownFunction
		isCountingDown=true;
		StartCoroutine(CountdownFunction());
	}

	
	public void Quit()
    {
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
	}






public static IEnumerator WaitForRealSeconds( float delay )
	{
		float start = Time.realtimeSinceStartup;
		while( Time.realtimeSinceStartup < start + delay)
		{
			yield return null;
		}
	}

	IEnumerator CountdownFunction() {
		//start the countdown
		for(countDown = countMax; countDown>-1;countDown--){
			if(countDown!=0){
				//display the number to the screen via the GUIText
				guiTextCountdown.text = countDown.ToString();
				//add a one second delay
				yield return StartCoroutine( WaitForRealSeconds( 1.0f ) );  
			}
			else{
				guiTextCountdown.text = "GO!";
				yield return StartCoroutine( WaitForRealSeconds( 1.0f ) );  
			}
		}
		//enable all the scripts and animation once the count is down
		/*MonoBehaviour[] scriptComponentsGameControl = gameObject.GetComponents<MonoBehaviour>();   
		foreach(MonoBehaviour script in scriptComponentsGameControl) {
			script.enabled = true;
		}*/


		Time.timeScale=1;
		//disable the GUIText once the countdown is done with
		guiTextCountdown.enabled = false;
		
		isCountingDown=false;


	}
}
