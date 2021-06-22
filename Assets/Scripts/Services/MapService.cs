using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class MapService {
  public static List<EntityInfo> entities = new List<EntityInfo>();
  public static Vector2 spawnPosition;
	// TODO: add variables for saving limits of map (left, right, top, bottom)

	public static void AddEntity (Entity entity) {
		// convert entity model to basic data structure
		EntityInfo entityInfo = new EntityInfo(entity);

		// add above info to list
		entities.Add(entityInfo);
	}
	
	public static void RemoveEntity () {}
	
	public static void Save () {
		Debug.Log("Saving map...");
	}
}

public class EntityInfo {
	public string uuid;
	public string type;
	public int id = 1;
	public PositionInfo position;

	public EntityInfo (Entity entity) {
		this.uuid = entity.uuid;
		this.type = entity.type.ToString();
		this.position = new PositionInfo(entity.position);
	}
}

public class PositionInfo {
	public float x;
	public float y;

	public PositionInfo (Vector2 position) {
		this.x = position.x;
		this.y = position.y;
	}
}