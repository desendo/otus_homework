using BlackboardUtils;
using Lessons.AI.HierarchicalStateMachine;
using Sample;
using UnityEngine;


namespace Sensors
{
    public class BackPackSensor : MonoBehaviour
    {
        [SerializeField]
        private Blackboard _blackboard;
        private Character _character;

        public void Start()
        {
            _character = _blackboard.GetVariable<Character>(BlackboardConst.SelfCharacter);
        }

        public void Update()
        {
            _blackboard.SetVariable(BlackboardConst.BagpackIsFull, _character.IsResourceBagFull());
        }
    }
}