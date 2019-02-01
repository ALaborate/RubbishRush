using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoissonDeleate : MonoBehaviour
{
    private float delay = 1;

    void Update()
    {
        if(delay <=0)
        {
            Destroy(this.gameObject);

        }
        else
        {
            delay -= Time.deltaTime;
        }
    }
}
