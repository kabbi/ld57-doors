using UnityEngine;
using UnityEngine.InputSystem;

public class NoteController : MonoBehaviour {
    public bool activated;
    private PlayerInput playerInput;
    private InputAction hintAction;
    public GameObject hintLabel;
    public GameObject content;

    void Awake() {
        playerInput = FindFirstObjectByType<PlayerInput>();
        hintAction = playerInput.currentActionMap.FindAction("Hint");
    }

    void OnEnable() {
        playerInput.onActionTriggered += HandleAction;
    }

    void OnDisable() {
        playerInput.onActionTriggered -= HandleAction;
    }

    void OnTriggerEnter(Collider other) {
        if (!enabled || !other.CompareTag("Player")) {
            return;
        }
        activated = true;
        hintLabel.SetActive(true);
    }

    void OnTriggerExit(Collider other) {
        if (!enabled || !other.CompareTag("Player")) {
            return;
        }
        activated = false;
        hintLabel.SetActive(false);
    }

    private void HandleAction(InputAction.CallbackContext context) {
        if (!activated) {
            return;
        }
        if (context.action == hintAction && context.performed) {
            content.SetActive(true);
            hintLabel.SetActive(false);
            enabled = false;
        }
    }

}
