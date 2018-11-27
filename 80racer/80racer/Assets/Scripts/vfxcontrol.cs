using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class vfxcontrol : MonoBehaviour {



	public ParticleSystem engineThrusterR;	//The right thruster particles
				//A reference to the ship's VehicleMovement script
	AudioSource engineAudio;	
	
	
  public Rigidbody body;
		ParticleSystem.MainModule mainModule;	//A module for storing and changing the thruster particles
float thrusterStartLife;				//The start life that the thrusters normally have
	
	// Use this for initialization
	void Start () {


		mainModule = engineThrusterR.main;
		thrusterStartLife = mainModule.startLifetime.constant;
	}
	
	// Update is called once per frame
	void Update () {
		UpdateThrusterParticles();
	}

	void UpdateThrusterParticles()
	{
		//Calculate the lifetime we want the thruster's particles to have
		float currentLifeTime = thrusterStartLife * (body.velocity.magnitude/35);

		//If the thrusters are powered on at all...
		if (currentLifeTime > 0f)
		{
			//...play the particle systems...
			engineThrusterR.Play();

			//...and then update the particle life for the right thruster
			mainModule = engineThrusterR.main;
			mainModule.startLifetime = currentLifeTime;
		}
		//...Otherwise stop the particle effects
		else
		{
			engineThrusterR.Stop();
		}
	}
}
