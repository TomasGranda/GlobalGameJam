using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class Entity : MonoBehaviour
{
    public StateMachine stateMachine = new StateMachine();

    public NavMeshAgent agent { get; private set; }

    private IdleState idle;
    private MoveState move;
    private AttackState attack;
    private PatrolState patrol;
    private DetectionState detection;
    private DeathState death;

    public virtual void Start()
    {
        agent = GetComponent<NavMeshAgent>();

        idle = new IdleState(this);
        move = new MoveState(this);

        attack = new AttackState(this);

        patrol = new PatrolState(this);
        detection = new DetectionState(this);

        death = new DeathState(this);

        SetChangeState();
    }

    public virtual void Update()
    {
        if (stateMachine.current != null)
            stateMachine.OnUpdate();
    }

    public virtual void SetChangeState()
    {
        stateMachine.Init(patrol);
    }
}