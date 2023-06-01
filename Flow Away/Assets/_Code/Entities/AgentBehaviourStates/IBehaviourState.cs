using UnityEngine;

namespace Assets.Scripts.BehaviourStates
{
    public interface IBehaviourState
    {
        public void Enter();
        public void Handle();
        public void Exit();
    }
}