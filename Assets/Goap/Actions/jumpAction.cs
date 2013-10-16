using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class jumpAction : Action {
	
	public jumpAction()
	{
		
		this.preConditions = new WorldState();
		this.preConditions.setProperty("needToJump", new WorldStateValue(true));
		
		this.postConditions = new WorldState();
		this.postConditions.setProperty("hasJumped", new WorldStateValue(true));
		
		this.cost = 4.0f;
		
		this.agentType.Add("normal");
		
	}
	
}
