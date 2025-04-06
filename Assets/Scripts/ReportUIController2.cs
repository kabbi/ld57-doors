using UnityEngine;
using UnityEngine.UIElements;

public class ReportUIController2 : MonoBehaviour {
    private VisualElement root;
    private VisualElement buttonPanel;
    private VisualElement reportPanel;
    private VisualElement resultPanel;

    void Start() {
        root = GetComponent<UIDocument>().rootVisualElement;

        buttonPanel = root.Q("buttonPanel");
        reportPanel = root.Q("reportPanel");
        resultPanel = root.Q("resultPanel");

        Button passButton = root.Q<Button>("passButton");
        passButton.clicked += OnPass;

        Button failButton = root.Q<Button>("failButton");
        failButton.clicked += OnFail;

        Button submitButton = root.Q<Button>("submitButton");
        submitButton.clicked += OnSubmit;
    }

    private void OnPass() {
        buttonPanel.style.display = DisplayStyle.None;
        resultPanel.style.display = DisplayStyle.Flex;
    }

    private void OnFail() {
        buttonPanel.style.display = DisplayStyle.None;
        reportPanel.style.display = DisplayStyle.Flex;
    }

    private void OnSubmit() {
        reportPanel.style.display = DisplayStyle.None;
        resultPanel.style.display = DisplayStyle.Flex;
    }
}
