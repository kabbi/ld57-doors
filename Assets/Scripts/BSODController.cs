using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class BSODController : MonoBehaviour {
    private Image image;

    void Awake() {
        image = GetComponent<Image>();
    }

    private IEnumerator Hide() {
        yield return new WaitForSeconds(4);
        image.enabled = false;
    }

    public void Begin() {
        image.enabled = true;
        StartCoroutine(Hide());
    }
}
