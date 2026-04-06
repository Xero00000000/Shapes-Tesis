using UnityEngine;

public class AlwaysFaceCamera : MonoBehaviour
{
    private Camera cam;

    [Header("Rotaciones")]
    public bool ignoreX = false;
    public bool ignoreY = false;
    public bool ignoreZ = false;
    void Start()
    {
        cam = Camera.main;
    }

    void LateUpdate()
    {
        Vector3 direction = cam.transform.position - transform.position;

        Quaternion lookRotation = Quaternion.LookRotation(direction);

        Vector3 euler = lookRotation.eulerAngles;
        Vector3 currentEuler = transform.eulerAngles;

        if (ignoreX) euler.x = currentEuler.x;
        if (ignoreY) euler.y = currentEuler.y;
        if (ignoreZ) euler.z = currentEuler.z;

        transform.rotation = Quaternion.Euler(euler);
    }
}
