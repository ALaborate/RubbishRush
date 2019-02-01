    using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Floater : MonoBehaviour
{
    public float degreesPerSecond = 15.0f;
    private bool turnClockWise;
    public float amplitude = 0.5f;
    public float frequency = 1f;
    public bool canFloat;
    public float minTimeToStop;
    public float maxTimeToStop;
    [SerializeField] private bool temp;
   [SerializeField]  Vector3 posOffset = new Vector3();
    Vector3 tempPos = new Vector3();

    // Update is called once per frame
    private void Start()
    {
        float rand = Random.Range(minTimeToStop, maxTimeToStop);
        posOffset = transform.position;
        turnClockWise = (Random.Range(0, 2) < 1) ? true : false;
        if (canFloat)
            StartCoroutine(WaitBeforeSink(rand));
    }
    void Update()
    {
      
        if (turnClockWise)
        transform.Rotate(new Vector3(0f, 0, Time.deltaTime * degreesPerSecond), Space.World);
        else
            transform.Rotate(new Vector3(0f, 0, -Time.deltaTime * degreesPerSecond), Space.World);
       if(canFloat&&temp)
        {
            
            Float();
        }

    }

    private void Float()
    {
        tempPos = transform.localPosition;
        
        GetComponent<Rigidbody2D>().velocity = Vector3.zero;
        GetComponent<Rigidbody2D>().isKinematic = true;
        tempPos.y += Mathf.Sin(Time.fixedTime * Mathf.PI * frequency) * amplitude;

        transform.localPosition = tempPos;
    }

    IEnumerator WaitBeforeSink(float time)
    {
                    yield return new WaitForSeconds(2);
        temp = true;
        yield break;
    }
   
}
