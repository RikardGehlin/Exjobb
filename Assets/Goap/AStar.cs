﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AStar {
	
	private List<AStarNode> openList;
	private List<AStarNode> closedList;
	private List<AStarNode> pathList;
	
	public AStar()
	{
		openList = new List<AStarNode>();
		closedList = new List<AStarNode>();
		pathList = new List<AStarNode>();
	}
	
	public List<AStarNode> Run(AStarNode startNode, AStarNode endNode)
	{
		AStarNode currentNode = startNode;
		//Debug.Log("************ASTAR RUNNING************");
		//Specialfall för första noden eftersom den INTE är något action utan en postCondition, räknar med att den aldrig kommer va mål!!!
		List<AStarNode> neighbourList = currentNode.getNeighbours(true); //ger lista med actions som har startNodes preCondition 	
		
		
		foreach(AStarNode node in neighbourList)
		{

			node.setF(ActionManager.Instance.getAction(node.getName()).cost + HeuristicCost(node, endNode));
			node.setParent(startNode);
			openList.Add(node); //Lägg till grannarna för start i openList
		}
		

	
		//Ta ut nod med lägsta f ur openList
		while(openList.Count > 0)
		{
			int lowInd = 0;
			for(int i = 0; i < openList.Count; i++) 
			{
				if(openList[i].getF() < openList[lowInd].getF()) 
				{ 
					lowInd = i; 
				}
			}
			currentNode = openList[lowInd];
			openList.Remove(currentNode);
			closedList.Add(currentNode);
			
			//Debug.Log("currentNode är nu: " + currentNode.getName());
			
			//Check if we have reached the target 
			if(ActionManager.Instance.getAction(currentNode.getName()).preConditions.contains(endNode.getWorldState()))
			{
				return CreatePath(currentNode, startNode);
			}
			
			
				
			//if multiple preconditions or multiple amounts of the same condition
			if(ActionManager.Instance.getAction(currentNode.getName()).preConditions.getProperties().Count > 1 || ActionManager.Instance.getAction(currentNode.getName()).preConditions.containsAmount())
			{
				List<List<AStarNode>> lists = new List<List<AStarNode>>();
				
				foreach(KeyValuePair<string, WorldStateValue> pair in ActionManager.Instance.getAction(currentNode.getName()).preConditions.getProperties())
				{
					//Debug.Log("GDDDDDDDDDDDDDDDDDDDDDDDDDDDDD" + pair.Value.propertyValues["amount"]);
					for(int j = 0; j < (int)pair.Value.propertyValues["amount"]; j++)
					{
						//Debug.Log ("IIIIIIIIIIIIII: " + j);
						AStar astar2 = new AStar();
						AStarNode tempNode = new AStarNode();
						
						
						WorldState tempWorldState = new WorldState();
						tempWorldState.setProperty(pair.Key, pair.Value);
						tempNode.setWorldState(tempWorldState);
						
						List<AStarNode> tempList = new List<AStarNode>();
						//List<AStarNode> test = astar2.Run(tempNode, endNode);
						//test.Reverse();
						tempList.AddRange(astar2.Run(tempNode, endNode));
						
						tempList[tempList.Count-1].setParent(currentNode); //nyligen tillagd 
						
						if(tempList.Count > 0)
						{
							lists.Add(tempList);
						}
						else 
						{
							if(!tempWorldState.contains(endNode.getWorldState()))
							{
								return new List<AStarNode>();
							}
						}
					}
				}
				
				//Sorts by cost to make the plan prioritize actions with small costs
				lists.Sort((a, b) => ActionManager.Instance.getAction(a[0].getName()).cost.CompareTo(ActionManager.Instance.getAction(b[0].getName()).cost));
				
				foreach(List<AStarNode> list in lists)
				{
					//add to final path
					pathList.AddRange(list);
				}
				return CreatePath(currentNode, startNode);
			}
			
			
			neighbourList = currentNode.getNeighbours(false);
			//Debug.Log ("antal grannar: " + neighbourList.Count);
			for(int i = 0; i < neighbourList.Count; i++)
			{
				AStarNode currentNeighbour = neighbourList[i];
				if(closedList.Contains(currentNeighbour))
				{
					//not a valid node
					continue;
				}
				
				if(!openList.Contains(currentNeighbour))
				{
					//Debug.Log("namnet på grannen: " + currentNeighbour.getName());
					//Debug.Log("kontroll om currentneighbour inte finns i openlist: " + !openList.Contains(currentNeighbour));

					currentNeighbour.setH(HeuristicCost(currentNeighbour, endNode));
					currentNeighbour.setG(currentNode.getG() + currentNeighbour.getTime()); 
					currentNeighbour.setF(currentNeighbour.getG() + currentNeighbour.getH());
					openList.Add(currentNeighbour);
				}	
			}
		}
		//Debug.Log ("det blev fel");
		return new List<AStarNode>();
	}
	
	public float HeuristicCost(AStarNode currentNeighbour, AStarNode endNode){
		// See list of heuristics: http://theory.stanford.edu/~amitp/GameProgramming/Heuristics.html
        /*var d1 = Math.abs (pos1.x - pos0.x);
        var d2 = Math.abs (pos1.y - pos0.y);
        return d1 + d2;*/
		float cost = ActionManager.Instance.getAction(currentNeighbour.getName()).cost + Random.Range(-3, 3); //could it be dangerous to use negative values?
		
		return cost;
	}
	
	public List<AStarNode> CreatePath(AStarNode currentNode, AStarNode endNode){
		
		while(!(ActionManager.Instance.getAction(currentNode.getName()).postConditions.contains(endNode.getWorldState())))
		{
			if(Object.ReferenceEquals(currentNode.getParent().getName(), null)){
				break;
			}
			else
			{
				pathList.Add(currentNode);
				currentNode = currentNode.getParent();
			}
		}
		pathList.Add(currentNode);

		return pathList;
	}
}