using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(Rigidbody), typeof(Collider), typeof(Animator))]
[RequireComponent(typeof(NavMeshAgent))]

public abstract class Living_Creature : MonoBehaviour
{
    public Rigidbody CreatureRB { get; private set; }
    public Collider CreatureCollider { get; private set; }
    public NavMeshAgent CreatureNavMeshAgent { get; private set; }
    public Animator CreatureAnimator { get; private set; }

    public Service_Manager Service_Manager { get; private set; }

    
    public LivingCreatureActionController ActionController { get; protected set; }

    
    protected virtual void Start()
    {
        CreatureRB = GetComponent<Rigidbody>();
        CreatureCollider = GetComponent<Collider>();
        CreatureNavMeshAgent = GetComponent<NavMeshAgent>();
        CreatureAnimator = GetComponent<Animator>();
        Service_Manager = Service_Manager.instance;
    }

    
}
