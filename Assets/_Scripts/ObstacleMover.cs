using UnityEngine;

public class ObstacleMover : MonoBehaviour
{
    public float speed = 5f;
    public float destroyTime = 10f; // Detonation time

    void Start()
    {
        Destroy(gameObject, destroyTime);
    }

    void Update()
    {
        transform.position += Vector3.left * speed * Time.deltaTime; // <==
    }
}