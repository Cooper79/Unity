using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(Animator))]
public class Movement_Controller : MonoBehaviour
{
    public event Action<bool> OnGetHurt = delegate { };
    private Rigidbody2D _playerRB;
    private Animator _playerAnimator;
    private Player_Controller _playerController;

    [Header("Horizontal Movement")]
    [SerializeField] private float _speed;
    private bool _faceRight = true;
    private bool _CanMove = true;


    [Header("Jumping")]
    [SerializeField] private float _jumpforce;
    [SerializeField] private float _radius;
    [SerializeField] private bool _AirControl;
    [SerializeField] private Transform _groundCheck;
    [SerializeField] private LayerMask _whatIsGround;
    private bool _canDoubleJump;
    private bool _grounded;

    [Header("Crawling")]
    [SerializeField] private Transform _cellCheck;
    [SerializeField] private Collider2D _headCollider;
    private bool _canStand;

    [Header("Casting")]
    [SerializeField] private GameObject _LittleKnife;
    [SerializeField] private Transform _LKPoint;
    [SerializeField] private float _LittleKnifeSpeed;
    private bool _isCasting;

    [Header("Attack")]
    [SerializeField] private Transform _AttackPoint;
    [SerializeField] private int _Adamage;
    [SerializeField] private float _AttackRange;
    [SerializeField] private LayerMask _enemies;

    [Header("Hard Attack")]
    [SerializeField] private Transform _PowerAttackPoint;
    [SerializeField] private int _Pdamage;
    [SerializeField] private float _PowerAttackRange;
    [SerializeField] private LayerMask _Enemies;
    private bool _isAttacking;


    [SerializeField] private float _pushForce;
    private float _lasthurttime;


    void Start()
    {
        _playerRB = GetComponent<Rigidbody2D>();
        _playerRB = GetComponent<Rigidbody2D>();
        _playerAnimator = GetComponent<Animator>();
    } 

    private void FixedUpdate()
    {
        _grounded = Physics2D.OverlapCircle(_groundCheck.position, _radius, _whatIsGround);
        if (_playerAnimator.GetBool("Hurt") && _grounded && Time.time - _lasthurttime > 0.5f)
            EndHurt();
    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(_groundCheck.position, _radius);
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(_cellCheck.position, _radius);
        Gizmos.color = Color.black;
        Gizmos.DrawWireSphere(_AttackPoint.position, _AttackRange);
        Gizmos.color = Color.grey;
        Gizmos.DrawWireSphere(_PowerAttackPoint.position, _PowerAttackRange);
    }

    void Flip()
    {
        _faceRight = !_faceRight;
        transform.Rotate(0, 180, 0);
    }

    public void Move(float move, bool jump, bool crawling)
    {
        if (!_CanMove)
            return;

        #region Movement

        
        if (move != 0 && (_grounded || _AirControl))
            _playerRB.velocity = new Vector2(_speed * move, _playerRB.velocity.y);

        if (move > 0 && !_faceRight)
        {
            Flip();
        }
        else if (move < 0 && _faceRight)
        {
            Flip();
        }
        #endregion

        #region Jumping

        if (jump)
        {
            if (_grounded)
            {
                _playerRB.velocity = new Vector2(_playerRB.velocity.x, _jumpforce);
                _canDoubleJump = true;
            }
            else if (_canDoubleJump)
            {
                _playerRB.velocity = new Vector2(_playerRB.velocity.x, _jumpforce);
                _canDoubleJump = false;
            }
        }
        #endregion

        #region Crawling
        _canStand = !Physics2D.OverlapCircle(_cellCheck.position, _radius, _whatIsGround);

        if (crawling)
        {
            _headCollider.enabled = false;
        }
        else if (!crawling && _canStand)
        {
            _headCollider.enabled = true;
        }
        #endregion

        #region Animation
        _playerAnimator.SetFloat("Speed", Mathf.Abs(move));
        _playerAnimator.SetBool("Jump", !_grounded);
        #endregion

        
    }
    public void StartPowerAttack()
    {
        if (_isAttacking)
            return;
        _playerAnimator.SetBool("PowerAttack", true);
        _isAttacking = true;
    }

    private void PowerAttack()
    {
        Collider2D[] enemies = Physics2D.OverlapCircleAll(_PowerAttackPoint.position, _PowerAttackRange, _enemies);

        for (int i = 0; i < enemies.Length; i++)
        {
            
            EnemyControllerBase enemy = enemies[i].GetComponent<EnemyControllerBase>();
            enemy.TakeDamage(_Pdamage, DamageType.PowerStrike);
            
        }
    }

    public void EndPowerAttack()
    {
        _playerAnimator.SetBool("PowerAttack", false);
        _isAttacking = false;
    }
    public void StartAttack()
    {
        if (_isAttacking)
            return;
        _playerAnimator.SetBool("Attack", true);
        _isAttacking = true;
    }

    private void Attack()
    {
        Collider2D[] enemies = Physics2D.OverlapCircleAll(_AttackPoint.position, _AttackRange, _enemies);
        
        for(int i = 0; i < enemies.Length; i++)
        {

            EnemyControllerBase enemy = enemies[i].GetComponent<EnemyControllerBase>();
            enemy.TakeDamage(_Adamage);
        }
    }

    public void EndAttack()
    {
        _playerAnimator.SetBool("Attack", false);
        _isAttacking = false;
    }
   

    public void StartCasting()
    {
        if (_isCasting)
            return;
        _isCasting = true;
        _playerAnimator.SetBool("Casting", true);
    }

    private void CastLittleKnife()
    {
        GameObject littleknife = Instantiate(_LittleKnife, _LKPoint.position, Quaternion.identity);
        littleknife.GetComponent<Rigidbody2D>().velocity = transform.right * _LittleKnifeSpeed;
        littleknife.GetComponent<SpriteRenderer>().flipX = !_faceRight;
        
    }

    public void EndCasting()
    {
        _isCasting = false;
        _playerAnimator.SetBool("Casting", false);
    }

    private void EndAnimations()
    {
        _playerAnimator.SetBool("Attack", false);
        _playerAnimator.SetBool("PowerAttack", false);
        _playerAnimator.SetBool("Casting", false);
    }

    public void GetHurt(Vector2 position)
    {

        _lasthurttime = Time.time;
        _CanMove = false;
        OnGetHurt(false);
        Vector2 pushDirection = new Vector2();
        pushDirection.x = position.x > transform.position.x ? -1 : 1;
        pushDirection.y = 1;
        _playerAnimator.SetBool("Hurt", true);
        EndAnimations();
        _playerRB.AddForce(pushDirection * _pushForce, ForceMode2D.Impulse);
    }

    private void EndHurt()
    {
        
        _CanMove = true;
        _playerAnimator.SetBool("Hurt", false);
        OnGetHurt(true);
        ResetPlayer();
        
    }

    private void ResetPlayer()
    {
        _playerAnimator.SetBool("Attack", false);
        _playerAnimator.SetBool("PowerAttack", false);
        _playerAnimator.SetBool("Casting", false);
        _playerAnimator.SetBool("Hurt", false);
        
        _isCasting = false;
        _isAttacking = false;
        _CanMove = true;
    }
}
