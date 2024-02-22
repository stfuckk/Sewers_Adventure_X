using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chunk : MonoBehaviour
{   
    private float speed;
    private float acceleration;
    private float maxSpeed;

    void Start() {
        speed = Mathf.Clamp(speed, 0f, maxSpeed);
    }

    void FixedUpdate() {
        speed += acceleration;
        transform.Translate(Vector2.left * speed / 10 * Time.deltaTime);
    }

    // Функция вызывается в GameManager, чтобы синхронизировать скорость платформ при спавне
    public void SyncronizeSpeed(float speed, float acceleration, float maxSpeed) { 
        this.speed = speed;
        this.acceleration = acceleration;
        this.maxSpeed = maxSpeed;
    }
}
