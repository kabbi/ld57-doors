using UnityEngine;

public class DoorTestZone : MonoBehaviour {
    public GameObject checklistPanel;
    public GameObject reportPanel;
    public GameObject resultPanel;
    public ReportUIController reportController;
    public TMPro.TMP_Text checklistLabel;
    public DoorTestCase testCase;

    void OnTriggerEnter(Collider other) {
        if (!other.CompareTag("Player")) {
            return;
        }
        checklistLabel.text = testCase.checklist;
        checklistPanel.SetActive(true);
        reportPanel.SetActive(true);
        reportController.testCase = testCase;
    }

    void OnTriggerExit(Collider other) {
        if (!other.CompareTag("Player")) {
            return;
        }
        checklistPanel.SetActive(false);
        reportPanel.SetActive(false);
        resultPanel.SetActive(false);
    }
}
