using UnityEngine;

public class HandManager : MonoBehaviour
{
    [SerializeField]
    private GameObject hand;

    [SerializeField]
    private GameObject player;

    [SerializeField]
    private float radius = 1f;

    [SerializeField]
    private Vector3 offset = Vector3.zero; 

    void Start()
    {
        if (hand != null && player != null)
        {
            
            Vector3 initialPosition = player.transform.position + Vector3.right * radius + offset;
            hand.transform.position = initialPosition;
        }
    }

    void Update()
    {
        if (hand != null && player != null)
        {
            
            Vector3 mouseScreenPosition = Input.mousePosition;
            Ray ray = Camera.main.ScreenPointToRay(mouseScreenPosition);
            Plane plane = new Plane(Vector3.up, player.transform.position);
            if (plane.Raycast(ray, out float distance))
            {
                Vector3 mouseWorldPosition = ray.GetPoint(distance);

                
                Vector3 direction = (mouseWorldPosition - player.transform.position).normalized;

                
                hand.transform.position = player.transform.position + direction * radius + offset;

                
                Vector3 lookDirection = player.transform.position - hand.transform.position;
                lookDirection.y = 0;
                if (lookDirection != Vector3.zero)
                {
                    Quaternion rotation = Quaternion.LookRotation(lookDirection);
                    hand.transform.rotation = Quaternion.Euler(90, rotation.eulerAngles.y, 0);
                }

                
                Vector3 toHand = hand.transform.position - player.transform.position;
                Vector3 forward = player.transform.forward;
                Vector3 cross = Vector3.Cross(forward, toHand);

                if (cross.y > 0)
                {
                    // La main est à droite du joueur
                    Vector3 scale = hand.transform.localScale;
                    if (scale.x < 0)
                    {
                        hand.transform.localScale = new Vector3(-scale.x, scale.y, scale.z);
                    }
                }
                else if (cross.y < 0)
                {
                    
                    Vector3 scale = hand.transform.localScale;
                    if (scale.x > 0)
                    {
                        hand.transform.localScale = new Vector3(-scale.x, scale.y, scale.z);
                    }
                }
                // Si cross.y == 0, la main est directement devant ou derrière le joueur, aucun flip nécessaire
            }
        }
    }
}
