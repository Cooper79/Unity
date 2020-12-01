using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectileController : MonoBehaviour
{
    [SerializeField] private int _damage;
    private float _lastincounter;
    private void OnTriggerEnter2D(Collider2D info)
    {
        if (Time.time - _lastincounter< 0.2f)
            return;

        _lastincounter = Time.time;

        Player_Controller player = info.GetComponent<Player_Controller>();

        if (player != null)
        {
            player.Takedamage(_damage);
        }

        Destroy(gameObject);
    }
}
