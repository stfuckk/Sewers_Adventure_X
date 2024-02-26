using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // на экране всегда 3 платформы, 1 удаляется слева, новая появляется справа - DONE
    // синхронизация скорости новой платформы с появившейся - DONE

    [Header("Настройки генератора платформ")]
    [SerializeField] private List<GameObject> platformPrefabs;
    [SerializeField] private float spawnX = 0f; // позиция спавна по х
    [SerializeField] private float spawnY = 0f; // позиция спавна по y
    [SerializeField] private float deleteX = -10f; // позиция по х, где платформа удалится
    [SerializeField] private float platformWidth = 20f;

    [Space(10)]

    [Header("Настройки скорости и ускорения")]
    [SerializeField] public float speed = 1f;
    [SerializeField] private float maxSpeed = 10f;
    [SerializeField] private float acceleration = 0.05f;

    [Space(10)]

    [Header("Панель FadeOut")]
    [SerializeField] private Animator fadeOutScreenAnimator;

    private GameObject currentPlatform1;
    private GameObject currentPlatform2;
    private GameObject currentPlatform3;


    void Start() {
        fadeOutScreenAnimator.SetTrigger("FadeOut");
        SpawnStartPlatform();
        SpawnPlatform();
        speed = Mathf.Clamp(speed, 0f, maxSpeed);
    }

    void Update() {
        if (currentPlatform1 == null || currentPlatform2 == null || currentPlatform3 == null)
            SpawnPlatform();
        if (currentPlatform1.transform.position.x < deleteX)
            Destroy(currentPlatform1);
        if (currentPlatform2.transform.position.x < deleteX) 
            Destroy(currentPlatform2);
        if (currentPlatform3.transform.position.x < deleteX) 
            Destroy(currentPlatform3);
    }

    void FixedUpdate() {
        speed += acceleration;
    }

    private void SpawnPlatform() {
        int randomIndex = Random.Range(0, platformPrefabs.Count);
        if (currentPlatform1 == null) {
            currentPlatform1 = Instantiate(platformPrefabs[randomIndex], new Vector2(currentPlatform3.transform.position.x + platformWidth, spawnY), Quaternion.identity);
            currentPlatform1.GetComponent<Chunk>().SyncronizeSpeed(speed, acceleration, maxSpeed);
        }
        if (currentPlatform2 == null) {
            currentPlatform2 = Instantiate(platformPrefabs[randomIndex], new Vector2(currentPlatform1.transform.position.x + platformWidth, spawnY), Quaternion.identity);
            currentPlatform2.GetComponent<Chunk>().SyncronizeSpeed(speed, acceleration, maxSpeed);
        }
        if (currentPlatform3 == null) {
            currentPlatform3 = Instantiate(platformPrefabs[randomIndex], new Vector2(currentPlatform2.transform.position.x + platformWidth, spawnY), Quaternion.identity);
            currentPlatform3.GetComponent<Chunk>().SyncronizeSpeed(speed, acceleration, maxSpeed);
        }
    }

    private void SpawnStartPlatform() {
        currentPlatform1 = Instantiate(platformPrefabs[0], new Vector2(spawnX, spawnY), Quaternion.identity);
        currentPlatform1.GetComponent<Chunk>().SyncronizeSpeed(speed, acceleration, maxSpeed);
    }

}
