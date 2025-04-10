using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    
    public int scoreValue = 1;
    public string enemyId;
    // Start is called before the first frame update
    void Start()
    {
        if (string.IsNullOrEmpty(enemyId))//iều kiện kiểm tra xem biến enemyId hiện tại có rỗng hoặc null không
        {
            enemyId = gameObject.name.Replace("(Clone)", "").Trim();
            //Lấy tên hiện tại của GameObject thông qua gameObject.name
            //Khi Unity tạo một bản sao (clone) của prefab bằng Instantiate, nó tự động thêm "(Clone)"
            //.Replace("(Clone)", "") loại bỏ chuỗi "(Clone)" khỏi tên, để lấy lại tên gốc của prefab
            //.Trim() xóa khoảng trắng thừa (nếu có) ở đầu và cuối chuỗi
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
