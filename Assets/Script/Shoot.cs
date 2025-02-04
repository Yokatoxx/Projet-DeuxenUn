using UnityEngine;

public class Shoot : MonoBehaviour
{
    [SerializeField]
    private GameObject projectilePrefabWhite;

    [SerializeField]
    private GameObject projectilePrefabOther;

    [SerializeField]
    private float projectileSpeed = 10f;

    [SerializeField]
    private Transform spawnPoint;

    [SerializeField]
    private float shootCooldown = 1f;

    [SerializeField]
    private float minShootDistance = 1f;

    public Transform playerTransform; // Variable publique ajoutée pour le Transform du joueur

    private DimensionManager dimensionManager;
    private float lastShootTime = -Mathf.Infinity;

    void Start()
    {
        dimensionManager = DimensionManager.Instance;
        UpdateProjectilePrefab();

        if (dimensionManager != null)
        {
            dimensionManager.OnDimensionChange += UpdateProjectilePrefab;
        }
    }

    void OnDestroy()
    {
        if (dimensionManager != null)
        {
            dimensionManager.OnDimensionChange -= UpdateProjectilePrefab;
        }
    }

    void UpdateProjectilePrefab()
    {
        if (dimensionManager.IsInWhiteDimension)
        {
            projectilePrefab = projectilePrefabWhite;
        }
        else
        {
            projectilePrefab = projectilePrefabOther;
        }
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && Time.time >= lastShootTime + shootCooldown)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            Vector3 targetPoint;

            if (Physics.Raycast(ray, out hit))
            {
                targetPoint = hit.point;
            }
            else
            {
                targetPoint = ray.GetPoint(1000f);
            }

            targetPoint.y = spawnPoint.position.y;
            float distance = Vector3.Distance(targetPoint, playerTransform.position);
            if (distance < minShootDistance)
            {
                return;
            }

            Vector3 direction = (targetPoint - spawnPoint.position).normalized;

            lastShootTime = Time.time;

            GameObject projectile = Instantiate(projectilePrefab, spawnPoint.position, spawnPoint.rotation);

            Rigidbody rb = projectile.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.useGravity = false;
                rb.linearVelocity = new Vector3(direction.x, 0f, direction.z) * projectileSpeed;
            }

            Destroy(projectile, 5f);
        }
    }

    private GameObject projectilePrefab;
}
