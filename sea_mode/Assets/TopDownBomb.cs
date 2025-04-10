using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TopDownBomb : MonoBehaviour
{
    Vector3 startPos;
    Vector3 targetPos;
    float travelTime = 1f;
    float arcHeight = 2f;
    float timer = 0f;

    public void Init(Vector3 target)
    {
        startPos = transform.position;
        targetPos = target;
    }

    void Update()
    {
        timer += Time.deltaTime;
        float t = timer / travelTime;

        if (t > 1f)
        {
            // Bomb landed
            Destroy(gameObject); // or explode
            return;
        }

        Vector3 pos = Vector3.Lerp(startPos, targetPos, t);
        pos.y += Mathf.Sin(t * Mathf.PI) * arcHeight;
        transform.position = pos;
    }

    void OnCollisionEnter2D(Collision2D collision)
{
    if (collision.gameObject.CompareTag("Enemy"))
    {
        // Example damage system
        EnemyBoat enemy = collision.gameObject.GetComponent<EnemyBoat>();
        if (enemy != null)
        {
            enemy.TakeDamage(30f); // example value
        }

        Destroy(gameObject); // destroy bomb on impact
    }
}
}
