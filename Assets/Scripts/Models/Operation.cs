using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Operation {
  // public string uuid;
  // public string timestamp;
  public Action action;
  public Entity entity;

  public Operation (Action action, Entity entity) {
    this.action = action;
    this.entity = entity;
  }
}
