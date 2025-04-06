using UnityEngine;
using UnityEngine.UI;

public class ReportUIController : MonoBehaviour {
    public GameObject reportPanel;
    public GameObject buttonPanel;
    public GameObject bugReportPanel;
    public GameObject resultPanel;
    public Button passButton;
    public Button failButton;
    public Button submitButton;
    public TMPro.TMP_Text resultLabel;
    public TMPro.TMP_Dropdown leftDropdown;
    public TMPro.TMP_Dropdown rightDropdown;
    public DoorTestCase testCase;

    void Start() {
    }

    void OnEnable() {
        passButton.onClick.AddListener(OnPassClick);
        failButton.onClick.AddListener(OnFailClick);
        submitButton.onClick.AddListener(OnSubmit);

        buttonPanel.SetActive(true);
        bugReportPanel.SetActive(false);
        Populate();
    }
    void OnDisable() {
        passButton.onClick.RemoveListener(OnPassClick);
        failButton.onClick.RemoveListener(OnFailClick);
        submitButton.onClick.RemoveListener(OnSubmit);
    }

    private void OnPassClick() {
        resultLabel.text = testCase.thereIsNoBug ? "Yeah, you are right. This door is GOOD." : "You are wrong. This door is broken AF. Users won't be happy";
        resultPanel.SetActive(true);
        reportPanel.SetActive(false);
    }

    private void OnFailClick() {
        buttonPanel.SetActive(false);
        bugReportPanel.SetActive(true);
    }

    private void OnSubmit() {
        bool success = false;
        string leftValue = leftDropdown.options[leftDropdown.value].text;
        string rightValue = rightDropdown.options[rightDropdown.value].text;
        foreach (var validAnswer in testCase.validAnswers) {
            if (validAnswer.left == leftValue && validAnswer.right == rightValue) {
                success = true;
            }
        }
        resultLabel.text = success ? "Noice" : "You are wrong";
        resultPanel.SetActive(true);
        reportPanel.SetActive(false);
    }

    public void Populate() {
        if (!testCase) {
            return;
        }
        leftDropdown.options.Clear();
        foreach (var option in testCase.leftOptions) {
            leftDropdown.options.Add(new TMPro.TMP_Dropdown.OptionData(option));
        }
        rightDropdown.options.Clear();
        foreach (var option in testCase.rightOptions) {
            rightDropdown.options.Add(new TMPro.TMP_Dropdown.OptionData(option));
        }
    }
}
