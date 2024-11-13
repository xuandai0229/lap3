using UnityEngine;
using UnityEngine.UI;

public class WinTrigger : MonoBehaviour
{
    public Text winText; // Tham chiếu tới UI Text để hiển thị thông báo chiến thắng

    void Start()
    {
        // Ẩn thông báo chiến thắng khi bắt đầu trò chơi
        winText.gameObject.SetActive(false);
    }

    // Phát hiện va chạm với đối tượng có tag "WinObject"
    void OnCollisionEnter2D(Collision2D collision) // Sử dụng OnTriggerEnter2D nếu đối tượng có IsTrigger
    {
        if (collision.gameObject.CompareTag("WinObject"))
        {
            // Hiển thị thông báo chiến thắng
            winText.gameObject.SetActive(true);
            winText.text = "Bạn đã chiến thắng!";
        }
    }
}
