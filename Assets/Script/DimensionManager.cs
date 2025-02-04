using UnityEngine;
using System;

public class DimensionManager : MonoBehaviour
{
    public static DimensionManager Instance;
    public bool IsInWhiteDimension = true;
    public event Action OnDimensionChange;

    void Awake()
    {
        // Assurez-vous qu'il n'existe qu'une seule instance du gestionnaire
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    public void SwitchDimension()
    {
        IsInWhiteDimension = !IsInWhiteDimension;
        OnDimensionChange?.Invoke();
    }
}
