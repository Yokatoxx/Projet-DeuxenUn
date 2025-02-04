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

    private DimensionManager dimensionManager;

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
        if (Input.GetMouseButtonDown(0))
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

            // Conserver la hauteur initiale du spawn point
            targetPoint.y = spawnPoint.position.y;

            Vector3 direction = (targetPoint - spawnPoint.position).normalized;

            GameObject projectile = Instantiate(projectilePrefab, spawnPoint.position, spawnPoint.rotation);

            Rigidbody rb = projectile.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.useGravity = false;
                // Appliquer la vitesse uniquement sur les axes X/Z
                rb.linearVelocity = new Vector3(direction.x, 0f, direction.z) * projectileSpeed;
            }

            Destroy(projectile, 5f); // D�truire le projectile apr�s 5 secondes
        }
    }

    // Ajout de la variable projectilePrefab pour basculer dynamiquement
    private GameObject projectilePrefab;
}
