using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity {
    public string uuid;
    public Entities type;
    public int id = 1;
    public Vector2 position;

    public Entity (Vector2 position, Entities type) {
        this.uuid = System.Guid.NewGuid().ToString();
        this.position = position;
        this.type = type;
    }
}
