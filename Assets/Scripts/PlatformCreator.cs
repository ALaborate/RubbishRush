using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Move))]
public class PlatformCreator : MonoBehaviour
{

    //I suggest there is a bug that occurs when character is passing platform junction in the air. THe effect is that ground literaly moves away on X axis.

    //should stand from left to right
    public GameObject[] tiles;
    public Sprite[] backgrounds;
    private float tile_XLength = 1f;
    //private float platform_FullLength;




    int tileIndex;

    // Use this for initialization
    void Start()
    {
        if (backgrounds.Length > 0)
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
                }
            }

        if (tiles.Length > 0)
        {
            var sr = tiles[0].GetComponent<SpriteRenderer>();
            tile_XLength = (sr.bounds.max.x - sr.bounds.min.x) / tiles[0].transform.localScale.x;

            for (int i = 1; i < tiles.Length; i++)
            {
                tiles[i].transform.position = tiles[0].transform.position + Vector3.right * tile_XLength * tiles[i].transform.localScale.x * i;
            }

            tileIndex = 1;
        }
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        if (!IsInCurrentScope())
        //{
        //    //change scope to 
        //    int inx = (platformIndex + (ctrler.IsWatchingRight()? 1 : -1)) % (platform_Pool.Length);
        //    platformIndex = inx < 0 ? platform_Pool.Length-1 : inx;
        //    //take next-2 to next position (platformIndex + platform_Pool.Length - 1) % platform_Pool.Length
        //    inx = (platformIndex + (ctrler.IsWatchingRight() ? 1 : -1)) % (platform_Pool.Length);
        //    platform_Pool[inx<0?platform_Pool.Length-1:inx].transform.Translate(Vector3.right*platform_FullLength*(platform_Pool.Length)*(ctrler.IsWatchingRight()?1:-1));
        //}
        {
            int delta = (IsWatchingPositiveX() ? 1 : -1);
            int inx = (tileIndex + delta)%tiles.Length;
            tileIndex = inx < 0 ? tiles.Length - 1 : inx;//current tile
            inx = (tileIndex + delta); //tile to move

            if (inx < 0)
                inx = tiles.Length - 1;
            else if (inx >= tiles.Length)
                inx = 0;
            tiles[inx].transform.Translate(Vector3.right * tile_XLength * tiles.Length * delta * tiles[inx].transform.localScale.x);
            
        }
    }
    bool IsInCurrentScope()
    {
        Transform cTileTransf = tiles[tileIndex].transform;
        float halfLength = cTileTransf.localScale.x / 2 * tile_XLength;
        if (gameObject.transform.position.x < cTileTransf.position.x + halfLength && gameObject.transform.position.x > cTileTransf.position.x - halfLength)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    bool IsWatchingPositiveX()
    {
        //return transform.TransformDirection(Vector3.right).x > 0f;
        return transform.position.x - tiles[tileIndex].transform.position.x > 0f;
    }
}

