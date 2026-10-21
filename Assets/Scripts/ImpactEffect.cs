
using UnityEngine;

public class ImpactEffect : MonoBehaviour
{
    [SerializeField] private float lifetime = 0.5f;

    private void Awake()
    {
        Destroy(gameObject, lifetime);
    }
}
