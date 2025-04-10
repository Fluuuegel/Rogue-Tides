using System.Collections;
using System.Collections.Generic;


using UnityEngine;
using UnityEngine.UI;

public class EnemyBoat : MonoBehaviour
{
    public float speed = 15f;
    public Transform target;
    public float hp = 50f;
    private float maxHp;

    private Rigidbody2D rb;
    public Slider hpBar; // Assign this in the Inspector

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
 

        maxHp = hp;

    if (hpBar != null)
    {
        hpBar.maxValue = 1f;
        hpBar.value = 1f; // Make sure it's full at start
    }
    }

    void Update()
    {
        if (target == null) return;

        Vector2 direction = (target.position - transform.position).normalized;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        rb.MoveRotation(angle - 90f);
        rb.MovePosition(rb.position + direction * speed * Time.deltaTime);

        if (hpBar != null)
        {
            hpBar.value = hp / maxHp;
        }
    }

    public void TakeDamage(float amount)
    {
        hp -= amount;
        Debug.Log("Enemy HP: " + hp);

        if (hpBar != null)
        {
            hpBar.value = hp / maxHp;
        }

        if (hp <= 0)
        {
            Destroy(gameObject);
        }
    }
}