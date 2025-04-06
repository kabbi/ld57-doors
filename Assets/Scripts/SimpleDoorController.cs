using UnityEngine;
using UnityEngine.InputSystem;

public class SimpleDoorController : MonoBehaviour {
    private PlayerInput playerInput;
    private InputAction interactAction;
    private AudioSource audioSource;
    public AudioClip openSound;
    public AudioClip closeSound;
    public Animator animator;
    public HintZone hintZone;
    public bool opened;

    void Awake() {
        audioSource = GetComponent<AudioSource>();
        playerInput = FindFirstObjectByType<PlayerInput>();
        interactAction = playerInput.currentActionMap.FindAction("Interact");
    }

    void OnEnable() {
        playerInput.onActionTriggered += HandleAction;
    }

    void OnDisable() {
        playerInput.onActionTriggered -= HandleAction;
    }

    private void HandleAction(InputAction.CallbackContext context) {
        if (!hintZone || !hintZone.activated) {
            return;
        }
        if (context.action == interactAction && context.performed) {
            SetOpened(!opened);
        }
    }

    public void SetOpened(bool v) {
        opened = v;
        animator.SetBool("opened", opened);
        audioSource.PlayOneShot(opened ? openSound : closeSound);
    }

    public void SetInProgress(bool wip) {
        animator.SetBool("wip", wip);
    }

    public void Crash() {
        BSODController bsod = FindFirstObjectByType<BSODController>();
        bsod.Begin();
        animator.SetBool("opened", false);
        opened = false;
    }
}
