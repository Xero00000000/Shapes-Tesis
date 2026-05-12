using UnityEngine;

public class RainCasterMovement : MonoBehaviour
{
    [Header("Movement")]
    public float speed = 3f;

    [Header("Area Object")]
    public BoxCollider areaCollider;

    private Vector3 targetPosition;

    void Start()
    {
        PickNewTarget();
    }

    void Update()
    {
        transform.position = Vector3.MoveTowards(
            transform.position,
            targetPosition,
            speed * Time.deltaTime
        );

        if (Vector3.Distance(transform.position, targetPosition) < 0.2f)
        {
            PickNewTarget();
        }
    }

    void PickNewTarget()
    {
        Bounds bounds = areaCollider.bounds;

        float randomX = Random.Range(bounds.min.x, bounds.max.x);
        float randomY = Random.Range(bounds.min.y, bounds.max.y);
        float randomZ = Random.Range(bounds.min.z, bounds.max.z);

        targetPosition = new Vector3(randomX, randomY, randomZ);
    }
}
