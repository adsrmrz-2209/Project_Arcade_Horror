using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyMiniHealth : MonoBehaviour
{
    public float maxMiniHealth;
    public float currentMiniHealth;
    public GameObject bodyPart;

    EnemyHealth enemyHealth;
    EnemyScript enemyScript;
    WeaponStats weaponStats;

    Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
       
        enemyHealth = GetComponentInParent<EnemyHealth>();
        enemyScript = GetComponentInParent<EnemyScript>();
        weaponStats = FindObjectOfType<WeaponStats>();
        rb = GetComponent<Rigidbody>();

        currentMiniHealth = maxMiniHealth;
        if (bodyPart == null) return;
    }


    public void TakeDamage(float damageVal, RaycastHit hit)
    {
        rb.AddForce(-hit.normal * 50f, ForceMode.Impulse);
        currentMiniHealth -= damageVal;

        if (currentMiniHealth <= 0)
        {
            enemyScript.zombieState = ZombieState.Ragdoll;

            //if this gameobject tag is "head", then deplete whole health instantly. else, take a normal damage
            enemyHealth.TakeDamage(gameObject.tag == "head" ? enemyHealth.currentHealth : weaponStats.gunDamage);

            Destroy(bodyPart);

            rb.constraints = RigidbodyConstraints.FreezeAll;

            //destroy all components except tranform and rigidbody
            foreach (Component cmpnt in gameObject.GetComponents<Component>())
            {
                if (!(cmpnt is Transform) && !(cmpnt is Rigidbody))
                {
                    Destroy(cmpnt);
                }
            }
        }
    }
}
