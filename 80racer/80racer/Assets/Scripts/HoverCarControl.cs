using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody))]
public class HoverCarControl : MonoBehaviour
{
  Rigidbody body;
  float deadZone = 0.1f;

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

  void Start()
  {
    body = GetComponent<Rigidbody>();

    layerMask = 1 << LayerMask.NameToLayer("Characters");
    layerMask = ~layerMask;
  }

  void OnDrawGizmos()
  {

    //  Hover Force
    RaycastHit hit;
    for (int i = 0; i < hoverPoints.Length; i++)
    {
      var hoverPoint = hoverPoints [i];
      if (Physics.Raycast(hoverPoint.transform.position, 
                          -Vector3.up, out hit,
                          hoverHeight, 
                          layerMask))
      {
        Gizmos.color = Color.blue;
        Gizmos.DrawLine(hoverPoint.transform.position, hit.point);
        Gizmos.DrawSphere(hit.point, 0.5f);
      } else
      {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(hoverPoint.transform.position, 
                       hoverPoint.transform.position - Vector3.up * hoverHeight);
      }
    }
  }
	
  void Update()
  {

    // Main Thrust
    currThrust = 0.0f;
    float aclAxis = Input.GetAxis("Vertical");
    if (aclAxis > deadZone)
      currThrust = aclAxis * forwardAcl;
    else if (aclAxis < -deadZone)
      currThrust = aclAxis * backwardAcl;

    // Turning
    currTurn = 0.0f;
    float turnAxis = Input.GetAxis("Horizontal");
    if (Mathf.Abs(turnAxis) > deadZone)
      currTurn = turnAxis;
  }

  void FixedUpdate()
  {

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
}
