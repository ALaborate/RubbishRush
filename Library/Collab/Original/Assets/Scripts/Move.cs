using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class Move : MonoBehaviour {



    public float speed;
    public float rotationSpeedPerSec;


    SpriteRenderer sr;
	// Use this for initialization
	void Start () {
        sr = gameObject.GetComponent<SpriteRenderer>();
	}

    void Update()
    {
        //if (Input.GetKey(KeyCode.LeftArrow))
        //{
        //    transform.Rotate(0, 0, rotationSpeedPerSec);
        //}
        //if (Input.GetKey(KeyCode.RightArrow))
        //{
        //    transform.Rotate(0, 0,-rotationSpeedPerSec);
        //}
        //if (Input.GetKey(KeyCode.UpArrow))
        //{
        //    //проверка на скорость 
        //    //transform.position += this.gameObject.transform.right * speed * Time.deltaTime;
        //    this.gameObject.GetComponent<Rigidbody2D>().AddForce(this.gameObject.transform.right * speed * Time.deltaTime, ForceMode2D.Impulse);
        //}

        var h = Input.GetAxis("Horizontal");
        var v = Input.GetAxis("Vertical");
        if(h!=0f)
        {
            transform.Rotate(0, 0, -h*rotationSpeedPerSec * Time.deltaTime);
        }
        if(v>0)
        {
            this.gameObject.GetComponent<Rigidbody2D>().AddForce(this.gameObject.transform.right * speed * Time.deltaTime, ForceMode2D.Impulse);
        }

        if (transform.TransformDirection(Vector3.right).x > 0)
        {
            sr.flipY = false;
        }
        else sr.flipY = true;
    }
}
