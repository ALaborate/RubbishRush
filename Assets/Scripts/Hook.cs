using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
[RequireComponent(typeof(Rigidbody2D))]
//public class Hook : MonoBehaviour
//{
//    public int minPosition;
//    public int maxPosition;
//    public float sinkSpeed;
//    public float riseSpeed;
//    public float holdTime;

//    private Vector2 startPoint;
//    private Rigidbody2D rb;
//    private void Awake()
//    {
//        rb = GetComponent<Rigidbody2D>();

//    }
//    private void Start()
//    {
//        rb.gravityScale = sinkSpeed;
//        startPoint = transform.position;
//        transform.localPosition -= new Vector3(0, Random.Range(minPosition, maxPosition + 1), 0);
//        StartCoroutine(GoUp());
//    }
//    IEnumerator GoUp()
//    {
//        yield return new WaitForSecondsRealtime(holdTime);
//        rb.gravityScale = -riseSpeed;

//    }
//    private void OnTriggerEnter2D(Collider2D collision)
//    {
//        if (collision.CompareTag("Fish"))
//        {
//            //Kill fish
//        }
//        else if (collision.CompareTag("Net"))
//        {
//            GrabNet(collision.gameObject);
//        }

//    }

//    private void GrabNet(GameObject net)
//    {
//        //Debug.Log("You just grabed net");
//        //DistanceJoint2D temp = this.gameObject.AddComponent<DistanceJoint2D>();
//        //temp.connectedBody = net.GetComponent<Rigidbody2D>();
//        //net.GetComponent<DistanceJoint2D>().connectedBody = null;

//        //temp.anchor = new Vector2(-1.95f, 0);
//        //OnGrabNet.Invoke(GarbageCollector.counter);
//        //GarbageCollector.counter = 0;

//        //old net 
//        //DistanceJoint2D temp = this.gameObject.AddComponent<DistanceJoint2D>();
//        //temp.connectedBody = net.GetComponent<Rigidbody2D>();
//        //temp.anchor = new Vector2(-1.95f, 0);
//        //net.GetComponent<DistanceJoint2D>().connectedBody = null;


//        //new net
//        //net.transform.parent.GetComponent<NodeGenerator>().NewNet(net.transform);
//        //net.transform.parent = null;



//        GameController.instance.Score += GarbageCollector.counter;
//        GarbageCollector.counter = 0;
//    }
//}

[RequireComponent(typeof(Rigidbody2D))]
public class Hook : MonoBehaviour
{
    public float sinkHeight;
    //public int maxDeviation;
    public float sinkSpeed;
    public float riseSpeed;
    public float holdTime;
    public float randomizeTimeHold = 0.5f;
    bool netIsGrabed = false;

    public enum State
    {
        sinking, holding, rising
    }
    private State currentState = State.sinking;
    private Vector3 destination;
    private Rigidbody2D rb;
    private float holdTimeDuplicate;
    private void Awake()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        rb.isKinematic = true;
        destination = transform.position - Vector3.up * sinkHeight;
        holdTimeDuplicate = holdTime+holdTime*Random.Range(-randomizeTimeHold,randomizeTimeHold);
    }

    private void Update()
    {
        switch (currentState)
        {
            case State.sinking:
                {
                    if (transform.position.y >= destination.y)
                    {
                        //sink
                        transform.position += Vector3.down * sinkSpeed * Time.deltaTime;
                    }
                    else currentState = State.holding;
                    break;
                }
            case State.holding:
                {
                    if (holdTimeDuplicate > 0)
                        holdTimeDuplicate -= Time.deltaTime;
                    else currentState = State.rising;
                    break;
                }
            case State.rising:
                {
                    transform.position += Vector3.up * riseSpeed * Time.deltaTime;
                    if (transform.position.y > destination.y + 2 * sinkHeight)
                        Destroy(gameObject, 2f);
                    break;
                }
            default: break;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (netIsGrabed) return;
        var hc = collision.gameObject.GetComponent<HealthCare>();
        if (hc!=null/*collision.CompareTag("Fish")*/)
        {
            //Kill fish
            hc.Kill();
        }
        else if (collision.CompareTag("Net"))
        {
            GrabNet(collision.gameObject);
            netIsGrabed = true;
            currentState = State.rising;
        }
    }

    private void GrabNet(GameObject net)
    {
        net.tag = "Untagged";
        net.transform.parent.GetComponent<NodeGenerator>().NewNet(net.transform.position,net.transform.rotation);
        net.transform.parent = this.transform;
        net.GetComponent<DistanceJoint2D>().connectedBody = null;


        GameController.instance.Score += GarbageCollector.counter;
        GarbageCollector.counter = 0;
    }
}


