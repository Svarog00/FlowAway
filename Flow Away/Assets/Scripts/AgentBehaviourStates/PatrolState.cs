using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.BehaviourStates
{
    [CreateAssetMenu(fileName = "PatrolState", menuName = "PatrolState", order = 3)]
    public class PatrolState : AIBehaviourState
    {
        [SerializeField] private float _waitTime = 0f;

        private EnemyMovement _movement;

        private Transform[] _patrolSpots;

        private float _curWaitTime;
        private int _randomSpot = 0;

        public override void Init()
        {
            
        }

        public override void Handle()
        {
            
        }
    }
}