using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float moveSpeed;

    private Rigidbody2D m_rg;

    private GameController m_gc;
    // Start is called before the first frame update
    void Start()
    {
        m_rg = GetComponent<Rigidbody2D>();
        m_gc = FindObjectOfType<GameController>();
    }

    // Update is called once per frame
    void Update()
    {
        m_rg.velocity = Vector2.down * moveSpeed;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("DeathZone"))
        {
            m_gc.SetIsGameover(true);
            Destroy(gameObject);
            Debug.Log("Va cham vs DeathZone ");
        }
    }
}
