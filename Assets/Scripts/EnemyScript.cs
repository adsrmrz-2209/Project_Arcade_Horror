using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyScript : MonoBehaviour
{
    EnemyHealth enemyHealth;

    [HideInInspector] public Animator anim;

    NavMeshAgent agent;
    [SerializeField] private Transform target;

    public ZombieState zombieState;

    public float recoveryTime = 0.1f;
    public float defRecoveryTime;

    public float dist;

    void Start()
    {
        enemyHealth = GetComponent<EnemyHealth>();
        anim = GetComponentInChildren<Animator>();
        agent = GetComponent<NavMeshAgent>();
        defRecoveryTime = recoveryTime;
    }



    void Update()
    {
       
        dist = Vector3.Distance(target.position, transform.position);

        anim.SetBool("isRunning", agent.velocity.magnitude > 0);
        if (enemyHealth.currentHealth > 0) zombieState = dist > 2.8f ? ZombieState.Moving : ZombieState.Idle;

        switch (zombieState)
        {
            case ZombieState.Idle:
                agent.destination = transform.position;
                break;

            case ZombieState.Moving:
                
                agent.destination = target.position;
                break;

            case ZombieState.Attacking:

                break;

            case ZombieState.Ragdoll:
                anim.enabled = false;
                enemyHealth.RagdollKinematics(false);
                recoveryTime -= Time.deltaTime;
                if (recoveryTime <= 0.00f && enemyHealth.currentHealth > 0)
                {
                    enemyHealth.RagdollKinematics(true);
                    anim.enabled = true;
                    recoveryTime = defRecoveryTime;
                    zombieState = ZombieState.Idle;
                }

                break;

            case ZombieState.Dead:

                //recoveryTime = 0.00f;
                anim.enabled = false;
                enemyHealth.RagdollKinematics(false); //enable the ragdoll by unchecking the rigidbody iskinematics
                Destroy(gameObject, 3f);
                break;
        }
    }

}

public enum ZombieState
{
    Idle,
    Moving,
    Attacking,
    Ragdoll,
    Dead
}
