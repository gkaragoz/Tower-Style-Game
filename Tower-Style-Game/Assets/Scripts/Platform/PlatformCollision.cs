using System.Diagnostics;
using UnityEngine;
using UnityEngine.PlayerLoop;

[ExecuteAlways]
public class PlatformCollision : MonoBehaviour {
    public enum PlatformType {
        One,
        Two,
        Three,
        Four,
        Five,
        Six
    }

    [SerializeField]
    private PlatformType platformType = PlatformType.Six;

    [System.Serializable]
    public class CollisionData {
        public BoxCollider2D collider;
        public GameObject platform;
    }

    public CollisionData[] collisions;

    private void Awake() {
        UpdatePlatforms();
    }
    
    private void UpdatePlatforms() {
        foreach (var item in collisions) {
            item.platform.gameObject.SetActive(false);
            item.collider.enabled = false;
        }

        collisions[(int)platformType].collider.enabled = true;
        collisions[(int)platformType].platform.SetActive(true);
    }

    private void OnValidate() {
        UpdatePlatforms();
    }

}
