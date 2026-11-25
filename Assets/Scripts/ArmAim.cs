using UnityEngine;

public class ArmAim : MonoBehaviour
{
    [SerializeField] private Transform armPivot;
    [SerializeField] private Camera mainCamera;

    private void Awake()
    {
        if (mainCamera == null)
            mainCamera = Camera.main;
    }

    private void Update()
    {
        if (armPivot == null || mainCamera == null) return;

        Vector3 mouseWorld = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        Vector2 dir = mouseWorld - armPivot.position;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;

        armPivot.rotation = Quaternion.Euler(0f, 0f, angle);

        Vector3 scale = armPivot.localScale;
        scale.y = (dir.x < 0f) ? -1f : 1f;
        armPivot.localScale = scale;
    }
}