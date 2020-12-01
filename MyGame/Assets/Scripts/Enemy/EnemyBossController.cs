using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class EnemyBossController : EnemyArcherController
{
    [SerializeField] private float _idleTime;

    private bool _inAttackRange;

    [Header("Strike")]
    [SerializeField] private Transform _strikePoint;
    [SerializeField] private int _damage;
    [SerializeField] private float _strikeRange;
    [SerializeField] private LayerMask _enemies;


    [Header("PowerStrike")]
    [SerializeField] private Collider2D _strikeCollider;
    [SerializeField] private int _powerStrikeDamage;
    [SerializeField] private float _powerStrikeSpeed;
    [SerializeField] private float _powerStrikeRange;

    [Header("Transition")]
    [SerializeField] private float _waitTime;
    [SerializeField] GameObject _palka;
    [SerializeField] Collider2D _collider;
    [SerializeField] private float _timetocreate;

    private float _currentStrikeRange;
    private bool _fightStarted;

    private EnemyState _stateOnHold;
    private EnemyState[] _attackState = { EnemyState.Strike, EnemyState.PowerStrike };

    #region Unity Methods
    protected override void FixedUpdate()
    {
        base.FixedUpdate();
        if (_currentState == EnemyState.Move && _Attacking)
        {
            TurnToPlayer();
            if (CanAttack())
            {
                ChangeState(_stateOnHold);
            }
                
        }
    }
    protected override void OnDrawGizmos()
    {
        base.OnDrawGizmos();
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(_strikePoint.position, new Vector3(_strikeRange, _strikeRange, 0));
    }
    #endregion

    #region Public Methods

    public override void TakeDamage(int damage, DamageType type = DamageType.Casual, Transform player = null)
    {
        if (_currentState == EnemyState.PowerStrike && type != DamageType.Projectile)
            return;

        base.TakeDamage(damage, type, player);
    }

    #endregion
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

    protected void StrikeWithPower()
    {
        _strikeCollider.enabled = true;
        _enemyRB.velocity = transform.right * _powerStrikeSpeed;
    }
    
    protected void EndPowerStrike()
    {
        _strikeCollider.enabled = false;
        _enemyRB.velocity = Vector2.zero;
    }

    protected override void TryToDamage(Collider2D enemy)
    {
        if (_currentState == EnemyState.Idle || _currentState == EnemyState.Shoot)
            return;
        base.TryToDamage(enemy);
    }

    protected override void CheckPlayerInRange()
    {
        if (_player == null || _isAngry)
            return;

        if (Vector2.Distance(transform.position, _player.transform.position) < _angerRange)
        {
            _isAngry = true;
            if (!_fightStarted)
            {
                StopAllCoroutines();
                StartCoroutine(BeginNewCircle());
            }
        }
        else
            _isAngry = false;
    }

    protected override void ChangeState(EnemyState state)
    {
        base.ChangeState(state);

        switch (_currentState)
        {
            case EnemyState.PowerStrike:
            case EnemyState.Strike:
                _Attacking = true;
                _currentStrikeRange = state == EnemyState.Strike ? _strikeRange : _powerStrikeRange;
                _enemyRB.velocity = Vector2.zero;
                if (!CanAttack())
                {
                    _stateOnHold = state;
                    _enemyAnimator.SetBool(_currentState.ToString(), false);
                    ChangeState(EnemyState.Move);
                }
                break;
            case EnemyState.Hurt:
                _Attacking = false;
                _enemyRB.velocity = Vector2.zero;
                StopAllCoroutines();
                break;

        }
    }

    protected override void ResetState()
    {
        base.ResetState();
        _enemyAnimator.SetBool(EnemyState.PowerStrike.ToString(), false);
        _enemyAnimator.SetBool(EnemyState.Strike.ToString(), false);
    }

    private bool CanAttack()
    {
        return Vector2.Distance(transform.position, _player.transform.position) < _currentStrikeRange;
    }


    protected override void DoStateAction()
    {
        base.DoStateAction();
        switch (_currentState)
        {
            case EnemyState.Strike:
                Strike();
                break;
            case EnemyState.PowerStrike:
                StrikeWithPower();
                break;
        }
    }

    protected override void EndState()
    {
        switch (_currentState)
        {
            case EnemyState.PowerStrike:
                EndPowerStrike();
                _Attacking = false;
                break;
            case EnemyState.Strike:
                _Attacking = false;
                break;
            
        }

        base.EndState();

        if (_currentState == EnemyState.Shoot || _currentState == EnemyState.PowerStrike || _currentState == EnemyState.Strike || _currentState == EnemyState.Hurt)
        {
            StartCoroutine(BeginNewCircle());
        }
    }

    private IEnumerator BeginNewCircle()
    {
        if (_currentState == EnemyState.Death)
            yield break;

        if (_fightStarted)
        {
            _isAngry = false;
            CheckPlayerInRange();
            if (!_isAngry)
            {
                _fightStarted = false;
                StartCoroutine(ScanForPlayer());
                yield break;
            }
            yield return new WaitForSeconds(_waitTime);
        }
        _fightStarted = true;
        TurnToPlayer();
        ChooseNextAttackState();
    }

    protected void ChooseNextAttackState()
    {
        int state = Random.Range(0, _attackState.Length);
        ChangeState(_attackState[state]);
    }

    protected override void Barier()
    {
        Destroy(_palka);
            _collider.enabled = true;
        base.Barier();
    }
}
