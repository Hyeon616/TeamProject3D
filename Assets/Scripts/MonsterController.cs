using System.Collections;
using UnityEngine;

using UnityEngine.AI;

public class MonsterController : MonoBehaviour
{

    public enum State
    {
        IDLE,
        TRACE,
        ATTACK,
        PATROL,
        DIE
    }



    public State state = State.IDLE;

    public float traceDistance = 10.0f;
    public float attackDistance = 2.5f;

    public bool isDie = false;

    private Transform monsterTr;
    private Transform playerTr;
    private NavMeshAgent agent;
    private Animator anim;
    private StateMachine stateMachine;

    [SerializeField] private Transform[] wayPoints = null;
    private int wayPointIndex;

    private readonly int hashTrace = Animator.StringToHash("IsTrace");
    private readonly int hashAttack = Animator.StringToHash("IsAttack");
    private readonly int hashDie = Animator.StringToHash("IsDie");


    private void Awake()
    {
        monsterTr = GetComponent<Transform>();
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
        playerTr = GameObject.FindWithTag("Player").GetComponent<Transform>();
        stateMachine = gameObject.AddComponent<StateMachine>();
        stateMachine.AddState(State.IDLE, new IdleState(this));
        stateMachine.AddState(State.TRACE, new TraceState(this));
        stateMachine.AddState(State.ATTACK, new AttackState(this));
        stateMachine.AddState(State.DIE, new DieState(this));
        stateMachine.AddState(State.PATROL, new PatrolState(this));
        stateMachine.InitState(State.IDLE);


    }

    private void Start()
    {
        wayPointIndex = 0;
        agent.SetDestination(wayPoints[wayPointIndex].position);
        StartCoroutine(CheckMonsterState());
    }

    private IEnumerator CheckMonsterState()
    {
        while (!isDie)
        {
            yield return new WaitForSeconds(0.5f);

            if (state == State.DIE)
            {
                stateMachine.ChangeState(State.DIE);
                yield break;
            }

            float distance = Vector3.Distance(playerTr.position, monsterTr.position);

            if (distance <= attackDistance)
            {
                monsterTr.LookAt(playerTr.position);
                stateMachine.ChangeState(State.ATTACK);

            }
            else if (distance <= traceDistance)
            {
                stateMachine.ChangeState(State.TRACE);

            }
            else if (distance > traceDistance)
            {
                stateMachine.ChangeState(State.PATROL);
                if (Vector3.Distance(wayPoints[wayPointIndex].position, monsterTr.position) < attackDistance)
                {
                    wayPointIndex++;
                }
                if (wayPointIndex >= wayPoints.Length)
                    wayPointIndex = 0;
            }
            else
            {
                stateMachine.ChangeState(State.IDLE);
            }
        }
    }




    private class BaseMonsterState : BaseState
    {
        protected MonsterController owner;
        public BaseMonsterState(MonsterController owner)
        {
            this.owner = owner;
        }
    }
    private class IdleState : BaseMonsterState
    {
        public IdleState(MonsterController owner) : base(owner) { }

        public override void Enter()
        {
            owner.agent.isStopped = true;
            owner.anim.SetBool(owner.hashTrace, false);
        }
    }
    private class TraceState : BaseMonsterState
    {
        public TraceState(MonsterController owner) : base(owner) { }
        public override void Enter()
        {

            owner.agent.SetDestination(owner.playerTr.position);
            owner.agent.isStopped = false;
            owner.anim.SetBool(owner.hashTrace, true);
            owner.anim.SetBool(owner.hashAttack, false);

        }
    }
    private class AttackState : BaseMonsterState
    {
        public AttackState(MonsterController owner) : base(owner) { }
        public override void Enter()
        {
            owner.anim.SetBool(owner.hashAttack, true);

        }
    }

    private class DieState : BaseMonsterState
    {
        public DieState(MonsterController owner) : base(owner) { }
        public override void Enter()
        {

            owner.anim.SetTrigger(owner.hashDie);

        }
    }

    private class PatrolState : BaseMonsterState
    {
        public PatrolState(MonsterController owner) : base(owner) { }
        public override void Enter()
        {
            owner.agent.SetDestination(owner.wayPoints[owner.wayPointIndex].position);
            owner.agent.isStopped = false;
            owner.anim.SetBool(owner.hashTrace, true);

        }
    }
}
