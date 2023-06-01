using Assets.Scripts.BehaviourStates;
using System;
using System.Collections.Generic;

public class EnemyBossBehaviour : AgentBehaviour
{
    private void Start()
    {
        Initialize();

        StateMachine.States = new Dictionary<Type, IBehaviourState>
        {
            //Состояние "пробуждения", когда первый раз босс призывается
            //Состояние первой фазы
            //Состояние второй и последующих фаз
            /*[typeof(PatrolState)] = new PatrolState(this, StateMachine),
            [typeof(ChaseState)] = new ChaseState(this, StateMachine),
            [typeof(EngageState)] = new EngageState(this, StateMachine),*/
        };
    }
}
