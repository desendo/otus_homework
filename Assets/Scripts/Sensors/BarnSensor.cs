using BlackboardUtils;
using Lessons.AI.HierarchicalStateMachine;
using Sample;
using UnityEngine;

namespace Sensors
{
    public class BarnSensor : MonoBehaviour
    {
        [SerializeField]
        private Blackboard _blackboard;
        private Barn _barn;

        public void Start()
        {
            _barn = FindObjectOfType<Sample.Barn>();
            if(_barn != null)
                _blackboard.SetVariable(BlackboardConst.Barn, _barn);
        }
    }
}