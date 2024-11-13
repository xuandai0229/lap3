using System.Collections;
using UnityEngine;

public class CircleController : MonoBehaviour
{
    public float moveSpeed = 3f;        
    public float spawnDelayMin = 1f;    
    public float spawnDelayMax = 5f;     
    public GameObject circlePrefab;      
    public Transform squareObject;      
    public float detectDistance = 5f;  
    public float verticalMoveRange = 2f; 

    private bool movingUp = true;       

    void Start()
    {
     
        if (squareObject == null)
        {
            Debug.LogError("Square object is not assigned!");
            return;
        }

        
        StartCoroutine(SpawnCircleRoutine());
    }

    void Update()
    {
       
        if (squareObject != null)
        {
            
            MoveCirclesTowardsSquare();
         
            MoveVertical();
        }
    }

    
    void MoveCirclesTowardsSquare()
    {
       
        GameObject[] circles = GameObject.FindGameObjectsWithTag("Circle");

        foreach (GameObject circle in circles)
        {
            
            if (circle != null && squareObject != null)
            {
                
                float distanceToSquare = Vector2.Distance(circle.transform.position, squareObject.position);

              
                if (distanceToSquare < detectDistance)
                {
                    
                    Vector2 direction = squareObject.position - circle.transform.position;
                    direction.Normalize(); 

                  
                    circle.transform.Translate(direction * moveSpeed * Time.deltaTime);
                }
            }
        }
    }

    // Hàm di chuyển lên và xuống
    void MoveVertical()
    {
        // Kiểm tra null để đảm bảo squareObject tồn tại
        if (squareObject == null) return;

        // Di chuyển hình tròn lên xuống nếu không gần hình vuông
        if (Vector2.Distance(transform.position, squareObject.position) >= detectDistance)
        {
            if (movingUp)
            {
                transform.Translate(Vector2.up * moveSpeed * Time.deltaTime);
                if (transform.position.y >= verticalMoveRange)
                {
                    movingUp = false;
                }
            }
            else
            {
                transform.Translate(Vector2.down * moveSpeed * Time.deltaTime);
                if (transform.position.y <= -verticalMoveRange)
                {
                    movingUp = true;
                }
            }
        }
    }

    // Coroutine để sinh hình tròn sau khoảng thời gian ngẫu nhiên
    IEnumerator SpawnCircleRoutine()
    {
        while (true)  // Lặp để liên tục sinh ra hình tròn
        {
            // Đợi thời gian ngẫu nhiên
            yield return new WaitForSeconds(Random.Range(spawnDelayMin, spawnDelayMax));

            // Sinh hình tròn tại vị trí ngẫu nhiên trong phạm vi nhất định
            Vector2 spawnPosition = new Vector2(
                Random.Range(-5f, 5f),  // Vị trí ngẫu nhiên theo trục x
                Random.Range(-5f, 5f)   // Vị trí ngẫu nhiên theo trục y
            );

            // Tạo hình tròn mới từ prefab
            if (circlePrefab != null)
            {
                GameObject newCircle = Instantiate(circlePrefab, spawnPosition, Quaternion.identity);
                newCircle.SetActive(true); // Kích hoạt hình tròn mới
                newCircle.tag = "Circle";  // Gán tag cho hình tròn
            }
        }
    }

    // Hàm xử lý khi hình tròn va chạm với hình vuông (player)
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            // Kiểm tra null để tránh lỗi khi hủy đối tượng
            if (collision.gameObject != null)
            {
                // Khi hình tròn va chạm với hình vuông, bạn có thể xử lý các sự kiện khác
                Destroy(collision.gameObject);  // Ví dụ, xóa đối tượng hình vuông
            }
        }
    }
}
