using UnityEngine;

class TargetingManager : MonoBehaviour
{
    public InputReader input;
    public Camera cam;
    public Vector3 mouseWorldPosition;

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
