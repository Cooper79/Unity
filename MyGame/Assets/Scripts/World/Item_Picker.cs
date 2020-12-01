using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item_Picker : MonoBehaviour
{
    [SerializeField] private int _HealValue;
    private void OnTriggerEnter2D(Collider2D info)
    {
        info.GetComponent<Player_Controller>().RestoreHP(_HealValue);
        Destroy(gameObject);
    }
}
