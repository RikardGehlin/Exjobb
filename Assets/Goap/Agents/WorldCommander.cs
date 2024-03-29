﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[AddComponentMenu("Commanders/WorldCommander")]

public class WorldCommander: MonoBehaviour
{
	void Start()
	{
		//Create resources
		GameObject resources = new GameObject();
		resources.name = "Resources";
		resources.layer = 9;
		
		for(int i = 0; i < 2; i++)
		{
			GameObject prefab = (GameObject)Instantiate(Resources.Load(("Prefabs/Resource"),typeof(GameObject)));
			
			if(i == 1)
			{
				prefab.transform.position = new Vector3(-20 + Random.Range(-25, 25), 0.25f, -20 + Random.Range(-25, 25));
			}
			else
			{
				prefab.transform.position = new Vector3(20 + Random.Range(-25, 25), 0.25f, 20 + Random.Range(-25, 25));
			}
			
			
			prefab.transform.parent = resources.transform;
			foreach(Transform child in prefab.transform)
    		{
				child.renderer.material.color = Color.red;
			}
			prefab.AddComponent("ResourceObject");
			prefab.tag = "RedSource";
		}
		for(int i = 0; i < 2; i++)
		{
			GameObject prefab = (GameObject)Instantiate(Resources.Load(("Prefabs/Resource"),typeof(GameObject)));
			if(i == 1)
			{
				prefab.transform.position = new Vector3(-20 + Random.Range(-25, 25), 0.25f, -20 + Random.Range(-25, 25));
			}
			else
			{
				prefab.transform.position = new Vector3(20 + Random.Range(-25, 25), 0.25f, 20 + Random.Range(-25, 25));
			}
			prefab.transform.parent = resources.transform;
			foreach(Transform child in prefab.transform)
    		{
				child.renderer.material.color = Color.blue;
			}
			prefab.AddComponent("ResourceObject");
			prefab.tag = "BlueSource";
		}
		for(int i = 0; i < 2; i++)
		{
			GameObject prefab = (GameObject)Instantiate(Resources.Load(("Prefabs/Resource"),typeof(GameObject)));
			if(i == 1)
			{
				prefab.transform.position = new Vector3(-20 + Random.Range(-25, 25), 0.25f, -20 + Random.Range(-25, 25));
			}
			else
			{
				prefab.transform.position = new Vector3(20 + Random.Range(-25, 25), 0.25f, 20 + Random.Range(-25, 25));
			}
			prefab.transform.parent = resources.transform;
			foreach(Transform child in prefab.transform)
    		{
				child.renderer.material.color = Color.yellow;
			}
			prefab.AddComponent("ResourceObject");
			prefab.tag = "YellowSource";
		}
		
		
		foreach(Transform child in resources.transform)
		{
			AstarPath.active.UpdateGraphs(child.collider.bounds);
		}
		
		int noOfClans = 1;
		for(int i = 0; i < noOfClans; i++)
		{
			//create the clans
			GameObject clan = new GameObject();
			
			if(i == 1)
			{
				clan.name = BlackBoard.Instance.GetClan();
				clan.transform.position = new Vector3(-20 + Random.Range(-5, 5), 0.25f, -20 + Random.Range(-5, 5));
			}
			else
			{
				clan.name = BlackBoard.Instance.GetClan();
				BlackBoard.Instance.SetScore(clan.name, 200);
				clan.transform.position = new Vector3(20 + Random.Range(-5, 5), 0.25f, 20 + Random.Range(-5, 5));
			}
			
			//Create a group for the commanders and place it in the clans
			GameObject commanderGroup = new GameObject();
			commanderGroup.name = "Commanders";
			commanderGroup.transform.parent = clan.transform;
			commanderGroup.transform.position = clan.transform.position;
			
			if(clan.name == "Blue Clan")
			{
				SupremeCommanderBlueClan prefab2 = (SupremeCommanderBlueClan)Instantiate(Resources.Load(("Prefabs/SupremeCommanderBlueClan"), typeof(SupremeCommanderBlueClan)));
				
				prefab2.transform.position = commanderGroup.transform.position;
				prefab2.transform.parent = commanderGroup.transform;
				prefab2.SetClan(clan.name);
			}
			else if(clan.name == "Red Clan")
			{
				SupremeCommanderRedClan prefab2 = (SupremeCommanderRedClan)Instantiate(Resources.Load(("Prefabs/SupremeCommanderRedClan"), typeof(SupremeCommanderRedClan)));
				
				prefab2.transform.position = commanderGroup.transform.position;
				prefab2.transform.parent = commanderGroup.transform;
				prefab2.SetClan(clan.name);
			}
			else if(clan.name == "Yellow Clan")
			{
				SupremeCommanderYellowClan prefab2 = (SupremeCommanderYellowClan)Instantiate(Resources.Load(("Prefabs/SupremeCommanderYellowClan"), typeof(SupremeCommanderYellowClan)));
				
				prefab2.transform.position = commanderGroup.transform.position;
				prefab2.transform.parent = commanderGroup.transform;
				prefab2.SetClan(clan.name);
			}
			
			
		}		
	}
}
