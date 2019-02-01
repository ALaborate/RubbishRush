using UnityEngine;

public class Move : MonoBehaviour {



    public float speed;
    public float rotationSpeed;

	// Use this for initialization
	void Start () {
		
	}

    void Update()
    {
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            transform.Rotate(0, 0, rotationSpeed);
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            transform.Rotate(0, 0,-rotationSpeed);
        }
        if (Input.GetKey(KeyCode.UpArrow))
        {
            //проверка на скорость 
            //transform.position += this.gameObject.transform.right * speed * Time.deltaTime;
            this.gameObject.GetComponent<Rigidbody2D>().AddForce(this.gameObject.transform.right * speed * Time.deltaTime, ForceMode2D.Impulse);
        }
        
    }
}
