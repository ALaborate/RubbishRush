using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GarbageCollector : MonoBehaviour
{

    public int thresholdNumber;

    public static int counter;
    public int counterDuble;

    public static List<GameObject> firstThresholdPass;
    public static List<GameObject> secondThresholdPass;

    public static List<GameObject> garbageСaught;

    public static int maxGarbage = 12;

    private void Start()
    {
        firstThresholdPass = new List<GameObject>();
        secondThresholdPass = new List<GameObject>();
        counter = 0;
    }
    void OnTriggerEnter2D(Collider2D col)
    {

        if (counter >= maxGarbage) return;

        if (thresholdNumber == 1)
        {
            

            if (col.gameObject.tag == "Garbage")
            {
                if (secondThresholdPass.Contains(col.gameObject))
                {
                    secondThresholdPass.Remove(col.gameObject);
                }
                else
                {
                    firstThresholdPass.Add(col.gameObject);
                }

            }
        }

        if (thresholdNumber == 2)
        {
            if (col.gameObject.tag == "Garbage")
            {
                if (firstThresholdPass.Contains(col.gameObject))
                {
                    firstThresholdPass.Remove(col.gameObject);

                    //col.gameObject.transform.position = this.gameObject.transform.position;
                    Catch(col.gameObject);
                    counter++;
                }
                else
                {
                    secondThresholdPass.Add(col.gameObject);
                }
            }

        }
    }




    private void Update()
    {
        counterDuble = counter;
    }

    private void Catch(GameObject garbage)
    {
        garbage.transform.Find("Poisson").gameObject.AddComponent<PoissonDeleate>();
        garbage.transform.Find("Poisson").gameObject.GetComponent<ParticleSystem>().Stop();
        garbage.gameObject.GetComponent<Rigidbody2D>().mass = 0;
        garbage.transform.Find("Poisson").gameObject.transform.parent = null;
        garbage.transform.SetParent(transform.parent);


        try
        {
            Destroy(garbage.GetComponent<Floater>());
            Destroy(garbage.GetComponent<Garbage>());
            garbage.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
        }
        catch (MissingReferenceException e)
        {
            Debug.Log(e.Data);
        }

        garbage.layer = 8;
        Debug.Log("Поймал2");
    }
}
    