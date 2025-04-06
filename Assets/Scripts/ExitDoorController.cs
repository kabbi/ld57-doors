using System.Collections;
using UnityEngine;

public class ExitDoorController : MonoBehaviour {
    private SimpleDoorController door;
    public ReportUIController2[] requiredReports;

    void Awake() {
        door = GetComponent<SimpleDoorController>();
        StartCoroutine(MonitorCompletion());
    }

    private IEnumerator MonitorCompletion() {
        while (true) {
            yield return new WaitForSeconds(1);
            if (IsCompleted()) {
                door.SetOpened(true);
                break;
            }
        }
    }

    private bool IsCompleted() {
        foreach (var report in requiredReports) {
            if (!report.completed) {
                return false;
            }
        }
        return true;
    }
}
