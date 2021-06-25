using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public static class MapService {
	// TODO: add variables for saving limits of map (left, right, top, bottom)
	[SerializeField] public static List<EntityInfo> entities = new List<EntityInfo>();
  public static Vector2 spawnPosition;
	static int size = 1;
	static GameObject entitiesFolder;

	#region static methods
		
		public static void Initialize () {
			entitiesFolder = new GameObject("Entities");
		}
		
		// TODO: clear "entities" folder, and re-generate all of its contents from `entities`
		public static void InitializeMapData (Level levelData) {
			entities = levelData.entities;
			spawnPosition = levelData.spawnPosition;
		}

		// TODO: check for existing entities at this location; if found, replace them
		public static Entity InsertEntity (GameObject go) {
			// get location to instantiate entity at
			Vector3 location = _GetNearestPositionOnGrid();

			// exit if entity already exists at this location
			if (_CheckIfEntityExists(location)) {
				Debug.Log("Already exists");
				return null;
			}

			// instantiate entity
			GameObject entity = GameObject.Instantiate(go, location, Quaternion.identity);

			// set parent in Hierarchy View
			entity.transform.SetParent(entitiesFolder.transform, false);

			// remove box collider
			GameObject.Destroy(entity.GetComponent<BoxCollider2D>());

			// generate new entity and return to parent
			Entity newEntity = _GenerateNewEntity(go, location);

			// convert entity model to basic data structure & add it to list
			EntityInfo entityInfo = new EntityInfo(newEntity);
			entities.Add(entityInfo);

			return newEntity;
		}
		
		public static void RemoveEntity () {}

		public static Level GetMapData () {
			return new Level(entities, spawnPosition);
		}

	#endregion

	#region private methods

		static Vector3 _GetNearestPositionOnGrid () {
			// determine mouse position on screen
			Vector3 worldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

			// get nearest point to mouse pointer
			float xPos = size * Mathf.RoundToInt(worldPosition.x / size);
			float yPos = size * Mathf.RoundToInt(worldPosition.y / size);

			// return computed position
			return new Vector3(xPos, yPos, 0);
    }

		static bool _CheckIfEntityExists (Vector3 location) {
			foreach (EntityInfo info in entities) {
				if (location.x == info.position.x && location.y == info.position.y) {
					return true;
				}
			}
			return false;
		}

    static Entity _GenerateNewEntity (GameObject selection, Vector3 location) {
			Entities type = Entities.BLOCK;
			switch (selection.name) {
				case "Block":
					type = Entities.BLOCK;
					break;
				case "Enemy":
					type = Entities.ENEMY;
					break;
			}
			Entity newEntity = new Entity(
				(Vector2)location,
				type
			);

			return newEntity;
    }
	
	#endregion
}

[Serializable]
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

[Serializable]
public class PositionInfo {
	public float x;
	public float y;

	public PositionInfo (Vector2 position) {
		this.x = position.x;
		this.y = position.y;
	}
}