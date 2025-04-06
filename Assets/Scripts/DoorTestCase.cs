using System;
using UnityEngine;

[Serializable]
public class ValidAnswer {
    public string left;
    public string right;
}

[CreateAssetMenu(fileName = "DoorTestCase", menuName = "Scriptable Objects/Door Test Case")]
public class DoorTestCase : ScriptableObject {
    [Tooltip("For internal use only")]
    public string title;
    [TextArea]
    public string checklist;
    public string[] leftOptions;
    public string[] rightOptions;
    public ValidAnswer[] validAnswers;
    public bool thereIsNoBug;
}
