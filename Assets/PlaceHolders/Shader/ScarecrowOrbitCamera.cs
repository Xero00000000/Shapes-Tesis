using UnityEngine;

public class ScarecrowOrbitCamera : MonoBehaviour
{
    [Header("Target")]
    public Transform target;

    [Header("Distancia base")]
    public float baseDistance = 6f;
    public float distanceVariation = 2f;
    public float distanceSmoothSpeed = 1.5f;
    public Vector3 lookOffset = new Vector3(0, 1.5f, 0); 

    [Header("Apertura lateral (XZ)")]
    public float maxOrbitExpansion = 3f;
    public float expansionSmoothSpeed = 1.5f;

    [Header("Altura / Rotación X")]
    public float minX = 70f;
    public float maxX = 94f;
    public float xSmoothSpeed = 2f;

    [Header("Movimiento horizontal")]
    public float baseYSpeed = 15f;
    public float yVariation = 10f;

    [Header("Movimiento orgánico")]
    public float noiseSpeed = 0.5f;

    [Header("Patrón tipo vuelo")]
    public float waveAmplitude = 5f;
    public float waveFrequency = 0.5f;

    [Header("Impulsos de vuelo")]
    public float approachChance = 0.3f;
    public float approachStrength = 2f;
    public float approachDuration = 2f;

    private float currentX;
    private float currentY;

    private float currentDistance;
    private float targetDistance;

    private float currentExpansion;
    private float targetExpansion;

    private float noiseOffset;
    private float time;

    private float approachTimer = 0f;
    private float approachOffset = 0f;

    void Start()
    {
        currentY = transform.eulerAngles.y;
        currentX = Random.Range(minX, maxX);

        noiseOffset = Random.Range(0f, 100f);

        currentDistance = baseDistance;
        targetDistance = baseDistance;
    }

    void Update()
    {
        if (target == null) return;

        time += Time.deltaTime;
        float noise = Mathf.PerlinNoise(noiseOffset, time * noiseSpeed);
        noise = (noise - 0.5f) * 2f;
        float dynamicYSpeed = baseYSpeed + (noise * yVariation);
        currentY += dynamicYSpeed * Time.deltaTime;

        float wave = Mathf.Sin(time * waveFrequency) * waveAmplitude;

        float targetX = Mathf.Clamp(
            Mathf.Lerp(minX, maxX, (noise + 1f) / 2f) + wave,
            minX,
            maxX
        );

        currentX = Mathf.Lerp(currentX, targetX, Time.deltaTime * xSmoothSpeed);
        float baseOffset = noise * distanceVariation;

        if (approachTimer <= 0f && Random.value < approachChance * Time.deltaTime)
        {
            approachOffset = Random.Range(-approachStrength, approachStrength);
            approachTimer = approachDuration;
        }

        if (approachTimer > 0f)
        {
            float t = approachTimer / approachDuration;
            baseOffset += approachOffset * t;
            approachTimer -= Time.deltaTime;
        }

        targetDistance = baseDistance + baseOffset;
        currentDistance = Mathf.Lerp(currentDistance, targetDistance, Time.deltaTime * distanceSmoothSpeed);

        float expansionFactor = Mathf.Clamp01((noise + 1f) * 0.5f);

        if (approachOffset > 0)
            expansionFactor += 0.3f;

        targetExpansion = maxOrbitExpansion * expansionFactor;
        currentExpansion = Mathf.Lerp(currentExpansion, targetExpansion, Time.deltaTime * expansionSmoothSpeed);

        float tilt = noise * 10f;
        Quaternion rotation = Quaternion.Euler(currentX, currentY, tilt);

        Vector3 basePos = target.position - (rotation * Vector3.forward * currentDistance);

        Vector3 lateralOffset = rotation * Vector3.right * currentExpansion;

        Vector3 finalPos = basePos + lateralOffset;

        transform.position = finalPos;

        Vector3 lookDir = ((target.position + lookOffset) - transform.position).normalized; 
        Quaternion lookRot = Quaternion.LookRotation(lookDir);
        Quaternion finalRot = Quaternion.Slerp(rotation, lookRot, 0.7f);
        transform.rotation = finalRot;
    }
}