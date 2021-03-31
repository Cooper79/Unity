using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Action_Controller : LivingCreatureActionController
{
    private Player_Creature _playerCreature;
    private Interectable _lastTarget;
    public Player_Action_Controller(Player_Creature player) : base(player)
    {
        _playerCreature = player;
        _playerCreature.Service_Manager.InputController.RightPointerClickHandler += OnRightPointerClicked;
    }

    private void OnRightPointerClicked(Vector3 destenation, Collider collider)
    {
        if (_lastTarget != null)
        {
            _lastTarget.OnUnFocus();
        }

        if (collider != null)
        {
            _lastTarget = collider.GetComponent<Interectable>();
            if (_lastTarget != null)
            {
                _lastTarget.OnFocus(_playerCreature);
                Vector3 centerPoint = new Vector3(_lastTarget.transform.position.x, _playerCreature.transform.position.y, _lastTarget.transform.position.z);
                Move(centerPoint, _lastTarget.StoppingDistance);
                return;
            }
            
        }

        Move(destenation);
    }

    protected override void OnDestroy()
    {
        base.OnDestroy();
        _playerCreature.Service_Manager.InputController.RightPointerClickHandler -= OnRightPointerClicked;
    }
}
