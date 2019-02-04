using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{

    public GameObject fish;
    public Transform cam;
    public float noFishZoneCoeff = 0.25f; //Частина фону, де буде діяти виштовхувальна сила

    private float topBoundary;
    private float bottomBoundary;

    private Vector3 offset;
    private Vector2 steerForce;
    private Rigidbody2D frb;

    private void Start()
    {
        SpriteRenderer tileSr = null;
        var pc = gameObject.GetComponentInChildren<PlatformCreator>();
        if (pc != null && pc.tiles.Length > 0)
        {
            tileSr = pc.tiles[0].GetComponent<SpriteRenderer>();
        }

        if (tileSr != null)
        {
            var backgroundHeight = tileSr.bounds.max.y - tileSr.bounds.min.y;
            var noFishZoneHeight = backgroundHeight * noFishZoneCoeff;
            topBoundary = (backgroundHeight - noFishZoneHeight) * 0.5f;
            bottomBoundary = -topBoundary;

        }


        offset = cam.transform.position - fish.transform.position;
        frb = fish.GetComponent<Rigidbody2D>();


    }
    private void Update()
    {
        if (GameController.instance.IsGameOver)
            return;

        cam.transform.position = fish.transform.position + offset;


        if (fish.transform.position.y >= topBoundary)
        {
            steerForce = Vector2.down;
        }
        else if (fish.transform.position.y <= bottomBoundary)
        {
            steerForce = Vector2.up;
        }
        else steerForce = Vector2.zero;
        frb.AddForce(steerForce * fish.GetComponent<Move>().speed * Time.deltaTime, ForceMode2D.Impulse);
    }
}
