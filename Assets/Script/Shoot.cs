using UnityEngine;

public class Shoot : MonoBehaviour
{
    [SerializeField]
    private GameObject projectilePrefab;

    [SerializeField]
    private float projectileSpeed = 10f;

    [SerializeField]
    private Transform spawnPoint;

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

            Destroy(projectile, 5f); // Détruire le projectile après 5 secondes
        }
    }
}
