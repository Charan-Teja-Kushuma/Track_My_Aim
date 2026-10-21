
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class WeaponController : MonoBehaviour
{
    [Header("Weapon Settings")]
    [SerializeField] private float damage = 25f;
    [SerializeField] private float range = 100f;
    [SerializeField] private float fireRate = 5f;

    [Header("Ammo Settings")]
    [SerializeField] private int magazineSize = 30;
    [SerializeField] private float reloadDuration = 1.5f;

    [Header("References")]
    [SerializeField] private Camera playerCamera;
    [SerializeField] private GameObject impactEffectPrefab;

    [Header("Muzzle Flash")]
    [SerializeField] private GameObject muzzleFlashPrefab;
    [SerializeField] private Transform muzzlePoint;


    [Header("Bullet Tracer")]
    [SerializeField] private BulletTracer bulletTracerPrefab;
    [SerializeField] private float tracerMissDistance = 100f;


    public int CurrentAmmo => currentAmmo;
    public int MagazineSize => magazineSize;
    public bool LastShotHitTarget { get; private set; }

    private int currentAmmo;
    private float nextFireTime;
    private bool isReloading;

    private void Awake()
    {
        currentAmmo = magazineSize;

        if (playerCamera == null)
            playerCamera = GetComponentInChildren<Camera>();
    }

    private void Update()
    {
        if (Keyboard.current == null || Mouse.current == null)
            return;

        if (Keyboard.current.rKey.wasPressedThisFrame && !isReloading)
        {
            if (currentAmmo < magazineSize)
                StartCoroutine(Reload());
        }

        if (Cursor.lockState != CursorLockMode.Locked ||
            playerCamera == null ||
            isReloading)
        {
            return;
        }

        if (Mouse.current.leftButton.isPressed &&
            Time.time >= nextFireTime)
        {
            if (currentAmmo <= 0)
                return;

            nextFireTime = Time.time + 1f / fireRate;
            Shoot();
        }
    }

    private void Shoot()
    {
        currentAmmo--;
        LastShotHitTarget = false;

        Ray ray = playerCamera.ViewportPointToRay(
            new Vector3(0.5f, 0.5f, 0f)
        );

        Vector3 destination =
            ray.origin + ray.direction * Mathf.Min(range, tracerMissDistance);

        if (Physics.Raycast(ray, out RaycastHit hit, range))
        {
            destination = hit.point;

            TargetHealth target =
                hit.collider.GetComponentInParent<TargetHealth>();

            if (target != null)
            {
                LastShotHitTarget = true;
                target.TakeDamage(damage);
            }

            if (impactEffectPrefab != null)
            {
                Instantiate(
                    impactEffectPrefab,
                    hit.point,
                    Quaternion.LookRotation(hit.normal)
                );
            }

            Debug.DrawLine(ray.origin, hit.point, Color.red, 1f);
        }

        // Spawn a tracer from the rifle muzzle toward the crosshair destination.
        SpawnBulletTracer(destination);

        // Brief muzzle flash at the rifle barrel.
        if (muzzleFlashPrefab != null && muzzlePoint != null)
        {
            Instantiate(
                muzzleFlashPrefab,
                muzzlePoint.position,
                muzzlePoint.rotation
            );
        }

        Debug.Log($"Ammo: {currentAmmo}/{magazineSize}");
    }

    private void SpawnBulletTracer(Vector3 destination)
    {
        if (bulletTracerPrefab == null || muzzlePoint == null)
            return;

        BulletTracer tracer = Instantiate(
            bulletTracerPrefab,
            muzzlePoint.position,
            Quaternion.identity
        );

        tracer.Initialize(muzzlePoint.position, destination);
    }

    private IEnumerator Reload()
    {
        isReloading = true;
        Debug.Log("Reloading...");

        yield return new WaitForSeconds(reloadDuration);

        currentAmmo = magazineSize;
        isReloading = false;

        Debug.Log($"Reload complete: {currentAmmo}/{magazineSize}");
    }
}
