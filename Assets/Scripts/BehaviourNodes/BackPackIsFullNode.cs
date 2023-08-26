using BlackboardUtils;
using Lessons.AI.HierarchicalStateMachine;
using Lessons.AI.LessonBehaviourTree;
using UnityEngine;

namespace BehaviourNodes
{
    public class BackPackIsFullNode : BehaviourNode, IBehaviourCallback
    {
        [SerializeField] private Blackboard _blackboard;
        [SerializeField] private BehaviourNode _onContinueNode;
        [SerializeField] private BehaviourNode _onFailNode;
        protected override void Run()
        {
            if (_blackboard.TryGetVariable<bool>(BlackboardConst.BagpackIsFull, out var isFull) && isFull)
            {
                _onContinueNode.Abort();
                _onContinueNode.Run(this);

                return;
            }
            _onFailNode.Abort();
            _onFailNode.Run(this);
        }

        public void Invoke(BehaviourNode node, bool success)
        {
            Return(success);
        }
    }
}