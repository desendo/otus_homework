using UnityEngine;

namespace Common
{
    public class AnimationEventFireBehaviour : StateMachineBehaviour
    {
        [SerializeField] public string _onExit;
        public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            animator.gameObject.GetComponent<AnimationEventListener>().StringEvent(_onExit);
        }
    }
}
