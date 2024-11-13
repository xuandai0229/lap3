using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;  
    private Rigidbody2D rb;      
    public Transform startPosition;

    void Start()
    {
        
        rb = GetComponent<Rigidbody2D>();
        if (rb == null)
        {
            Debug.LogError("Rigidbody2D is missing from the Player GameObject!");
        }

       
        if (startPosition == null)
        {
            startPosition = new GameObject("Start Position").transform;
            startPosition.position = transform.position; 
        }
    }

    void Update()
    {
      
        float moveX = Input.GetAxis("Horizontal"); 
        float moveY = Input.GetAxis("Vertical");   

      
        Vector2 moveDirection = new Vector2(moveX, moveY).normalized;
        rb.velocity = moveDirection * moveSpeed;
    }

   
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Circle"))
        {
            
            Debug.Log("Player collided with circle! Player is destroyed.");
            
            HandlePlayerCollision();
        }
    }

    
    private void HandlePlayerCollision()
    {
        
        StartCoroutine(RespawnPlayer());
    }

   
    private IEnumerator RespawnPlayer()
    {
        gameObject.SetActive(false);

       
        yield return new WaitForSeconds(1f);

        
        transform.position = startPosition.position;
        gameObject.SetActive(true);
    }
}
