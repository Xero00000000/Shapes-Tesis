using UnityEngine;

public class BirdEyeCamera : MonoBehaviour
{
    public Transform target;

    [Header("Distancia")]
    public float baseDistance = 6f;
    public float distanceVariation = 2f;
    public float distanceSpeed = 1.5f;

    [Header("Órbita")]
    public float baseYSpeed = 20f;
    public float yVariation = 10f;

    [Header("Altura")]
    public float minX = 70f;
    public float maxX = 90f;
    public float xSmooth = 2f;

    [Header("Apertura lateral")]
    public float maxExpansion = 3f;
    public float expansionSmooth = 2f;

    [Header("Movimiento orgánico")]
    public float noiseSpeed = 0.5f;

    [Header("Ciclo")]
    public float orbitDuration = 8f;
    public float focusDuration = 3f;

    private float currentX;
    private float currentY;

    private float currentDistance;
    private float targetDistance;

    private float currentExpansion;
    private float targetExpansion;

    private float noiseOffset;
    private float time;

    private float cycleTimer;
    private bool focusing = false;

    void Start()
    {
        currentY = transform.eulerAngles.y;
        currentX = Random.Range(minX, maxX);

        currentDistance = baseDistance;
        targetDistance = baseDistance;

        noiseOffset = Random.Range(0f, 100f);
    }

    void Update()
    {
        if (target == null) return;

        time += Time.deltaTime;
        cycleTimer += Time.deltaTime;
        if (!focusing && cycleTimer > orbitDuration)
        {
            focusing = true;
            cycleTimer = 0f;
        }
        else if (focusing && cycleTimer > focusDuration)
        {
            focusing = false;
            cycleTimer = 0f;
        }
        float noise = Mathf.PerlinNoise(noiseOffset, time * noiseSpeed);
        noise = (noise - 0.5f) * 2f;

        if (!focusing)
        {
            float dynamicY = baseYSpeed + noise * yVariation;
            currentY += dynamicY * Time.deltaTime;
            float targetX = Mathf.Lerp(minX, maxX, (noise + 1f) * 0.5f);
            currentX = Mathf.Lerp(currentX, targetX, Time.deltaTime * xSmooth);
            targetDistance = baseDistance + noise * distanceVariation;
            currentDistance = Mathf.Lerp(currentDistance, targetDistance, Time.deltaTime * distanceSpeed);
            float expansionFactor = Mathf.Clamp01((noise + 1f) * 0.5f);
            targetExpansion = maxExpansion * expansionFactor;
            currentExpansion = Mathf.Lerp(currentExpansion, targetExpansion, Time.deltaTime * expansionSmooth);
        }

        else
        {
            currentY += baseYSpeed * 0.2f * Time.deltaTime;
            currentX = Mathf.Lerp(currentX, 85f, Time.deltaTime * 2f);
            currentDistance = Mathf.Lerp(currentDistance, baseDistance * 0.5f, Time.deltaTime * 2f);
            currentExpansion = Mathf.Lerp(currentExpansion, 0f, Time.deltaTime * 2f);
        }

        Quaternion rotation = Quaternion.Euler(currentX, currentY, 0);

        Vector3 basePos = target.position - (rotation * Vector3.forward * currentDistance);
        Vector3 lateralOffset = rotation * Vector3.right * currentExpansion;

        Vector3 finalPos = basePos + lateralOffset;

        transform.position = finalPos;

        Vector3 lookDir = (target.position - transform.position).normalized;
        transform.rotation = Quaternion.LookRotation(lookDir);
    }
}