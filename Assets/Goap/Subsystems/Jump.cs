using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class Jump : MonoBehaviour {
	
	CharacterController controller;
	public float jumpHeight = 8.0f;
	public float gravity = 20.0f;
	Vector3 moveDirection;
	
	void Start()
	{
		
	}
	
	void Update()
	{
		
		controller = GetComponent<CharacterController>();
		moveDirection = new Vector3(0,jumpHeight,0);
		
		// Apply gravity
		moveDirection.y -= gravity * Time.deltaTime;
		
		// Move the controller
		controller.Move(moveDirection * Time.deltaTime);
		
	}
	
}