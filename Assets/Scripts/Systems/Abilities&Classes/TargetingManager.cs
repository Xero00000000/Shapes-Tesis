using UnityEngine;

class TargetingManager : MonoBehaviour
{
    public InputReader input;
    public Camera cam;
    public Vector3 mouseWorldPosition;
    public Quaternion lookRotation;
    public bool isTargetting = false;

    TargetingStrategy currentStrategy;

    private void Update()
    {
        if (currentStrategy != null && currentStrategy.IsTargetting)
        {
            currentStrategy.Update();
        }
    }

    public void SetCurrentStrategy(TargetingStrategy strategy) => currentStrategy = strategy;
    public void ClearCurrentStrategy() => currentStrategy = null;
}
