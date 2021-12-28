using UnityEngine;

namespace Assets.Scripts.BehaviourStates
{
    [CreateAssetMenu(fileName = "ChaseState", menuName = "ChaseState", order = 1)]
    public class ChaseState : AIBehaviourState
    {
        private EnemyMovement _movement;
        private Transform _target;

        [SerializeField] private float _chaseTimeOut;
        private float _elapsedTime = 0f;

        public override void Init()
        {
            
        }

        public override void Handle()
        {
            
        }
    }
}