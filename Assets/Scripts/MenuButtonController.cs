using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuButtonController : MonoBehaviour {
    public string mainSceneName;

    public void Click() {
        SceneManager.LoadScene(mainSceneName);
    }
}
