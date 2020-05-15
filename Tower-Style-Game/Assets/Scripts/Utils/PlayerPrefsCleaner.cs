using UnityEngine;

public class PlayerPrefsCleaner : MonoBehaviour {

    private void Awake() {
        PlayerPrefs.DeleteAll();
    }

}
