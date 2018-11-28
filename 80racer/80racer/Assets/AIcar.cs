
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(Rigidbody))]
public class AIcar : MonoBehaviour
{
  Rigidbody body;
  float deadZone = 0.1f;
 
	public string myname;
  public float hoverForce = 9.0f;
  public float hoverHeight = 2.0f;
  public GameObject[] hoverPoints;

  public float forwardAcl = 100.0f;
  public float backwardAcl = 25.0f;
  float currThrust = 0.0f;

  public float turnStrength = 10f;
  float currTurn = 0.0f;

  public GameObject leftAirBrake;
  public GameObject rightAirBrake;

  int layerMask;

	public Transform shipBody;	
  	public float angleOfRoll = 30f;			//The angle that the ship "banks" into a turn
  
  public Transform path;
  public float maxSteerAngle=45f;
  private List<Transform> nodes;
  public int currentNode=0;

 private float turnAxis = 0;//Input.GetAxis("Horizontal");
  
    private float aclAxis = 0;//Input.GetAxis("Vertical");
  

  bool firstcheckpoint=true;
	private int currentCheckpoint=0;
	public int laps=2;
	private int currentlaps=1;
  void Start()
  {
    body = GetComponent<Rigidbody>();

    layerMask = 1 << LayerMask.NameToLayer("Characters");
    layerMask = ~layerMask;


	Transform[] pathTransforms=path.GetComponentsInChildren<Transform>();
	nodes =new List<Transform>();

		for (int i =0;i<pathTransforms.Length;i++)
		{
			if(pathTransforms[i] != transform){
				nodes.Add(pathTransforms[i]);
			}

		}
  }




  void Update()
  {

    // Main Thrust
    currThrust = 0.0f;
    if (aclAxis > deadZone)
      currThrust = aclAxis * forwardAcl;
    else if (aclAxis < -deadZone)
      currThrust = aclAxis * backwardAcl;

    // Turning
    currTurn = 0.0f;
    if (Mathf.Abs(turnAxis) > deadZone)
      currTurn = turnAxis;


      
		float angle = angleOfRoll * -Input.GetAxis("Horizontal");

		//Calculate the rotation needed for this new angle
		Quaternion bodyRotation = transform.rotation * Quaternion.Euler(0f, 0f, angle);
		//Finally, apply this angle to the ship's body
		shipBody.rotation = Quaternion.Lerp(shipBody.rotation, bodyRotation, Time.deltaTime * 10f);
  }

  void FixedUpdate()
  {
	ApplySteer();
	Drive();
	CheckWaypointDistance();
    //  Hover Force
    RaycastHit hit;
    for (int i = 0; i < hoverPoints.Length; i++)
    {
      var hoverPoint = hoverPoints [i];
      if (Physics.Raycast(hoverPoint.transform.position, 
                          -Vector3.up, out hit,
                          hoverHeight,
                          layerMask))
        body.AddForceAtPosition(Vector3.up 
          * hoverForce
          * (1.0f - (hit.distance / hoverHeight)), 
                                  hoverPoint.transform.position);
      else
      {
        if (transform.position.y > hoverPoint.transform.position.y)
          body.AddForceAtPosition(
            hoverPoint.transform.up * hoverForce,
            hoverPoint.transform.position);
        else
          body.AddForceAtPosition(
            hoverPoint.transform.up * -hoverForce,
            hoverPoint.transform.position);
      }
    }

    // Forward
    if (Mathf.Abs(currThrust) > 0)
      body.AddForce(transform.forward * currThrust);

    // Turn
    if (currTurn > 0)
    {
      body.AddRelativeTorque(Vector3.up * currTurn * turnStrength);
    } else if (currTurn < 0)
    {
      body.AddRelativeTorque(Vector3.up * currTurn * turnStrength);
    }
  }

	private void ApplySteer(){
		

		   Vector3 direction = nodes[currentNode].position - transform.position;
             Quaternion newlook_rotation = Quaternion.LookRotation(direction);
             transform.rotation = Quaternion.Slerp(transform.rotation, newlook_rotation, 40 * Time.deltaTime);
	}
  private void Drive(){
		aclAxis = 1;//Input.GetAxis("Vertical");


  
  }

  
  private void CheckWaypointDistance(){
	if(Vector3.Distance(transform.position,nodes[currentNode].position)<20.0f){
		if(currentNode==nodes.Count-1){
			currentNode=0;
		}
		else
		{
			currentNode++;
		}
		
		
	}
  
  }


  public void Checkpoint()
	{
		if(!firstcheckpoint){

		if(currentCheckpoint>=2)
			currentCheckpoint=0;
		else 
			currentCheckpoint++;
		
			Debug.Log(currentCheckpoint);	
		if(currentCheckpoint==0)
			currentlaps++;
			if(currentlaps >laps)
				{
				FindObjectOfType<racecontrol>().finished(myname);
				}
		}
		if(firstcheckpoint)
			firstcheckpoint=false;
	}
}
