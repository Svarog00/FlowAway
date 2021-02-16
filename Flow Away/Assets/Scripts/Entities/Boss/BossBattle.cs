using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBattle : MonoBehaviour
{
    enum Phases {phase_0, phase_1, phase_2};

    public Boss boss;

    public Observer obs;

    private Phases currentPhase;

    [SerializeField] private ColliderTrigger colliderTrigger;



    private void Awake()
    {
        currentPhase = Phases.phase_0;
    }
    private void Start()
    {
        colliderTrigger.OnPlayerEnterTrigger += ColliderTriger_OnPlayerEnterTrigger;
        boss.OnBossDamaged += BossBattle_OnDamaged;
        //show UI
    }




    private void ColliderTriger_OnPlayerEnterTrigger(object sender, System.EventArgs e)
    {
        StartBattle();
        colliderTrigger.OnPlayerEnterTrigger -= ColliderTriger_OnPlayerEnterTrigger;
    }


    private void BossBattle_OnDamaged(object sender, System.EventArgs e)
    {
        //Gargoyle took damage
        boss.OnBossDamaged -= BossBattle_OnDamaged;
        Debug.Log("BossWasDamaged");
        //Stage control


        //Apply changes to UI
    }

    private void BossBattle_OnDead(object sender, System.EventArgs e)
    {
        //
        //Player should choose kill or not
    }



    private void StartBattle()
    {
        Debug.Log("Gargoyle, wake up, little shiet");
        ChangePhase();
        //Limit player`s space


        //Display UI


    }

    private void ChangePhase()
    {
        switch (currentPhase)
        {
            case Phases.phase_0:
                currentPhase = Phases.phase_1;
                break;

            case Phases.phase_1:
                currentPhase = Phases.phase_2;
                break;

          
        }
    }
}
