
using UnityEngine;

public class MuzzleFlash : MonoBehaviour
{
    [SerializeField] private float lifetime = 0.08f;


    private void Awake()
    {
        Destroy(gameObject, lifetime);
    }
}
