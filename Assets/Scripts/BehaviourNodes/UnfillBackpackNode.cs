using BlackboardUtils;
using Lessons.AI.HierarchicalStateMachine;
using Lessons.AI.LessonBehaviourTree;
using Sample;
using UnityEngine;

namespace BehaviourNodes
{
    public class UnfillBackpackNode : BehaviourNode
    {
        [SerializeField] private Blackboard _blackboard;
        protected override void Run()
        {
            var character = _blackboard.GetVariable<Character>(BlackboardConst.SelfCharacter);

            if (_blackboard.TryGetVariable<Barn>(BlackboardConst.Barn, out var barn))
            {
                if (!barn.IsFull())
                {
                    var left = barn.CapacityLeft();
                    var unloadedResources = character.TakeResources(left);
                    barn.AddResources(unloadedResources);
                    Return(true);
                    return;
                }
                Return(false);
                return;
            }
            Return(false);
        }
    }
}