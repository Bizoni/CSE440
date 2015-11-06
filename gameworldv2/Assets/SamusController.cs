using UnityEngine;
using System.Collections;

public class SamusController : MonoBehaviour {

    public float moveSpeed = 3f;
    private bool facingRight = true;
    Rigidbody2D rb;
    private bool grounded = true;
    // Use this for initialization
    void Start () {
        rb = GetComponent<Rigidbody2D>();

    }
	
	// Update is called once per frame
	void FixedUpdate () {
        rb.velocity = new Vector2(0, rb.velocity.y);
        Vector3 theScale = transform.localScale;
        if (Input.GetKey(KeyCode.RightArrow))
        { 
            if (facingRight == false)
            {
                theScale.x *= -1;
                facingRight = true;
            }
            rb.velocity = new Vector2(moveSpeed, rb.velocity.y);
        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            if (facingRight == true)
            {
                theScale.x *= -1;
                facingRight = false;
            }
            rb.velocity = new Vector2(moveSpeed * -1 , rb.velocity.y);

        }
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            if (grounded == true)
            {
                rb.AddForce(new Vector2(0, 6), ForceMode2D.Impulse);
                grounded = false;
                Debug.Log("not grounded");
            }
        }
        //if (Input.GetKey(KeyCode.DownArrow))
        //  transform.Translate(new Vector3(0, -1, 0) * moveSpeed * Time.deltaTime);
        
        transform.localScale = theScale;
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if( other.CompareTag("cube"))
        {
            GetComponent<SpriteRenderer>().color = Color.blue;
        }
        Debug.Log("grounded");
        grounded = true;
    }
}
