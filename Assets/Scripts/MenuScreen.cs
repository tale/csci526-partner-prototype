using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class MenuScreen : MonoBehaviour {
  public string title;
  public string nextScene;

  void Update() {
    if (Keyboard.current.spaceKey.wasPressedThisFrame) {
      SceneManager.LoadScene(nextScene);
    }
  }

  void OnGUI() {
    GUIStyle style = new GUIStyle(GUI.skin.label) {
      fontSize = 40,
      alignment = TextAnchor.MiddleCenter
    };

    GUI.Label(new Rect(0, 0, Screen.width, Screen.height), $"{title}\npress Space", style);
  }
}
