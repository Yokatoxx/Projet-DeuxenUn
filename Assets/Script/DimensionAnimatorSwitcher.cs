using UnityEngine;

public class DimensionAnimationSwitcher : MonoBehaviour
{
    public RuntimeAnimatorController WhiteDimensionController;
    public RuntimeAnimatorController BlackDimensionController;
    private Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
        DimensionManager.Instance.OnDimensionChange += UpdateAnimator;
        UpdateAnimator();
    }

    void OnDestroy()
    {
        DimensionManager.Instance.OnDimensionChange -= UpdateAnimator;
    }

    void UpdateAnimator()
    {
        if (DimensionManager.Instance.IsInWhiteDimension)
            animator.runtimeAnimatorController = WhiteDimensionController;
        else
            animator.runtimeAnimatorController = BlackDimensionController;
    }
}
