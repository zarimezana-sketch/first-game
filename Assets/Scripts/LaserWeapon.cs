using System.Collections;
using UnityEngine;

public class LaserWeapon : MonoBehaviour
{
    [Header("Точки и объекты")]
    [SerializeField] private Transform muzzlePoint;
    [SerializeField] private GameObject muzzleFlash;
    [SerializeField] private LineRenderer laserLine;

    [Header("Параметры лазера")]
    [SerializeField] private float maxDistance = 15f;
    [SerializeField] private float fireCooldown = 0.2f;
    [SerializeField] private float effectDuration = 0.06f;
    [SerializeField] private int damage = 1;
    [SerializeField] private LayerMask hitMask;

    private float nextFireTime;
    private Coroutine effectRoutine;

    private void Awake()
    {
        if (muzzleFlash != null) muzzleFlash.SetActive(false);
        if (laserLine != null)
        {
            laserLine.positionCount = 2;
            laserLine.enabled = false;
        }
    }

    private void Update()
    {
        if ((Input.GetMouseButton(0) || Input.GetKey(KeyCode.J)) && Time.time >= nextFireTime)
        {
            nextFireTime = Time.time + fireCooldown;
            FireLaser();
        }
    }

    private void FireLaser()
    {
        if (muzzlePoint == null) return;

        Vector2 startPos = muzzlePoint.position;
        Vector2 direction = muzzlePoint.right;

        RaycastHit2D hit = Physics2D.Raycast(startPos, direction, maxDistance, hitMask);
        Vector2 endPos = hit.collider != null ? hit.point : startPos + direction * maxDistance;

        if (hit.collider != null && hit.collider.TryGetComponent<EnemyHealth>(out var enemy))
        {
            enemy.TakeDamage(damage);
        }

        if (effectRoutine != null)
            StopCoroutine(effectRoutine);

        effectRoutine = StartCoroutine(ShowEffects(startPos, endPos));
    }

    private IEnumerator ShowEffects(Vector2 startPos, Vector2 endPos)
    {
        if (muzzleFlash != null)
            muzzleFlash.SetActive(true);

        if (laserLine != null)
        {
            laserLine.enabled = true;
            laserLine.SetPosition(0, startPos);
            laserLine.SetPosition(1, endPos);
        }

        yield return new WaitForSeconds(effectDuration);

        if (muzzleFlash != null)
            muzzleFlash.SetActive(false);

        if (laserLine != null)
            laserLine.enabled = false;
    }
}

public class EnemyHealth : MonoBehaviour
{
    public void TakeDamage(int amount) {}
}