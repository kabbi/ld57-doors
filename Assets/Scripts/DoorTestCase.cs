using System;
using UnityEngine;

[Serializable]
public class ValidAnswer {
    public string left;
    public string middle;
    public string right;
}

[CreateAssetMenu(fileName = "DoorTestCase", menuName = "The Doors/Door Test Case")]
public class DoorTestCase : ScriptableObject {
    [Tooltip("For internal use only")]
    public string title;
    [TextArea]
    public bool thereIsNoBug;
    public string checklist;
    public string[] leftOptions;
    public string[] middleOptions;
    public string[] rightOptions;
    public ValidAnswer[] validAnswers;
}
