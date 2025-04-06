using UnityEngine;

public class LampController : MonoBehaviour {
    public bool activated;
    public Material onMaterial;
    public Material offMaterial;
    public GameObject lightFx;
    private MeshRenderer mesh;

    void Awake() {
        mesh = GetComponent<MeshRenderer>();
    }

    public void UpdateVisuals() {
        mesh.material = activated ? onMaterial : offMaterial;
        lightFx.SetActive(activated);
    }
}
