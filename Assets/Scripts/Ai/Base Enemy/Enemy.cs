using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent), typeof(Animator))]
public class Enemy : MonoBehaviour, IDetectionSound, IDamage
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

    public Transform playerTarget { get; private set; }

    /////////////////////////////////////////////////////////////////////////////

    public StateMachine stateMachine = new StateMachine();
    public NavMeshAgent agent { get; private set; }
    public Animator animator { get; private set; }

    #region States
    private IdleState idle;
    private FollowPlayerState followPlayer;
    private AttackState attack;
    private PatrolState patrol;
    private FollowSoundState followSound;
    private SearchState search;
    private DeathState death;
    #endregion

    #region Custom Gizmo

    [Header("Settings Gizmo")]
    public bool isActiveLine = false;

    [Tooltip("Cambia el color de las Flechas que señalan el camino de patrullaje")]
    public Color colorLine = new Color(0, 0, 0, 1);

    public bool isActiveRangeVision = false;

    [Tooltip("Cambia el Color de la esfera de vision")]
    public Color colorRangeVision = new Color(0, 0, 0, 1);

    public bool isActiveRangeAttack = false;

    [Tooltip("Cambia el Color de la esfera de vision")]
    public Color colorRangeAttack = new Color(0, 0, 0, 1);

    public bool isActiveAngleVision = false;

    [Tooltip("Cambia el color de las lineas limites de vision")]
    public Color colorLimitVision = new Color(0, 0, 0, 1);

    [Header("Route Customization")]

    [Tooltip("Puedes Visualizar cual es la ruta que esta creando")]
    public bool isActivePathView;

    [Tooltip("Cambia te color la ruta")]
    public Color colorPathView = new Color(0, 0, 0, 1);

    [Header("Active Current StateMachine")]
    public bool isActiveCurrentStateMachine = false;

    #endregion

    public virtual void Start()
    {
        agent = GetComponent<NavMeshAgent>();

        animator = GetComponent<Animator>();

        currentDamage = stats.maxLife;

        foreach (var item in wayPoints)
        {
            item.parent = null;
        }

        idle = new IdleState(this);
        followPlayer = new FollowPlayerState(this);
        attack = new AttackState(this);
        patrol = new PatrolState(this);
        followSound = new FollowSoundState(this);
        search = new SearchState(this);
        death = new DeathState(this);

        SetChangeState();
    }

    public bool debug;

    public virtual void Update()
    {
        if (stateMachine.current != null)
            stateMachine.OnUpdate();

        if (isActiveCurrentStateMachine && debug)
            Debug.Log(stateMachine.current);

        animator.SetFloat("Speed", agent.velocity.magnitude);
    }

    public virtual void SetChangeState()
    {
        idle.AddTransition(patrol);
        idle.AddTransition(followSound);
        idle.AddTransition(followPlayer);
        idle.AddTransition(attack);
        idle.AddTransition(death);

        followPlayer.AddTransition(idle);
        followPlayer.AddTransition(attack);
        followPlayer.AddTransition(death);

        followSound.AddTransition(followPlayer);
        followSound.AddTransition(patrol);
        followSound.AddTransition(idle);
        followSound.AddTransition(search);
        followSound.AddTransition(death);

        patrol.AddTransition(idle);
        patrol.AddTransition(followSound);
        patrol.AddTransition(followPlayer);
        patrol.AddTransition(death);

        attack.AddTransition(idle);
        attack.AddTransition(followPlayer);

        search.AddTransition(patrol);
        search.AddTransition(followPlayer);
        search.AddTransition(followSound);
        search.AddTransition(death);

        stateMachine.Init(patrol);
    }

    #region FollowPath
    public int counterIndex { get; set; }

    public bool isWaiting { get; set; }

    public void FollowPath()
    {
        if (counterIndex <= wayPoints.Count - 1)
        {
            FollowTarget(wayPoints[counterIndex].position, stats.moveSpeed);

            RotateNetxNode();

            if (Vector3.Distance(transform.position, wayPoints[counterIndex].transform.position) < 2f)
            {
                counterIndex++;
            }
        }
        else
        {
            if (!isLoopPath)
            {
                isWaiting = true;

                stateMachine.Transition<IdleState>();
            }

            counterIndex = 0;
        }
    }

    public void RotateNetxNode()
    {
        var rotateSpeedV = Mathf.Clamp(Vector3.Distance(transform.position, wayPoints[counterIndex].transform.position), stats.minRotateSpeed, stats.maxRotateSpeed);

        if (agent.path.corners.Length > 1)
            RotateTarget(agent.path.corners[1], rotateSpeedV);
        else
            RotateTarget(wayPoints[counterIndex].transform.position, rotateSpeedV);

    }

    public void RotateTargetPlayer()
    {
        var rotateSpeedV = Mathf.Clamp(Vector3.Distance(transform.position, agent.path.corners[1]), stats.minRotateSpeed, stats.maxRotateSpeed);

        if (agent.path.corners.Length > 1)
            RotateTarget(agent.path.corners[1], rotateSpeedV);
        else
        {
            if (playerTarget != null)
                RotateTarget(playerTarget.position, rotateSpeedV);
        }

    }

    public void RotateTarget(Vector3 target, float speed)
    {
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation((target - transform.position).normalized), speed * Time.deltaTime);
    }

    public void FollowTarget(Vector3 target, float speed)
    {
        agent.speed = speed;

        agent.acceleration = stats.moveSpeed + 4.5f;

        agent.SetDestination(target);

        // var moveSpeedV = Mathf.Clamp(Vector3.Distance(transform.position, wayPoints[counterIndex].transform.position), stats.minMoveSpeed, stats.maxMoveSpeed);

        // transform.position += transform.forward.normalized * moveSpeedV * Time.deltaTime;
    }

    #endregion

    #region Cono Vision

    public bool isOnVision()
    {
        var countCollision = Physics.OverlapSphere(transform.position, stats.rangeVision, playerMask);

        if (countCollision.Length > 0)
        {
            var playerPosition = countCollision[0].transform;

            playerTarget = playerPosition;

            if (!Physics.Raycast(transform.position, playerTarget.position - transform.position, Vector3.Distance(transform.position, playerTarget.position), obstacleMask))
            {
                var angle = Vector3.Angle(playerTarget.position - transform.position, transform.forward);

                return angle < stats.angleVision;
            }
        }

        return false;
    }
    #endregion

    #region Detector Sound

    public bool isDetectedSound { get; private set; }

    public void DetectCollisionSound(Vector3 target)
    {
        stateMachine.Transition<FollowSoundState>(target);
    }

    #endregion

    public IEnumerator DisableObject(float disableTime = 2)
    {
        yield return new WaitForSeconds(disableTime);

        gameObject.SetActive(false);
    }


    public Vector3 asd;

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.black;

        Gizmos.DrawSphere(asd, 2f);
        // Gizmos.DrawSphere(wayPoints[counterIndex].transform.position, .5f);

        ////////////////////////////////////////////////////////////////////////////////////////////////

        if (wayPoints.Count > 0 && isActiveLine)
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

        if (isActiveRangeVision)
        {
            Gizmos.color = colorRangeVision;

            Gizmos.DrawWireSphere(transform.position, stats.rangeVision);
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////

        if (isActiveRangeAttack)
        {
            Gizmos.color = colorRangeAttack;

            Gizmos.DrawWireSphere(transform.position, stats.rangeAttack);
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////

        if (isActiveAngleVision)
        {
            Gizmos.color = colorLimitVision;

            Gizmos.DrawRay(transform.position, Quaternion.Euler(0, stats.angleVision, 0) * transform.forward * stats.rangeVision);

            Gizmos.DrawRay(transform.position, Quaternion.Euler(0, -stats.angleVision, 0) * transform.forward * stats.rangeVision);
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////

        if (isActivePathView && agent != null)
        {
            if (agent.path.corners.Length > 1)
            {
                Gizmos.color = colorPathView;

                for (int i = 0; i < agent.path.corners.Length - 1; i++)
                {
                    Gizmos.DrawLine(agent.path.corners[i], agent.path.corners[i + 1]);
                }

                foreach (var item in agent.path.corners)
                {
                    Gizmos.DrawSphere(item, .25f);
                }
            }
        }
    }

    private float currentDamage;

    public void Damage(float damage)
    {
        if (currentDamage > stats.maxLife)
        {
            currentDamage -= damage;
        }
        else if (currentDamage <= stats.maxLife && stateMachine.current.ToString() != death.ToString())
        {
            animator.SetTrigger("Death");
            stateMachine.Transition<DeathState>();
        }
    }

    public void FireDamage(float damage)
    {

    }
}