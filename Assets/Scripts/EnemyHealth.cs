using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public float maxHealth = 100;
    public float currentHealth;
    public Rigidbody[] ragdollRb;




    EnemyScript enemyScript;
    

    void Start()
    {
        //playerScript = FindObjectOfType<PlayerScript>();
        enemyScript = GetComponent<EnemyScript>();
        ragdollRb = GetComponentsInChildren<Rigidbody>();

        currentHealth = maxHealth;
        
        RagdollKinematics(true); //disable the ragdoll by checking the rigidbody iskinematics
    }

    public void RagdollKinematics(bool enable)
    {
        foreach (Rigidbody rb in ragdollRb)
        {
            rb.isKinematic = enable;
        }
    }

    public void TakeDamage(float damageVal)
    {
        currentHealth -= damageVal;
        if (currentHealth <= 0)
        {
            enemyScript.zombieState = ZombieState.Dead;
        }

    }
}


