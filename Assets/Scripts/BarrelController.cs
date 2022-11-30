using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarrelController : MonoBehaviour
{
    //Variable Declaration 
    private Rigidbody2D barrelRb;
    private float speed = 3;
    // Start is called before the first frame update
    void Start()
    {
        barrelRb = GetComponent<Rigidbody2D>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Ground")) 
        {
            barrelRb.AddForce(collision.transform.right * speed , ForceMode2D.Impulse);
        }
        else if (collision.collider.CompareTag("Player"))
        {
            Destroy(gameObject);
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
