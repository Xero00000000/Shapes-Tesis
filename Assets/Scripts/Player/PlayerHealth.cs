using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using System.Collections;

public class PlayerHealth : Health
{
    [Header("Volume (Damage Feedback)")]
    [SerializeField] private Volume globalVolume;

    [SerializeField] private float vignetteIntensityOnHit = 0.4f;
    [SerializeField] private float lensIntensityOnHit = -0.4f;
    [SerializeField] private float chromaticIntensityOnHit = 0.5f;

    [SerializeField] private float volumeFadeSpeed = 2f;

    private Vignette vignette;
    private LensDistortion lensDistortion;
    private ChromaticAberration chromatic;

    [Header("Camera Shake")]
    [SerializeField] private Transform cameraTransform;
    [SerializeField] private float shakeDuration = 0.2f;
    [SerializeField] private float shakeStrength = 0.15f;

    private Vector3 originalCamPos;
    private Coroutine volumeCoroutine;
    private Coroutine shakeCoroutine;

    protected override void Awake()
    {
        base.Awake();

        if (globalVolume == null)
        {
            Debug.LogWarning("Global Volume no asignado");
            return;
        }

        globalVolume.profile.TryGet(out vignette);
        globalVolume.profile.TryGet(out lensDistortion);
        globalVolume.profile.TryGet(out chromatic);

        if (vignette != null) vignette.intensity.value = 0f;
        if (lensDistortion != null) lensDistortion.intensity.value = 0f;
        if (chromatic != null) chromatic.intensity.value = 0f;

        if (cameraTransform != null)
        {
            originalCamPos = cameraTransform.localPosition;
        }
    }

    public override void TakeDamage(int amount)
    {
        base.TakeDamage(amount);

        TriggerVolume();

        if (cameraTransform != null)
        {
            if (shakeCoroutine != null) StopCoroutine(shakeCoroutine);
            //shakeCoroutine = StartCoroutine(ShakeCamera());
        }
    }

    // =========================
    // VOLUME FX
    // =========================
    void TriggerVolume()
    {
        if (volumeCoroutine != null)
            StopCoroutine(volumeCoroutine);

        if (vignette != null)
            vignette.intensity.value = vignetteIntensityOnHit;

        if (lensDistortion != null)
            lensDistortion.intensity.value = lensIntensityOnHit;

        if (chromatic != null)
            chromatic.intensity.value = chromaticIntensityOnHit;

        volumeCoroutine = StartCoroutine(VolumeFade());
    }

    IEnumerator VolumeFade()
    {
        while (
            (vignette != null && vignette.intensity.value > 0.01f) ||
            (lensDistortion != null && Mathf.Abs(lensDistortion.intensity.value) > 0.01f) ||
            (chromatic != null && chromatic.intensity.value > 0.01f)
        )
        {
            if (vignette != null)
                vignette.intensity.value = Mathf.Lerp(vignette.intensity.value, 0f, Time.deltaTime * volumeFadeSpeed);

            if (lensDistortion != null)
                lensDistortion.intensity.value = Mathf.Lerp(lensDistortion.intensity.value, 0f, Time.deltaTime * volumeFadeSpeed);

            if (chromatic != null)
                chromatic.intensity.value = Mathf.Lerp(chromatic.intensity.value, 0f, Time.deltaTime * volumeFadeSpeed);

            yield return null;
        }

        if (vignette != null) vignette.intensity.value = 0f;
        if (lensDistortion != null) lensDistortion.intensity.value = 0f;
        if (chromatic != null) chromatic.intensity.value = 0f;
    }

    // =========================
    // CAMERA SHAKE
    // =========================
    //IEnumerator ShakeCamera()
    //{
    //    float elapsed = 0f;

    //    while (elapsed < shakeDuration)
    //    {
    //        Vector3 offset = Random.insideUnitSphere * shakeStrength;
    //        cameraTransform.localPosition = originalCamPos + offset;

    //        elapsed += Time.deltaTime;
    //        yield return null;
    //    }

    //    cameraTransform.localPosition = originalCamPos;
    //}

    protected override void Die()
    {
        base.Die();

        Debug.Log("Jugador murió");
        Time.timeScale = 0f;
    }
}