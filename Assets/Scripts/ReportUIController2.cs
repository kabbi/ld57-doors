using UnityEngine;
using UnityEngine.UIElements;

public class ReportUIController2 : MonoBehaviour {
    private VisualElement root;
    private VisualElement reportPanel;
    private VisualElement resultPanel;
    public DoorTestCase testCase;

    void Start() {
        root = GetComponent<UIDocument>().rootVisualElement;

        reportPanel = root.Q("reportPanel");
        resultPanel = root.Q("resultPanel");

        Button submitButton = root.Q<Button>("submitButton");
        submitButton.clicked += OnSubmit;
    }

    private void OnSubmit() {
        reportPanel.style.display = DisplayStyle.None;
        resultPanel.style.display = DisplayStyle.Flex;
    }
}
