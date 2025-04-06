using UnityEngine;
using UnityEngine.UIElements;

public class ReportUIController2 : MonoBehaviour {
    private VisualElement root;
    private VisualElement reportPanel;
    private VisualElement resultPanel;
    private DropdownField leftDropdown;
    private DropdownField middleDropdown;
    private DropdownField rightDropdown;
    public DoorTestCase testCase;
    public LampController redLamp;
    public LampController greeenLamp;

    void Start() {
        root = GetComponent<UIDocument>().rootVisualElement;

        reportPanel = root.Q("reportPanel");
        resultPanel = root.Q("resultPanel");
        leftDropdown = root.Q<DropdownField>("leftDropdown");
        rightDropdown = root.Q<DropdownField>("rightDropdown");
        middleDropdown = root.Q<DropdownField>("middleDropdown");

        Button submitButton = root.Q<Button>("submitButton");
        submitButton.clicked += OnSubmit;

        Populate();
    }

    public void Populate() {
        leftDropdown.choices.Clear();
        foreach (var option in testCase.leftOptions) {
            leftDropdown.choices.Add(option);
        }
        middleDropdown.choices.Clear();
        foreach (var option in testCase.middleOptions) {
            middleDropdown.choices.Add(option);
        }
        rightDropdown.choices.Clear();
        foreach (var option in testCase.rightOptions) {
            rightDropdown.choices.Add(option);
        }
    }

    private void OnSubmit() {
        string leftOption = leftDropdown.value;
        string middleOption = middleDropdown.value;
        string rightOption = rightDropdown.value;
        bool success = false;
        foreach (var validAnswer in testCase.validAnswers) {
            if (validAnswer.left == leftOption && validAnswer.middle == middleOption && validAnswer.right == rightOption) {
                success = true;
            }
        }
        reportPanel.style.display = DisplayStyle.None;
        resultPanel.style.display = DisplayStyle.Flex;
        if (success) {
            greeenLamp.activated = true;
            greeenLamp.UpdateVisuals(); ;
        }
        else {
            redLamp.activated = true;
            redLamp.UpdateVisuals();
        }
    }
}
