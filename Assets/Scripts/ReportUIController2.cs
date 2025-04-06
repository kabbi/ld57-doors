using System.Collections;
using UnityEngine;
using UnityEngine.UIElements;

public class ReportUIController2 : MonoBehaviour {
    private VisualElement root;
    private VisualElement reportPanel;
    private VisualElement progressPanel;
    private VisualElement successPanel;
    private VisualElement failPanel;
    private DropdownField leftDropdown;
    private DropdownField middleDropdown;
    private DropdownField rightDropdown;
    public SimpleDoorController[] doors;
    public DoorTestCase testCase;
    public bool completed;

    void Start() {
        root = GetComponent<UIDocument>().rootVisualElement;

        reportPanel = root.Q("reportPanel");
        progressPanel = root.Q("progressPanel");
        successPanel = root.Q("successPanel");
        failPanel = root.Q("failPanel");
        leftDropdown = root.Q<DropdownField>("leftDropdown");
        rightDropdown = root.Q<DropdownField>("rightDropdown");
        middleDropdown = root.Q<DropdownField>("middleDropdown");

        Button submitButton = root.Q<Button>("submitButton");
        submitButton.clicked += OnSubmit;
        Button restartButton = root.Q<Button>("restartButton");
        restartButton.clicked += OnRestart;

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
        StartCoroutine(SubmitDoor(success));
    }

    private void OnRestart() {
        failPanel.style.display = DisplayStyle.None;
        reportPanel.style.display = DisplayStyle.Flex;
    }

    private IEnumerator SubmitDoor(bool success) {
        foreach (var door in doors) {
            door.SetInProgress(true);
        }
        reportPanel.style.display = DisplayStyle.None;
        progressPanel.style.display = DisplayStyle.Flex;
        yield return new WaitForSeconds(5);
        progressPanel.style.display = DisplayStyle.None;
        if (success) {
            successPanel.style.display = DisplayStyle.Flex;
            completed = true;
        }
        else {
            foreach (var door in doors) {
                door.SetInProgress(false);
            }
            failPanel.style.display = DisplayStyle.Flex;
        }
    }
}
