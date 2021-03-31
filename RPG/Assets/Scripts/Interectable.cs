using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Interectable : MonoBehaviour
{
    private bool _isFocused;
    private bool _hasInteracted;
    protected Player_Creature _player;
    [SerializeField] private float _interactionDistance;
    public virtual float StoppingDistance 
    { 
        get { return _interactionDistance * 0.4f; }
    }

    public void OnFocus(Player_Creature player)
    {
        _isFocused = true;
        _player = player;
    }

    public void OnUnFocus()
    {
        _isFocused = false;
        _hasInteracted = false;
    }

    // Start is called before the first frame update
    protected virtual void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (_isFocused && _player != null)
        {
            
            Vector3 centerPoint = new Vector3(transform.position.x, _player.transform.position.y, transform.position.z);

            if (Vector3.Distance(centerPoint, _player.transform.position) < _interactionDistance && !_hasInteracted)
            {
                Interact();
            }
        }
    }

    protected virtual void Interact()
    {
        _hasInteracted = true;
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, _interactionDistance);
    }
}
