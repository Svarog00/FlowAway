using UnityEngine;

namespace Assets.Scripts.BehaviourStates
{
    [CreateAssetMenu(fileName = "EngageState", menuName = "EngageState", order = 2)]
    public class EngageState : AIBehaviourState
    {
        private EnemyAttack _attack;
        private PlayerHealthModel _playerHealth;

        private bool _canAttack = false;

        public override void Init()
        {
            
        }

        public override void Handle()
        {
            
        }
    }
}