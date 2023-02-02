using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class Entity : MonoBehaviour
{
    [Header("Parameters")]
    public EntityStats stats;

    [Header("Ruta")]
    public List<Transform> wayPoints = new List<Transform>();


    [Header("Detection")]
    [Tooltip("layer del player")]
    public LayerMask mask;
    [Tooltip("layers de los obstaculos ejem : Paredes")]
    public LayerMask obstacle;
    public Vector3 target { get; private set; }

    /////////////////////////////////////////////////////////////////////////////

    public StateMachine stateMachine = new StateMachine();
    public NavMeshAgent agent { get; private set; }

    #region States
    private IdleState idle;
    private MoveState move;
    private AttackState attack;
    private PatrolState patrol;
    private DetectionState detection;
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
        stateMachine.Init(detection);
    }

    #region Path

    public int counterIndex { get; private set; }

    public void FollowPath()
    {
        if (counterIndex <= wayPoints.Count - 1)
        {
            transform.position += transform.forward * stats.moveSpeed * Time.deltaTime;

            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(wayPoints[counterIndex].transform.position - transform.position), 1 * Time.deltaTime);

            if (Vector3.Distance(transform.position, wayPoints[counterIndex].transform.position) < 1f)
            {
                counterIndex++;
            }
        }
    }

    #endregion

    #region Cono Vision
    public bool isOnVision()
    {
        var countCollision = Physics.OverlapSphere(transform.position, stats.rangeVision, mask);

        if (countCollision.Length > 0)
        {
            var playerPosition = countCollision[0].transform;

            target = playerPosition.position;

            RaycastHit hit;

            if (!Physics.Raycast(transform.position, target - transform.position, out hit, Vector3.Distance(transform.position, target), obstacle))
            {
                var angle = Vector3.Angle(target - transform.position, transform.forward);

                return angle < stats.angleVision;
            }
        }

        return false;
    }
    #endregion

    #region 
    public void DetectCollisionSound(float rangeSound)
    {
        Debug.Log(rangeSound);
    }
    #endregion

    private void OnDrawGizmosSelected()
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