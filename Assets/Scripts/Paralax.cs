using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Paralax : MonoBehaviour
{
    public GameObject[] tiles;
    public Sprite[] backgrounds;
    public float speedCoef = 0.5f;
    public int orderInLayerOverride = 0;
    public bool verticalMovement = false;
    
    [Header("Random movement")]
    public bool randomMovement = false;
    public float paralaxMovementSpeedMin = 1f;
    public float paralaxMovementSpeedMax = 5f;
    public float acceleration = 1f;
    public float randomizeInterval;
    public bool randomizeDirection = false;
    public float randomIntervalCoef = 0.5f;
    

    private float nextTimeToRandomize = 0f;
    private float desiredSpeed, currentSpeed;







    new GameObject camera;
    int tileIndex;
    float fullTileXLength;
    // Start is called before the first frame update
    void Start()
    {
        camera = Camera.main.gameObject;
        if (backgrounds.Length > 0)
        {
            for (int i = 0, j = 0; i < tiles.Length; i++, j++)
            {
                if (j == backgrounds.Length)
                {
                    j = 0;
                }
                var sr = tiles[i].GetComponent<SpriteRenderer>();
                if (sr != null)
                {
                    sr.sprite = backgrounds[j];
                    sr.sortingOrder = orderInLayerOverride;
                }
            }
        }

        if (tiles.Length > 0)
        {
            var sr = tiles[0].GetComponent<SpriteRenderer>();
            fullTileXLength = (sr.bounds.max.x - sr.bounds.min.x);
            tiles[0].transform.position = camera.transform.position - Vector3.right * fullTileXLength + Vector3.forward*5f;
            tiles[0].transform.position = new Vector3(camera.transform.position.x - fullTileXLength, 0f, 0f);

            for (int i = 1; i < tiles.Length; i++)
            { 
                tiles[i].transform.position = tiles[0].transform.position + Vector3.right * fullTileXLength * i;
            }

            tileIndex = 1;
        }

    }

    private void Update()
    {
        if(randomMovement)
        {
            if(Time.time>=nextTimeToRandomize)
            {
                if (randomizeDirection)
                    desiredSpeed = Random.Range(-paralaxMovementSpeedMax + paralaxMovementSpeedMin, paralaxMovementSpeedMax - paralaxMovementSpeedMin);
                else desiredSpeed = Random.Range(paralaxMovementSpeedMin, paralaxMovementSpeedMax);
                nextTimeToRandomize = Time.time + randomizeInterval+Random.Range(-randomIntervalCoef, randomIntervalCoef);
            }
        }

        if (!CameraIsInScope())
        {
            //shift side tile
            int delta = translationXPositive ? 1 : -1;
            int tileToMoveInx = (tileIndex + delta) % tiles.Length;
            tileIndex = tileToMoveInx < 0 ? tiles.Length - 1 : tileToMoveInx;
            tileToMoveInx = (tileIndex + delta);
            if (tileToMoveInx < 0)
                tileToMoveInx = tiles.Length - 1;

            if (tileToMoveInx < 0)
                tileToMoveInx = tiles.Length - 1;
            else if (tileToMoveInx >= tiles.Length)
                tileToMoveInx = 0;
            tiles[tileToMoveInx].transform.Translate(Vector3.right * fullTileXLength * tiles.Length * delta);
        }
    }

    private Vector3 previousCamPosition;
    private bool translationXPositive;
    void LateUpdate()
    {
        var translation = (camera.transform.position - previousCamPosition) - Vector3.right * currentSpeed * Time.deltaTime;
        translationXPositive = translation.x > 0f;
        if (!verticalMovement)
            translation.y = 0f;

        if(randomMovement&&currentSpeed<0)//TODO: performance loss
        {
            var sprites = from f in tiles select f.GetComponent<SpriteRenderer>();
            foreach ( var sr in sprites )
            {
                sr.flipX = true;
            }
        }
        else
        {
            var sprites = from f in tiles select f.GetComponent<SpriteRenderer>();
            foreach (var sr in sprites)
            {
                sr.flipX = false;
            }
        }

        gameObject.transform.position = transform.position + translation * speedCoef + Vector3.right*currentSpeed*Time.deltaTime;
        var deltaSpeed = desiredSpeed - currentSpeed;
        if (currentSpeed != desiredSpeed)
        {
            if (Mathf.Abs(deltaSpeed) < acceleration * Time.deltaTime)
            {
                currentSpeed = desiredSpeed;
            }
            else
            {
                currentSpeed += Mathf.Sign(deltaSpeed) * acceleration * Time.deltaTime;
            }
        }
        previousCamPosition = camera.transform.position;
    }

    private bool CameraIsInScope()
    {
        //throw new System.NotImplementedException();
        var cTilePos = tiles[tileIndex].transform.position;
        var camPos = camera.transform.position;
        var halfLength = fullTileXLength / 2;
        return camPos.x < cTilePos.x + halfLength && camPos.x > cTilePos.x - halfLength;
    }
}
