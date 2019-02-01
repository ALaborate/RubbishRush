using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{

    public GameObject botWall;
    public GameObject topWall;
    public GameObject fish;
    public Transform cam;
    public float noFishZone = 0.2f;
    //public float minimumBoundary;
    //public float maximumBoundary;
   
    private Vector3 offset;
    private Vector2 steerForce;
    private Rigidbody2D frb;
    
    private void Start()
    {
        SpriteRenderer tileSr = null;
       var pc = gameObject.GetComponentInChildren<PlatformCreator>();
        if (pc != null&&pc.tiles.Length>0)
        {
            tileSr = pc.tiles[0].GetComponent<SpriteRenderer>();
        }
        
        if(tileSr!=null)
        {
            var height = tileSr.bounds.max.y - tileSr.bounds.min.y;
            var zoneHeight = height * noFishZone;

            topWall.transform.position = new Vector3(topWall.transform.position.x,
                tileSr.gameObject.transform.position.y + height * 0.5f - zoneHeight,
                topWall.transform.position.z
                );
            botWall.transform.position = new Vector3(botWall.transform.position.x,
                tileSr.gameObject.transform.position.y - height * 0.5f + zoneHeight,
                botWall.transform.position.z
                );
        }

        
        offset = cam.transform.position - fish.transform.position;
        frb = fish.GetComponent<Rigidbody2D>();


    }
    private void Update()
    {
        if (GameController.instance.IsGameOver)
            return;

        
        
        cam.transform.position = fish.transform.position + offset;
        

        //       cam.transform.localPosition = new Vector3
        //(
        //   cam.transform.localPosition.x,
        //    Mathf.Clamp(cam.transform.position.y, minimumBoundary, maximumBoundary),
        //    cam.transform.position.z
        //);
        if (fish.transform.position.y + 2 > topWall.transform.position.y)
        {
            steerForce = Vector2.down;

        }
        else if (fish.transform.position.y - 2 < botWall.transform.position.y)
        {
            steerForce = Vector2.up;
        }
        else steerForce = Vector2.zero;
        frb.AddForce(steerForce * fish.GetComponent<Move>().speed*Time.deltaTime, ForceMode2D.Impulse);
    }
}
