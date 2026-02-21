using UnityEngine;

public class FallAndRiseBehaviour : StateMachineBehaviour
{


    public override void OnStateMachineEnter(Animator animator, int stateMachinePathHash)
    {
        GameManager.Instance.playerCharacterController.ToggleMoving(false);
    }

    public override void OnStateMachineExit(Animator animator, int stateMachinePathHash)
    {
        GameManager.Instance.playerCharacterController.ToggleMoving(true);
    }
}
