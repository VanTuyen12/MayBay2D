using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Projectile : MonoBehaviour
{
    public float speed;
    public float timeDestroy;
    private GameController m_gc;
    private Rigidbody2D m_rb;

    private AudioSource aus;
    public AudioClip hitSound;

    public GameObject hitVFX;
    // Start is called before the first frame update
    void Start()
    {
        m_gc = FindObjectOfType<GameController>();
        m_rb = GetComponent<Rigidbody2D>();
        aus = FindObjectOfType<AudioSource>();
        
        Destroy(gameObject, timeDestroy);
        
    }

    // Update is called once per frame
    void Update()
    {
        if (m_rb)
        {
            m_rb.velocity = Vector2.up * speed;
        } ; // Vận tốc, Vector2.up là vector(0,1)
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            HandleEnemyHit(other.gameObject);
        }
        else if (other.gameObject.CompareTag("SceneTopLimit"))
        {
            Destroy(gameObject);
        }
    }

    private void HandleEnemyHit(GameObject enemy)
    {
        // Lấy component EnemyController
        EnemyController enemyCtrl = enemy.GetComponent<EnemyController>();
        int scoreValue = enemyCtrl != null ? enemyCtrl.scoreValue : 1; 
        string enemyId = enemyCtrl != null ? enemyCtrl.enemyId : enemy.name;
        
        // Tăng điểm
        if (m_gc != null)
        {
            m_gc.SetScore(scoreValue);
            m_gc.ScoreIncrease();
        }
        
        // Phát âm thanh
        if (aus && hitSound)
        {
            aus.PlayOneShot(hitSound);
        }
        
        // Tạo hiệu ứng
        if (hitVFX)
        {
            var effect = Instantiate(hitVFX, enemy.transform.position, Quaternion.identity);
            Destroy(effect, 2f);
        }
        
        // Debug
        Debug.Log($"Tiêu diệt {enemyId}, nhận được {scoreValue} điểm");
        
        // Hủy đạn và enemy
        Destroy(gameObject);
        Destroy(enemy);
    }
}
