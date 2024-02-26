using Unity.VisualScripting;
using UnityEngine;

public class Parallax : MonoBehaviour
{
    [Header("Настройки Parallax эффекта")]

    [SerializeField] private float speed = 100f;

    [Space(10)]

    [SerializeField] private SpriteRenderer bgRenderer;

    private void Update() {
        bgRenderer.material.mainTextureOffset += new Vector2(speed * Time.deltaTime, 0);
    }
}
