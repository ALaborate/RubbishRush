using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Garbage : MonoBehaviour
{
    public float minPoisonRadius = 0.2f;
    public float maxPoisonRadius = 6f;
    public float poisonExtentionPerSecond = 0.1f;
    public float poisonDamagePerSecond = 10f;
    public ParticleSystem decoration;
    public float decorationRadiusCoef = 1.2f;

    private CircleCollider2D poisonTrigger;
    private ParticleSystem.MainModule main;

    private static int _garbageQuantity;//not used currently
    private float notUsedVarriable;    

    private void Awake()
    {
        main = decoration.main;
        _garbageQuantity++;
    }

    // Start is called before the first frame update
    void Start()
    {
        poisonTrigger = decoration.gameObject.AddComponent<CircleCollider2D>();
        poisonTrigger.radius = minPoisonRadius;
        poisonTrigger.isTrigger = true;
        decoration.Play();
        
    }
   
    // Update is called once per frame
    void Update()
    {
        if (poisonTrigger != null)
        {
            //poisonTrigger.radius = Mathf.Min(minPoisonRadius + (poisonExtentionPerSecond) * (Time.time - awakeTime), maxPoisonRadius);
            poisonTrigger.radius = Mathf.Clamp(poisonTrigger.radius + poisonExtentionPerSecond * Time.deltaTime, minPoisonRadius, maxPoisonRadius);
            //main.startLifetime = new ParticleSystem.MinMaxCurve(poisonTrigger.radius / main.startSpeed.Evaluate(Time.time));
            if (decoration != null)
                main.startLifetime = poisonTrigger.radius * decorationRadiusCoef / main.startSpeed.Evaluate(0.5f);
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        var hc = collision.gameObject.GetComponent<HealthCare>();
        if (hc != null)
        {
            hc.ReceiveDamage(new Damage() { ammount = poisonDamagePerSecond * Time.deltaTime });
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject == null)
            return;

        if (collision.transform.tag == "Wall")
            GentleDestroy();
        else
        {
            var hc = collision.gameObject.GetComponent<HealthCare>();
            var g = collision.gameObject.GetComponent<Garbage>();
            if (hc!=null)
            {
                hc.Kill();
                GentleDestroy();
            }
            else if(g!=null)
            {
                if (collision.gameObject.transform.position.y > transform.position.y)
                {
                    g.poisonDamagePerSecond += poisonDamagePerSecond * 0.5f;
                    GentleDestroy();
                }
            }
            
        }
    }
    public void GentleDestroy(float delay = 0f)
    {
        if (decoration != null)
        {
            decoration.transform.SetParent(null);
            decoration.Stop();
            Destroy(decoration.gameObject, 3f);
        }
        Destroy(gameObject, delay);
    }

    private void OnDestroy()
    {
        _garbageQuantity--;
        decoration.Stop();
    }
}
