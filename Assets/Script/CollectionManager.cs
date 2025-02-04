using UnityEngine;
using TMPro;

public class CollectionManager : MonoBehaviour
{
    public TextMeshProUGUI ameUI;
    int ameCollected = 0;

    private void OnEnable()
    {
        Ame.OnAmeCollected += AmeCollected;
    }

    private void AmeCollected()
    {
        ameCollected++;
        ameUI.text = ameCollected.ToString();
    }
}
