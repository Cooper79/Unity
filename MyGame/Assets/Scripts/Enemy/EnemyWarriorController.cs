using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWarriorController : EnemyControllerBase
{
    protected Player_Controller _player;
    protected bool _isAngry;
    protected bool _Attacking;

    [SerializeField] private int _damage;
    [SerializeField] protected float _angerRange;
    [SerializeField] private Transform _strikePoint;
    [SerializeField] private float _strikeRange;
    [SerializeField] private LayerMask _enemies;

    [Header("Transition")]
    [SerializeField] private float _waitTime;
    protected override void Start()
    {
        base.Start();
        _player = FindObjectOfType<Player_Controller>();
        StartCoroutine(ScanForPlayer());
    }

    
    protected override void Update()
    {
        if (_isAngry)
            return;
        base.Update();
    }

    protected override void ChangeState(EnemyState state)
    {
        base.ChangeState(state);

        switch (state)
        {
            case EnemyState.Strike:
                _Attacking = true;
                _enemyRB.velocity = Vector2.zero;
                break;
        }
    }

    protected override void EndState()
    {
        switch (_currentState)
        {
            case EnemyState.Strike:
                _Attacking = false;
                break;
        }

        base.EndState();
    }

    protected override void ResetState()
    {
        base.ResetState();
        _enemyAnimator.SetBool(EnemyState.Strike.ToString(), false);
        _enemyAnimator.SetBool(EnemyState.Death.ToString(), false);
    }

    protected virtual void DoStateAction()
    {
        switch (_currentState)
        {
            case EnemyState.Strike:
                Strike();
                break;
        }
    }

    protected void Strike()
    {
        Collider2D player = Physics2D.OverlapBox(_strikePoint.position, new Vector2(_strikeRange, _strikeRange), 0, _enemies);
        if (player != null)
        {
            Player_Controller playerController = player.GetComponent<Player_Controller>();
            if (playerController != null)
                playerController.Takedamage(_damage);
        }
    }

    protected IEnumerator ScanForPlayer()
    {
        while (true)
        {
            CheckPlayerInRange();
            yield return new WaitForSeconds(1f);
        }
    }

    protected virtual void CheckPlayerInRange()
    {
        if (_player == null || _Attacking)
        {
            return;
        }

        if (Vector2.Distance(transform.position, _player.transform.position) < _angerRange)
        {
            _isAngry = true;
            TurnToPlayer();
            ChangeState(EnemyState.Strike);
        }
        else
            _isAngry = false;

    }

    protected void TurnToPlayer()
    {
        if (_player.transform.position.x - transform.position.x > 0 && !_faceright)
            Flip();
        else if (_player.transform.position.x - transform.position.x < 0 && _faceright)
            Flip();
    }
}
