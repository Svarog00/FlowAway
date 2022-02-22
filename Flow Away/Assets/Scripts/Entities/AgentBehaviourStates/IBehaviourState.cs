using UnityEngine;

namespace Assets.Scripts.BehaviourStates
{
    public interface IBehaviourState
    {
        public void Handle();
        public void Enter();
        public void Exit();
    }
}