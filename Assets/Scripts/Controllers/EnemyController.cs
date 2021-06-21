using UnityEngine;

public class EnemyController : MonoBehaviour {
  public bool isActive = false;
  public Directions initialDirection = Directions.Left;
  public float speed = 2f;
  
  Vector2 direction;
  Rigidbody2D rb;

  void Start () {
    direction = new Vector2(initialDirection == Directions.Left ? -1 : 1, 0);
    rb = GetComponent<Rigidbody2D>();
  }
  
  void FixedUpdate () {
    _Move();
  }

  private void OnCollisionEnter2D(Collision2D entity) {
    // if enemy collides with a non-player entity, revert enemy's direction
    if(entity.gameObject.tag != "Player") { 
      Directions impactDirection = _GetCollidingSide(entity);
      switch (impactDirection) {
        case Directions.Left:
          direction.x = 1;
          break;
        case Directions.Right:
          direction.x = -1;
          break;
      }
    }

  }

  private void _Move () {
    transform.Translate(direction * speed * Time.deltaTime);
    // float speedPerFrame = speed * Time.deltaTime;
    // rb.AddForce(transform.right * speed * direction.x);
  }

  // side of gameobject ("enemy") against which external object (such as "block") collided
  private Directions _GetCollidingSide (Collision2D entity) {
    // vector from contact point to center of current gameobject ("enemy")
    Vector2 collisionSide = (Vector2)(transform.position) - entity.collider.ClosestPoint(transform.position);
    collisionSide.Normalize();

    int xVal = Mathf.RoundToInt(collisionSide.x);
    int yVal = Mathf.RoundToInt(collisionSide.y);

    // xVal: 1 = left, -1 = right; yVal: 1 = bottom, -1 = top
    return xVal != 0 ? (xVal == 1 ? Directions.Left : Directions.Right)
      : (yVal == 1 ? Directions.Bottom : Directions.Top);
  }
}
