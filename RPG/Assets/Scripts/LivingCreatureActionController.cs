using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LivingCreatureActionController 
{
    [SerializeField] private Living_Creature _livingCreature;
    private ActionType _currnetAction;
    // Start is called before the first frame update
    

    public LivingCreatureActionController(Living_Creature living_Creature)
    {
        _livingCreature = living_Creature;
        _livingCreature.Service_Manager.DestroyHandler += OnDestroy;
        _livingCreature.Service_Manager.UpdateHandler += OnUpdate;
        
    }

    protected virtual void Move(Vector3 destenation, float stoppingDistance = 0.5f)
    {
        _livingCreature.CreatureNavMeshAgent.destination = destenation;
        _livingCreature.CreatureNavMeshAgent.stoppingDistance = stoppingDistance;
        ChangeAction(ActionType.Run);
    }

    protected virtual void ChangeAction(ActionType action)
    {
        ResetAction();
        _currnetAction = action;
        if (_currnetAction != ActionType.None)
        {
            _livingCreature.CreatureAnimator.SetBool(_currnetAction.ToString(), true);
        }
            
    }

    protected virtual void ResetAction()
    {
        if (_currnetAction != ActionType.None)
        {
            _livingCreature.CreatureAnimator.SetBool(_currnetAction.ToString(), false);
        }
        _currnetAction = ActionType.None;
    }

    protected virtual void OnUpdate()
    {
        if (Vector3.Distance(_livingCreature.transform.position, _livingCreature.CreatureNavMeshAgent.destination) <= _livingCreature.CreatureNavMeshAgent.stoppingDistance)
        {
            ChangeAction(ActionType.None);
            _livingCreature.CreatureNavMeshAgent.destination = _livingCreature.transform.position;
        }
    }

    protected virtual void OnDestroy()
    {
        _livingCreature.Service_Manager.DestroyHandler -= OnDestroy;
        _livingCreature.Service_Manager.UpdateHandler -= OnUpdate;
    }

}

public enum ActionType
{
    None,
    Walk,
    Run,
    Sprint,
    Attack,
    Hurt,
    Death,
    Take,
}
