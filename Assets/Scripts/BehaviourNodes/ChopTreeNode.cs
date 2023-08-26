using BlackboardUtils;
using Lessons.AI.HierarchicalStateMachine;
using Lessons.AI.LessonBehaviourTree;
using Sample;
using UnityEngine;
using Tree = Sample.Tree;

namespace BehaviourNodes
{
    public class ChopTreeNode : BehaviourNode
    {
        [SerializeField] private Blackboard _blackboard;
        protected override void Run()
        {
            var character = _blackboard.GetVariable<Character>(BlackboardConst.SelfCharacter);

            if (_blackboard.TryGetVariable<Tree>(BlackboardConst.Tree, out var tree) && tree.HasResources())
            {
                character.Chop(tree);
                Return(true);
                return;
            }
            Return(false);
        }
    }
}