using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {
  public PlayerController player;
  public float playHalfWidth = 4.5f;
  public float startScrollSpeed = 2f;
  public float scrollAcceleration = 0.1f;
  public float speedBoostPerLostHealth = 0.25f;
  public float multiplierPerLostHealth = 1f;

  public float scrollSpeed;
  public float CameraTop => mainCamera.transform.position.y + mainCamera.orthographicSize;

  Camera mainCamera;
  float baseScrollSpeed;
  float score;

  void Start() {
    mainCamera = Camera.main;
    baseScrollSpeed = startScrollSpeed;
    scrollSpeed = startScrollSpeed;
  }

  void Update() {
    int healthLost = player.GetHealthLost();
    float cameraBottom = mainCamera.transform.position.y - mainCamera.orthographicSize;
    float playerTop = player.transform.position.y + player.transform.localScale.y / 2f;
    if (healthLost >= player.startingHealth || playerTop < cameraBottom) {
      SceneManager.LoadScene("GameOver");
    }

    float dt = Time.deltaTime;
    baseScrollSpeed += scrollAcceleration * dt;
    scrollSpeed = baseScrollSpeed * (1f + speedBoostPerLostHealth * healthLost);
    mainCamera.transform.position += Vector3.up * (scrollSpeed * dt);
    score += baseScrollSpeed * dt * GetMultiplier();
  }

  float GetMultiplier() {
    return 1f + multiplierPerLostHealth * player.GetHealthLost();
  }

  void OnGUI() {
    GUIStyle style = new GUIStyle(GUI.skin.label) { fontSize = 28 };
    int health = player.startingHealth - player.GetHealthLost();
    GUI.Label(new Rect(20, 20, 800, 40), $"Score {score:0}    x{GetMultiplier():0.0}    HP {health}", style);
  }
}
