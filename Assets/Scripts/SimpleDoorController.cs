using UnityEngine;
using UnityEngine.InputSystem;

public class SimpleDoorController : MonoBehaviour {
    private PlayerInput playerInput;
    private InputAction interactAction;
    public Animator animator;
    public HintZone hintZone;
    private bool opened;

    void Awake() {
        playerInput = GetComponent<PlayerInput>();
        interactAction = playerInput.currentActionMap.FindAction("Interact");
    }

    void OnEnable() {
        playerInput.onActionTriggered += HandleAction;
    }

    void OnDisable() {
        playerInput.onActionTriggered -= HandleAction;
    }

    private void HandleAction(InputAction.CallbackContext context) {
        if (context.action == interactAction && context.performed) {
            if (!hintZone.activated) {
                return;
            }
            opened = !opened;
            animator.SetBool("opened", opened);
        }
    }
}
