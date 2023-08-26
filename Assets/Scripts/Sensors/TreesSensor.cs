using System;
using System.Linq;
using BlackboardUtils;
using Lessons.AI.HierarchicalStateMachine;
using Sirenix.Utilities;
using UnityEngine;
using Object = UnityEngine.Object;
using Tree = Sample.Tree;


namespace Sensors
{
    public class TreesSensor : MonoBehaviour
    {
        [SerializeField] private Blackboard _blackboard;
        [SerializeField] private Transform _centerPoint;
        [SerializeField] private float _sensorUpdatePeriod;
        private float _timer;

        public void Update()
        {

            _timer -= Time.deltaTime;
            if (_timer <= 0)
            {
                _timer =  _sensorUpdatePeriod;
                UpdateTrees();
            }
        }

        private void UpdateTrees()
        {
            var trees = Object.FindObjectsOfType<Sample.Tree>().ToList();
            trees.Sort((tree, tree1) =>
            {
                if (SqrMagnitude(tree) > SqrMagnitude(tree1))
                    return 1;
                if (SqrMagnitude(tree) < SqrMagnitude(tree1))
                    return -1;

                return 0;

            });

            var nearestTree = trees.Count > 0 ? trees[0] : null;
            if (nearestTree != null && nearestTree.HasResources())
                _blackboard.SetVariable(BlackboardConst.Tree, nearestTree);
            else
                _blackboard.RemoveVariable(BlackboardConst.Tree);
        }

        private float SqrMagnitude(Component tree)
        {
            return (tree.transform.position - _centerPoint.position).sqrMagnitude;
        }
    }
}