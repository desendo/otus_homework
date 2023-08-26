using BlackboardUtils;
using Lessons.AI.HierarchicalStateMachine;
using Lessons.AI.LessonBehaviourTree;
using Sample;
using UnityEngine;

namespace BehaviourNodes
{
    public class SetBarnMovePositionNode : BehaviourNode
    {
        [SerializeField] private Blackboard _blackboard;
        protected override void Run()
        {
            if (_blackboard.TryGetVariable<Barn>(BlackboardConst.Barn, out var barn))
            {
                _blackboard.SetVariable(BlackboardConst.MoveTarget, barn.transform.position);
                Return(true);
                return;
            }
            Return(false);
        }
    }
}