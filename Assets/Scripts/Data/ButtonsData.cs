using System;
using UnityEngine;

[CreateAssetMenu(fileName = "Buttons", menuName = "EditorData/Buttons")]
public class ButtonsData : ScriptableObject {
    public ButtonData[] buttons;
}

[Serializable]
public class ButtonData {
  public string id;
  public string key;

  public ButtonData (string id, string key) {
    this.id = id;
    this.key = key;
  }
}