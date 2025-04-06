using UnityEngine;

public class DoorPusherTag : MonoBehaviour {
    public float force;

    void OnCollisionEnter(Collision collision) {
        if (!collision.gameObject.CompareTag("Player")) {
            return;
        }
        if (!collision.gameObject.GetComponent<Rigidbody>()) {
            collision.gameObject.AddComponent<Rigidbody>();
        }
        CharacterController controller = collision.gameObject.GetComponent<CharacterController>();
        controller.enabled = false;
        Rigidbody body = collision.gameObject.GetComponent<Rigidbody>();
        body.AddForce(collision.contacts[0].normal * force);
    }
}
