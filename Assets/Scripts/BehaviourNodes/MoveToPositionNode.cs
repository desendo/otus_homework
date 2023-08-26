using BlackboardUtils;
using Lessons.AI.HierarchicalStateMachine;
using Lessons.AI.LessonBehaviourTree;
using Sample;
using UnityEngine;
using Tree = Sample.Tree;

namespace BehaviourNodes
{
    public class MoveToPositionNode : BehaviourNode
    {
        [SerializeField] private Blackboard _blackboard;
        protected override void Run()
        {
            var character = _blackboard.GetVariable<Character>(BlackboardConst.SelfCharacter);
            if (_blackboard.TryGetVariable<Vector3>(BlackboardConst.MoveTarget, out var target))
            {
                var dist =  target - character.transform.position ;
                if (dist.sqrMagnitude < 1f)
                {
                    Return(true);
                    return;
                }
                character.Move(dist.normalized);

                Return(false);
                return;
            }
            Return(false);
        }
    }
}