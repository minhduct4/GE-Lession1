using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    Rigidbody rb;
    float turnInput = 0f;
    GameManager gameManager;
    public float forwardSpeed = 5f;

    public GameObject boomEffectPrefab; // Prefab của hiệu ứng nổ
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        gameManager = FindAnyObjectByType<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        turnInput = Input.GetAxis("Horizontal");
    }
    private void FixedUpdate()
    {
        rb.linearVelocity = new Vector3(turnInput * 5, rb.linearVelocity.y, forwardSpeed);
        if (rb.linearVelocity.y < 0) return;
    }
    private void OnTriggerEnter(Collider other)
    {
        // kiểm tra xem đối tượng player trigger vào có phải là checkpoint không
        if (other.gameObject.CompareTag("TriggerArea"))
        {
            Debug.Log("Checkpoint reached!");
            // Thực hiện các hành động khi chạm vào checkpoint ở đây
            // tạo ra đoạn đường mới
            gameManager.CreateBasicRoad(true);
            // xóa đoạn đường đã đi qua sau một khoảng thời gian
            Destroy(other.transform.parent.gameObject, 1f);

        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Obstacle"))
        {
            Debug.Log("Hit an obstacle! Game Over.");
            // Thực hiện các hành động khi va chạm với chướng ngại vật ở đây
            // Ví dụ: Dừng trò chơi, hiển thị hiệu ứng nổ
            Instantiate(boomEffectPrefab, transform.position, Quaternion.identity);
            //Time.timeScale = 0f; // Dừng trò chơi
            Invoke(nameof(LoadSceneToPlay), 1.5f); // Gọi hàm LoadSceneToPlay sau 1 giây 
        }
    }
    private void LoadSceneToPlay()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
