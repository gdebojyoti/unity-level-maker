using System.Collections.Generic;
using UnityEngine;

public class Level {
  public List<EntityInfo> entities = new List<EntityInfo>();
  public Vector2 spawnPosition;

  public Level (List<EntityInfo> entities, Vector2 spawnPosition) {
    this.entities = entities;
    this.spawnPosition = spawnPosition;
  }
}