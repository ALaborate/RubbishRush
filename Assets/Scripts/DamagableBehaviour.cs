using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;             
using System.Collections;

[System.Serializable]
public class Damage
{
    public float ammount;
}
public interface HealthCare 
{
    void ReceiveDamage(Damage dmg);
    float health0To1();
    void Kill();
}
public class DamagableBehaviour : MonoBehaviour, HealthCare
{
    [Header("Health")]
    public float maxHealth = 100f;
    public GameObject explosionPrefab;
    public float explosionDestructionDelay;

    public UnityEvent onDamageTaken;
    public UnityEvent onDeath;
    
    float health;
    GameObject explosion = null;
    protected virtual void Awake()
    {
        Resurrect();
        if (explosionPrefab != null)
        {
            explosion = Instantiate(explosionPrefab);
            explosion.transform.SetParent(transform);
            explosion.SetActive(false);
        }
    }


    public void Resurrect()
    {
        health = maxHealth;
    }
    public void Die()
    {
        //onDeath?.Invoke();
        if (onDeath != null)
            onDeath.Invoke();

        if (explosion != null)
            explosion.transform.SetParent(null);

        Destroy(gameObject);

        if (explosion != null)
        {
            explosion.transform.SetPositionAndRotation(gameObject.transform.position, gameObject.transform.rotation);
            explosion.SetActive(true);
            Destroy(explosion, explosionDestructionDelay);
        }
    }

    float HealthCare.health0To1()
    {
        return health / maxHealth;
    }
    void HealthCare.Kill()
    {
        Die();
    }
    public void ReceiveDamage(Damage dmg)
    {
        //Debug.Log("received " + dmg);
        if (health - dmg.ammount > 0)
        {
            health -= dmg.ammount;
            //onDamageTaken?.Invoke();
            if (onDamageTaken != null)
                onDamageTaken.Invoke();
        }
        else
            Die();
    }
    //protected virtual void OnCollisionEnter(Collision collision)
    //{
    //    ;
    //}
}
