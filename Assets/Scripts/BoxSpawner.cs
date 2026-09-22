using UnityEngine;

public class BoxSpawner : MonoBehaviour {
  public GameObject boxPrefab;
  public GameManager game;
  public int laneCount = 6;
  public float rowSpacing = 3f;
  public float spacingScale = 0.5f;
  public float boxChance = 0.4f;

  float nextRowY = 6f;

  void Update() {
    float spacing = rowSpacing * Mathf.Pow(game.scrollSpeed / game.startScrollSpeed, spacingScale);
    while (nextRowY < game.CameraTop + spacing) {
      SpawnRow(nextRowY);
      nextRowY += spacing;
    }
  }

  void SpawnRow(float y) {
    float halfWidth = game.playHalfWidth;
    float laneWidth = halfWidth * 2f / laneCount;

    for (int lane = 0; lane < laneCount; lane++) {
      if (Random.value > boxChance) continue;

      float x = -halfWidth + laneWidth * (lane + 0.5f);
      var box = Instantiate(boxPrefab, new Vector3(x, y, 0f), Quaternion.identity, transform);
      box.transform.localScale = new Vector3(laneWidth * 0.9f, 0.6f, 1f);
    }
  }
}
