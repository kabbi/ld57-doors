using UnityEngine;

public class CharacterPusher : MonoBehaviour {
    public float pushPower = 2.0f;

    void OnControllerColliderHit(ControllerColliderHit hit) {
        Rigidbody body = hit.collider.attachedRigidbody;
        if (!body || body.isKinematic) {
            return;
        }

        if (hit.moveDirection.y < -0.3) {
            return;
        }

        Vector3 pushDir = new Vector3(hit.moveDirection.x, 0, hit.moveDirection.z);
        body.linearVelocity = pushDir * pushPower;
    }
}
