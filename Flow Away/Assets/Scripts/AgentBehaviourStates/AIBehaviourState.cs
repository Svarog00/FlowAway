using UnityEngine;

namespace Assets.Scripts.BehaviourStates
{
    public abstract class AIBehaviourState : ScriptableObject
    {
        [HideInInspector]
        public AgentBehaviour Agent;

        public virtual void Init() { }
        public abstract void Handle();
    }
}