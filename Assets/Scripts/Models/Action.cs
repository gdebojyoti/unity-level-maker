using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Action {
  public Actions type;
  public Vector2 position;

  public Action (Actions type, Vector2 position) {
    this.type = type;
    this.position = position;
  }
  public Action (Actions type) {
    this.type = type;
  }
}
