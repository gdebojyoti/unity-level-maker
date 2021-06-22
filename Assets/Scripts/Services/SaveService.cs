// responsible for saving to and loading from JSON files

using System.IO; // to work with files
using UnityEngine;

public static class SaveService {
  static string path = Application.persistentDataPath + "/level.json";

  public static void SaveLevel () {
    Level levelData = MapService.GetMapData();

    var output = JsonUtility.ToJson(levelData, true);
    Debug.Log(output);

    File.WriteAllText(path, output);
  }

  public static void LoadLevel () {
    // string abc = "{\"entities\":[{\"uuid\":\"1a47375d-5fe3-48b0-b076-17f706be31ef\",\"type\":\"ENEMY\",\"id\":1,\"position\":{\"x\":4.0,\"y\":3.0}}],\"spawnPosition\":{\"x\":0.0,\"y\":0.0}}";

    if (File.Exists(path)) {
      // Read the entire file and save its contents.
      string fileContents = File.ReadAllText(path);
      Level levelData = JsonUtility.FromJson<Level>(fileContents);
      EntityInfo info = levelData.entities[0];
      Debug.Log("info: " + info.position.x + info.position.y);
    } else {
      Debug.Log("File not found!!!");
    }
  }
}


