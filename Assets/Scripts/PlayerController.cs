using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour {
  public float horizontalSpeed = 7f;
  public int startingHealth = 3;
  public InputAction moveAction;
  public GameManager game;

  Rigidbody2D rigidBody;
  int health;

  void Start() {
    rigidBody = GetComponent<Rigidbody2D>();
    moveAction.Enable();
    health = startingHealth;
  }

  void FixedUpdate() {
    float moveInput = moveAction.ReadValue<float>();
    rigidBody.linearVelocity = new Vector2(moveInput * horizontalSpeed, game.scrollSpeed);

    float limit = game.playHalfWidth - transform.localScale.x / 2f;
    rigidBody.position = new Vector2(Mathf.Clamp(rigidBody.position.x, -limit, limit), rigidBody.position.y);
  }

  void OnCollisionEnter2D(Collision2D collision) {
    if (!collision.collider.CompareTag("Obstacle")) return;
    health--;
  }

  public int GetHealthLost() {
    return startingHealth - health;
  }
}
