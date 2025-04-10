using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;



public class SpawnerWithResources : MonoBehaviour
{
    public string[] prefabNames; // 
    private GameObject[] saveprefab;
    
    private Dictionary<string,int> scoreValues = new Dictionary<string, int>(); 
    
    private GameController m_gc;
    
    private Coroutine spawnCoroutine; // Để lưu trữ coroutine đang chạy,tham chiếu (reference) đến một coroutine đang chạy
    private bool isSpawning = true; // Biến kiểm soát việc sinh đối tượng

    private void Awake()
    {
        InitScoreValues();
        LoadGameobjectPrefab();
        m_gc = FindObjectOfType<GameController>(); // Tìm GameController
    }

    void Start()
    {
        //gọi StartCoroutine(),gán kết quả vào spawnCoroutine để theo dõi hoặc điều khiển nó.
        spawnCoroutine = StartCoroutine(SpawnEnemyLoop());
    }
    

    private void InitScoreValues()
    {
        // Đặt giá trị điểm cho từng loại enemy
        scoreValues.Add("Su57", 1);
        scoreValues.Add("A10", 2);
        scoreValues.Add("Sr71", 5);
        scoreValues.Add("B2", 10);
    }
    public IEnumerator SpawnEnemyLoop()
    {
            
            while (isSpawning) //isSpawning True thị loop
            {
                // Kiểm tra trạng thái game trước khi sinh ra đối tượng
                if (m_gc != null && !m_gc.GetIsGameover())
                {
                    SpawnEnemy();
                }
                yield return new WaitForSeconds(2f); // Spawn mỗi 2 giây
            }
    }
    
    // Phương thức để dừng spawning
    public void StopSpawning()
    {
        isSpawning = false;
        if (spawnCoroutine != null) //Ktra # null tức là đối tượng vẫn chạy
        {
            StopCoroutine(spawnCoroutine);// dừng coroutine lại
            spawnCoroutine = null;// cài lại mặc định nó là null
        }
    }
    
    public void SpawnEnemy()
    {
        // Kiểm tra lại xem game đã kết thúc chưa
        if (m_gc != null && m_gc.GetIsGameover())
            return;
        
        if (saveprefab == null || saveprefab.Length == 0 || saveprefab[0] == null)
            return;
        
        float randXpos = Random.Range(-7.0f, 7.0f);//Random tọa độ
        int rdEnemy = Random.Range(0, prefabNames.Length);//random prefabs
        
        Vector2 spawnPos = new Vector2(randXpos, 6);
        
        if (saveprefab[rdEnemy])
        {
            string enemyName = prefabNames[rdEnemy];
            GameObject clEnemy =  Instantiate(saveprefab[rdEnemy], spawnPos, Quaternion.identity);
            
            // Gán điểm cho enemy
            //GameObject clEnemy luôn có một component EnemyController để sử dụng. Nếu đã có sẵn thì dùng, nếu chưa có thì tạo mới
            EnemyController enemyCtrl = clEnemy.GetComponent<EnemyController>();
            if (enemyCtrl == null)
            {
                enemyCtrl = clEnemy.AddComponent<EnemyController>();
            }
            
            enemyCtrl.enemyId = enemyName;
            
            // Lấy giá trị điểm từ Dictionary
            if (scoreValues.ContainsKey(enemyName)) //kiểm tra xem tên kẻ thù (enemyName) có tồn tại trong dictionary scoreValues
            {
                enemyCtrl.scoreValue = scoreValues[enemyName];//Nếu có, gán giá trị điểm tương ứng cho enemyCtrl.scoreValue
            }
            else
            {
                // Nếu không có trong dictionary, thử trích xuất số từ tên
                string numberStr = enemyName.Replace("Enemy", "");//Code loại bỏ chuỗi "Enemy" từ enemyName (ví dụ: "Enemy5" -> "5")
                int value;
                if (int.TryParse(numberStr, out value))//dùng int.TryParse() để chuyển đổi chuỗi còn lại thành số nguyên
                {
                    enemyCtrl.scoreValue = value;//Nếu chuyển đổi thành công, gán giá trị số đó cho enemyCtrl.scoreValue
                }
                else
                {
                    //Nếu không thể trích xuất số từ tên, code sẽ sử dụng giá trị mặc định là 1
                    enemyCtrl.scoreValue = 1; // Giá trị mặc định
                }
            }
            Destroy(clEnemy,15f);//Xóa đối tg trong 15' sau khi sinh ra
        }
    }
    
    public void LoadGameobjectPrefab()
    {
        if (prefabNames.Length == 0) {
            return;
        }
        
        saveprefab = new GameObject[prefabNames.Length];

        for (int i = 0; i < prefabNames.Length; i++)
        {
            GameObject prefab = Resources.Load<GameObject>($"Prefabs/{prefabNames[i]}");

            if (prefab != null)
            {
                    saveprefab[i] = prefab;
                    Debug.Log($" Đã load thành công: {prefabNames[i]}");
            }
            else
            {
                Debug.LogError($" Không tìm thấy Prefab: {prefabNames[i]} trong Resources/Prefabs/");
            }
        }
    }
    
    
}
