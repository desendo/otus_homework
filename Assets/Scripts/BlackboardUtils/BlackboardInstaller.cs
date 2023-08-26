using Lessons.AI.HierarchicalStateMachine;
using Sample;
using UnityEngine;

namespace BlackboardUtils
{
    public sealed class BlackboardInstaller : MonoBehaviour
    {
        [SerializeField]
        private Character _unit;

        private void Awake()
        {
            var blackboard = this.GetComponent<Blackboard>();
            blackboard.SetVariable(BlackboardConst.SelfCharacter, _unit);

        }
    }
}