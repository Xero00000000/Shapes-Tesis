using UnityEngine;

public class FollowAndDie : MonoBehaviour
{
    GameObject player;
    [SerializeField] float lifeTime;
    [SerializeField] float verticalOffset;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = GameObject.FindWithTag("Player");
        transform.SetParent(player.transform);
        transform.localPosition = new Vector3(0, verticalOffset, 0);
        transform.localRotation = Quaternion.identity;
        Destroy(gameObject, lifeTime);
    }
}
