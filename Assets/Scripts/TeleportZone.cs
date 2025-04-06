using UnityEngine;
using UnityEngine.SceneManagement;

public class TeleportZone : MonoBehaviour {
    public string sceneName;

    void OnTriggerEnter(Collider other) {
        if (!other.CompareTag("Player")) {
            return;
        }
        SceneManager.LoadScene(sceneName);
    }
}
