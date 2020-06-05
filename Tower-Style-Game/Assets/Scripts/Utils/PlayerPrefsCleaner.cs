using UnityEngine;

public class PlayerPrefsCleaner : MonoBehaviour {

    [SerializeField]
    private bool _forceDisableThisScript = false;

    private void Awake() {
        if (_forceDisableThisScript) {
            this.enabled = false;
            return;
        }

        if (Application.isEditor) {
            PlayerPrefs.DeleteAll();
        }
    }

}
