using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;
using UnityEngine.UI;

public class Player_Controller : MonoBehaviour
{
    private ServiceManager _serviceManager;
    [SerializeField] private int _maxHP;
    private int _currentHP;

    [SerializeField] Slider _hpSlider;
   
    Movement_Controller _playerMovement;
    Vector2 _startPosition;

    private bool _canBeDamaged = true;
    void Start()
    {
        _playerMovement = GetComponent<Movement_Controller>();
        _playerMovement.OnGetHurt += OngetHurt;
        _currentHP = _maxHP;
        _hpSlider.maxValue = _maxHP;
        _hpSlider.value = _maxHP;
        _startPosition = transform.position;
        _serviceManager = ServiceManager.Instanse;
    }

    public void Takedamage(int damage, DamageType type = DamageType.Casual, Transform enemy = null)
    {
        
        if (!_canBeDamaged)
            return;
        
        _currentHP -= damage;
        if (_currentHP <= 0)
        {
            OnDeath();
        }

        switch (type)
        {
            case DamageType.PowerStrike:
                _playerMovement.GetHurt(enemy.position);
                break;
        }

        _hpSlider.value = _currentHP;


        
    }

    private void OngetHurt(bool CanBeDamaged)
    {
        _canBeDamaged = CanBeDamaged;
    }
    public void RestoreHP(int hp)
    {
        _currentHP += hp;

        if (_currentHP > _maxHP)
        {
            _currentHP = _maxHP;
        }
        _hpSlider.value = _currentHP;
        
    }
    public void OnDeath()
    {
        _serviceManager.Restart();
    }
}
