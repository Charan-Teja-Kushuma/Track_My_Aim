
using UnityEngine;

public class BulletTracer : MonoBehaviour
{
    [SerializeField] private float speed = 250f;
    [SerializeField] private float lifetime = 0.15f;

    private Vector3 destination;
    private bool initialized;

    public void Initialize(Vector3 start, Vector3 end)
    {
        transform.position = start;
        destination = end;
        initialized = true;

        Vector3 direction = end - start;
        if (direction.sqrMagnitude > 0.0001f)
            transform.rotation = Quaternion.LookRotation(direction);
    }

    private void Update()
    {
        if (!initialized)
            return;

        transform.position = Vector3.MoveTowards(
            transform.position,
            destination,
            speed * Time.deltaTime
        );

        if (Vector3.Distance(transform.position, destination) < 0.02f)
            Destroy(gameObject);
    }

    private void Awake()
    {
        Destroy(gameObject, lifetime);
    }
}
