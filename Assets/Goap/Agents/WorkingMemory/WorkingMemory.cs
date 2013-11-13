﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class WorkingMemory {
	
	
	
	private Dictionary<string, List<WorkingMemoryValue>> knownFacts;
	private BlackBoard blackBoard;
	
	public WorkingMemory(){
	
		knownFacts = new Dictionary<string, List<WorkingMemoryValue>>();
		blackBoard = BlackBoard.Instance;
	}
	
	public void setFact(string name, WorkingMemoryValue factValue)
	{
		if(!knownFacts.ContainsKey(name)){
			List<WorkingMemoryValue> tempList = new List<WorkingMemoryValue>();
			tempList.Add(factValue);
			knownFacts.Add(name, tempList);
		} else {
		
			knownFacts[name].Add(factValue);
		}
		
		//Check if it is a globaly important fact that everyone needs to know about, then send it to the blackboard
		if(name == "Wood" || name == "Stone" || name == "Building"){
			
			blackBoard.setFact(name, factValue);
			
		}
	}
	
	public List<WorkingMemoryValue> getFact(string name)
	{
		//If the fact is not in WorkingMemory the check if it is in the blackboard 		
		if(containsFact(name))
		{
			return knownFacts[name]; 
		} else {
			//check in blackboard
			List<WorkingMemoryValue> temp = blackBoard.getFact(name);
			
			
			return temp; //OBS! returnerar bara första värdet ur listan! 
			
			
		}
		
		
	}
	
	public bool containsFact(string name)
	{
		return knownFacts.ContainsKey(name);
	}
	
	public void removeFact(string name)
	{
		knownFacts.Remove(name);
	}

}