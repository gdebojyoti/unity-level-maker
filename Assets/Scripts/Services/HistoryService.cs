using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class HistoryService {
  static Hashtable operations = new Hashtable();
  static List<string> orderOfOperations = new List<string>();
  static string currentIndex;

  public static void AddEntity (Entity entity) {
    // exit if no entity is found
    if (entity == null) {
      return;
    }
    
    // check if existing entity exists at given position

    // create Action
    Action action = new Action(
      Actions.INSERT
    );

    // create Operation
    Operation op = new Operation(action, entity);
    
    // save to hashtable
    string key = entity.position.x.ToString() + "|" + entity.position.y.ToString();
    operations.Add(key, op);

    // add key to orderOfOperations
    orderOfOperations.Add(key);

    MapService.AddEntity(entity);
  }

  public static void Undo () {}
  public static void Redo () {}
  public static void Save () {
    SaveService.SaveLevel();
  }
  public static void Load () {
    Level levelData = SaveService.LoadLevel();
    // initialize level map
    MapService.InitializeMapData(levelData);
  }
}