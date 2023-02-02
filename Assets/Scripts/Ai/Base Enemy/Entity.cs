using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class Entity : MonoBehaviour, IDetectionSound
{
    [Header("Parameters")]
    public EntityStats stats;

    [Header("Ruta")]
    public List<Transform> wayPoints = new List<Transform>();

    [Tooltip("El NPC tiene que volver por donde vino o tiene que hacer un giro")]
    public bool isLoopPath;


    [Header("Detection")]
    [Tooltip("layer del player")]
    public LayerMask playerMask;

    [Tooltip("layers de los obstaculos ejem : Paredes")]
    public LayerMask obstacleMask;

    public Vector3 target { get; private set; }

    /////////////////////////////////////////////////////////////////////////////

    public StateMachine stateMachine = new StateMachine();
    public NavMeshAgent agent { get; private set; }

    #region States
    private IdleState idle;
    private FollowPlayerState move;
    private AttackState attack;
    private PatrolState patrol;
    private FollowSoundState detection;
    private DeathState death;
    #endregion

    #region Custom Gizmo

    [Header("Settings Gizmo")]
    [Tooltip("Cambia el color de las Flechas que señalan el camino de patrullaje")]
    public Color colorLine = new Color(0, 0, 0, 1);

    [Tooltip("Cambia el Color de la esfera de vision")]
    public Color colorSphere = new Color(0, 0, 0, 1);
    [Tooltip("Cambia el color de las lineas limites de vision")]
    public Color colorLimitVision = new Color(0, 0, 0, 1);

    #endregion

    public virtual void Start()
    {
        agent = GetComponent<NavMeshAgent>();

        idle = new IdleState(this);
        move = new FollowPlayerState(this);

        attack = new AttackState(this);

        patrol = new PatrolState(this);
        detection = new FollowSoundState(this);

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
        idle.AddTransition(patrol);
        idle.AddTransition(detection);
        idle.AddTransition(death);

        move.AddTransition(idle);
        move.AddTransition(detection);
        move.AddTransition(death);

        attack.AddTransition(idle);

        stateMachine.Init(idle);
    }

    #region FollowPath
    public int counterIndex { get; private set; }

    public void FollowPath()
    {
        if (counterIndex <= wayPoints.Count - 1)
        {
            // agent.SetDestination(wayPoints[counterIndex].position);

            var moveSpeedV = Mathf.Clamp(Vector3.Distance(transform.position, wayPoints[counterIndex].transform.position), stats.minMoveSpeed, stats.maxMoveSpeed);

            transform.position += transform.forward.normalized * moveSpeedV * Time.deltaTime;

            var rotateSpeedV = Mathf.Clamp(Vector3.Distance(transform.position, wayPoints[counterIndex].transform.position), stats.minRotateSpeed, stats.maxRotateSpeed);

            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation((wayPoints[counterIndex].transform.position - transform.position).normalized), rotateSpeedV * Time.deltaTime);

            if (Vector3.Distance(transform.position, wayPoints[counterIndex].transform.position) < 2f)
            {
                counterIndex++;
            }
        }
        else
        {
            if (!isLoopPath)
                wayPoints.Reverse();

            counterIndex = 0;
        }
    }

    #endregion

    #region Cono Vision
    public bool isOnVision()
    {
        var countCollision = Physics.OverlapSphere(transform.position, stats.rangeVision, playerMask);

        if (countCollision.Length > 0)
        {
            var playerPosition = countCollision[0].transform;

            target = playerPosition.position;

            RaycastHit hit;

            if (!Physics.Raycast(transform.position, target - transform.position, out hit, Vector3.Distance(transform.position, target), obstacleMask))
            {
                var angle = Vector3.Angle(target - transform.position, transform.forward);

                return angle < stats.angleVision;
            }
        }

        return false;
    }
    #endregion

    #region Detector Sound

    public bool isDetectedSound { get; private set; }

    public void DetectCollisionSound()
    {
        isDetectedSound = true;
    }

    #endregion

    private void OnDrawGizmos()
    {
        if (wayPoints.Count > 0)
        {
            Gizmos.color = colorLine;

            for (int i = 0; i < wayPoints.Count - 1; i++)
            {
                Gizmos.DrawLine(wayPoints[i].position, wayPoints[i + 1].position);

                Vector3 start = wayPoints[i].position;

                Vector3 end = wayPoints[i + 1].position;

                Vector3 arrowTip = end;

                Vector3 left = end - Quaternion.LookRotation(Vector3.forward) * (Quaternion.Euler(0, -50, 0) * (end - start).normalized);
                Vector3 right = end - Quaternion.LookRotation(Vector3.forward) * (Quaternion.Euler(0, 50, 0) * (end - start).normalized);

                Gizmos.DrawLine(arrowTip, left);
                Gizmos.DrawLine(arrowTip, right);
            }
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////

        Gizmos.color = colorSphere;

        Gizmos.DrawWireSphere(transform.position, stats.rangeVision);

        Gizmos.color = colorLimitVision;

        Gizmos.DrawRay(transform.position, Quaternion.Euler(0, stats.angleVision, 0) * transform.forward * stats.rangeVision);

        Gizmos.DrawRay(transform.position, Quaternion.Euler(0, -stats.angleVision, 0) * transform.forward * stats.rangeVision);

        ////////////////////////////////////////////////////////////////////////////////////////////////

    }
}