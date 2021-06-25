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
	static EditorList editorList;

	#region static methods
		
		public static void Initialize (EditorList el) {
			entitiesFolder = new GameObject("Entities");
			editorList = el;
		}
		
		public static void InitializeMapData (Level levelData) {
			spawnPosition = levelData.spawnPosition;

			// clear folder
			foreach (Transform child in entitiesFolder.transform) {
				GameObject.Destroy(child.gameObject);
			}

			// re-initialize entities list
			entities = new List<EntityInfo>();

			foreach (EntityInfo info in levelData.entities) {
				GameObject go = editorList.FetchEditorObject(info.type);
				Vector3 position = new Vector3(info.position.x, info.position.y);
				
				Entity newEntity = _AddNewEntityInScene(go, position);
				entities.Add(info);
			}
		}

		
		public static Entity AddEntity (GameObject go) {
			// get location to instantiate entity at
			Vector3 location = _GetNearestPositionOnGrid();

			// exit if entity already exists at this location
			if (_CheckIfEntityExists(location)) {
				Debug.Log("Already exists");
				// TODO: replace entities instead of exiting
				return null;
			}

			Entity newEntity = _AddNewEntityInScene(go, location);

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

		// generate new `Entity`; add it under "Entities" folder
		static Entity _AddNewEntityInScene (GameObject go, Vector3 location) {
			// instantiate entity
			GameObject entity = GameObject.Instantiate(go, location, Quaternion.identity);

			// set parent in Hierarchy View
			entity.transform.SetParent(entitiesFolder.transform, false);

			// remove box collider
			GameObject.Destroy(entity.GetComponent<BoxCollider2D>());

			// generate new entity
			Entity newEntity = _GenerateNewEntity(go, location);

			return newEntity;
		}

		// create a new `Entity` from parameters
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