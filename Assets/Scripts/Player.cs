using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float moveSpeed;
    public GameObject projectile;

    public Transform shootingPoint;

    private GameController m_gc;

    public AudioSource aus;
    public AudioClip shootingSound;
    // Start is called before the first frame update
    void Start()
    {
         m_gc = FindObjectOfType<GameController>();
    }

    // Update is called once per frame
    void Update()
    {
        float xDirection = Input.GetAxisRaw("Horizontal");
        if (m_gc.GetIsGameover())
            return;
        
        if ((transform.position.x < -8 && xDirection < 0 )|| (xDirection > 0 && transform.position.x > 8))
            return;
        transform.position += Vector3.right * (xDirection * moveSpeed * Time.deltaTime);

        if (Input.GetMouseButtonDown(0))
        {
            Shoot();
        }
    }

    public void Shoot()
    {
        if (projectile && shootingPoint)
        {
            if (aus && shootingSound)
            {
                aus.PlayOneShot(shootingSound);
            }
            Instantiate(projectile,shootingPoint.position,Quaternion.identity);// sinh ra gameObject ở vị trí của shootingPoint và ko xoay
        }
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            m_gc.SetIsGameover(true);
            Destroy(other.gameObject);
            Debug.Log("va cham");
        }
    }
}
