using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class EditorList : MonoBehaviour {
  [SerializeField] public List<EditorEntity> entities = new List<EditorEntity>();

  public GameObject FetchEditorObject (string name) {
    for (int i = 0; i < entities.Count; i++) {
      if (entities[i].name == name) {
        return entities[i].entity;
      }
    }
    return null;
  }
}

[Serializable]
public class EditorEntity {
  public string name;
  public GameObject entity;
  public EditorEntity(string name, GameObject entity) {
    this.name = name;
    this.entity = entity;
  }
}