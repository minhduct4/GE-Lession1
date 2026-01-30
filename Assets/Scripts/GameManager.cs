using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject roadPrefab; // Prefab của đoạn đường
    public List<GameObject> obstaclePrefab; // Prefab của chướng ngại vật
    public int numOfRoads = 10; // Số đoạn đường ban đầu
    Vector3 firstPosition = new Vector3(0, 0, 0); // Vị trí bắt đầu tạo đường
    Vector3 nextPosition; // Vị trí tiếp theo để tạo đoạn đường
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        nextPosition = firstPosition;
        for (int i = 0; i < numOfRoads; i++)
        {
            CreateBasicRoad(i >= 2); // tạo 5 đoạn đường đầu tiên không có chướng ngại vật
        }
    }
    public void CreateBasicRoad(bool hasObstacle)
    {
        // tạo ra con đường ban đầu với khoảng 10 đoạn đường
        GameObject basicRoad = Instantiate(roadPrefab, nextPosition, Quaternion.identity);
        // lấy vị trí đặt road tiếp theo ở trong nextPosition của roadPrefab kế trước
        nextPosition = basicRoad.transform.Find("NextRoadPosition").transform.position;
        if(!hasObstacle) return;
        // tạo một obstacle lên road vừa tạo tại 1 trong 3 điểm là left, right, center
        int randomPos = Random.Range(0, 3); // 0-left, 1-center, 2-right
        Vector3 obstaclePosition = basicRoad.transform.Find(randomPos == 0 ? "Left" : randomPos == 1 ? "Right" : "Center").transform.position;
        if (obstaclePrefab != null)
        {
            GameObject obstacle = Instantiate(obstaclePrefab[Random.Range(0, obstaclePrefab.Count)], obstaclePosition, Quaternion.identity);
            obstacle.transform.parent = basicRoad.transform; // đặt obstacle làm con của road để dễ quản lý
        } 
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
