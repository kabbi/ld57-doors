using UnityEngine;

public class HintZone : MonoBehaviour {
    public bool activated;
    public GameObject hintLabel;

    void OnTriggerEnter(Collider other) {
        if (!other.CompareTag("Player")) {
            return;
        }
        activated = true;
        hintLabel.SetActive(true);
    }

    void OnTriggerExit(Collider other) {
        if (!other.CompareTag("Player")) {
            return;
        }
        activated = false;
        hintLabel.SetActive(false);
    }
}
