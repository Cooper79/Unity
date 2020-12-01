using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class Damage_Dealer : MonoBehaviour
{
    [SerializeField] private int _damage;
    [SerializeField] private float _timeDelay;
    private Player_Controller _player;
    private DateTime _lastincounter;

    private void OnTriggerEnter2D(Collider2D info)
    {
        if ((DateTime.Now - _lastincounter).TotalSeconds < _timeDelay/2)
            return;

        _lastincounter = DateTime.Now;

        _player = info.GetComponent<Player_Controller>();

        if (_player != null)
        {
            _player.Takedamage(_damage);
        }

        
    }

    private void OnTriggerExit2D(Collider2D info)
    {
        if (_player == info.GetComponent<Player_Controller>())
            _player = null;
    }

    private void Update()
    {
        if (_player != null && (DateTime.Now - _lastincounter).TotalSeconds > _timeDelay)
        {
            _player.Takedamage(_damage);
            _lastincounter = DateTime.Now;
        }
    }
}
