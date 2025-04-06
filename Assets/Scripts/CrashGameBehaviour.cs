using UnityEngine;

public class CrashGameBehaviour : StateMachineBehaviour {
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        animator.GetComponent<SimpleDoorController>().Crash();
    }
}
