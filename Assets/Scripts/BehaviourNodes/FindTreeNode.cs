using BlackboardUtils;
using Lessons.AI.HierarchicalStateMachine;
using Lessons.AI.LessonBehaviourTree;
using UnityEngine;
using Tree = Sample.Tree;

namespace BehaviourNodes
{
    public class FindTreeNode : BehaviourNode
    {
        [SerializeField] protected Blackboard _blackboard;
        protected override void Run()
        {
            if (_blackboard.TryGetVariable<Tree>(BlackboardConst.Tree, out var tree))
            {
                _blackboard.SetVariable(BlackboardConst.MoveTarget, tree.transform.position);
                Return(true);
                return;
            }
            Return(false);
        }
    }
}