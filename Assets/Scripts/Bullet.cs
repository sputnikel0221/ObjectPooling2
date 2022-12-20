using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private void Update() {
        transform.Translate(Vector2.up * 10 * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other) {
        if(other.CompareTag("Wall")){
            // Destroy(gameObject);
            Pooler.Despawn(gameObject);
        }
    }
}
