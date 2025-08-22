using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // Changed from UIElements to UI for cooldown bar

public class IceBuilder : MonoBehaviour
{
    [Header("Wall Settings")]
    public LineRenderer lineRenderer;
    public GameObject wallSegmentPrefab;
    public float segmentSpacing = 0.2f;
    public float maxWallLength = 100f;
    public LayerMask groundLayer;
    public LayerMask enemyLayer;
    public float checkRadius = 0.3f;

    [Header("Cooldown Settings")]
    public float cooldownTime = 5f; // how long until we can use again
    private float nextReadyTime;

    [Header("UI")]
    public Image cooldownImage; // drag in your UI fill image

    private List<Vector2> points = new List<Vector2>();
    private Vector2 lastPoint;
    private bool isDrawing = false;
    private float totalLength = 0f;

    private WeaponManager weaponManager;
    private IceBallStrategy iceBallStrategy;
    internal bool isActive = false;

    private void Start()
    {
        weaponManager = GetComponentInParent<WeaponManager>();
        iceBallStrategy = GetComponentInParent<IceBallStrategy>();

        if (cooldownImage != null)
            cooldownImage.fillAmount = 1f; // starts ready
    }

    private void Update()
    {
        // Update cooldown UI
        float cooldownRemaining = nextReadyTime - Time.time;
        if (cooldownRemaining > 0)
        {
            cooldownImage.fillAmount = 1 - (cooldownRemaining / cooldownTime);
        }
        else
        {
            cooldownImage.fillAmount = 1f;
        }

        if (!isActive)
            return;

        if (Input.GetMouseButtonDown(0))
        {
            if (Time.time >= nextReadyTime) // only start if off cooldown
            {
                StartDrawing();
                weaponManager.canSwitch = false;
                iceBallStrategy.canfire = false;
            }
        }

        if (isDrawing && Input.GetMouseButton(0))
        {
            ContinueDrawing();
        }

        if (Input.GetMouseButtonUp(0) && isDrawing)
        {
            FinishDrawing();
            isActive = false;
            weaponManager.canSwitch = true;
            iceBallStrategy.canfire = true;
        }
    }

    public void StartDrawing()
    {
        Vector2 start = GetMouseWorldPos();
        RaycastHit2D hit = Physics2D.Raycast(start, Vector2.down, 50f, groundLayer);
        if (hit.collider == null)
        {
            Debug.Log("Must start drawing on ground!");
            return;
        }
        isDrawing = true;
        points.Clear();
        totalLength = 0f;
        lastPoint = hit.point;
        points.Add(lastPoint);
        lineRenderer.positionCount = 1;
        lineRenderer.SetPosition(0, lastPoint);
    }

    Vector2 GetMouseWorldPos()
    {
        return Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }

    public void ContinueDrawing()
    {
        Vector2 current = GetMouseWorldPos();
        float dist = Vector2.Distance(current, lastPoint);

        if (dist > segmentSpacing)
        {
            totalLength += dist;
            if (totalLength > maxWallLength)
            {
                FinishDrawing();
                return;
            }

            Vector2 direction = (current - lastPoint).normalized;
            float distanceRemaining = dist;

            while (distanceRemaining >= segmentSpacing)
            {
                Vector2 nextPoint = lastPoint + direction * segmentSpacing;

                if (Physics2D.OverlapCircle(nextPoint, checkRadius, enemyLayer))
                {
                    Debug.Log("Enemy in the way, stopping wall.");
                    FinishDrawing();
                    return;
                }
                points.Add(nextPoint);
                lastPoint = nextPoint;
                distanceRemaining -= segmentSpacing;
                lineRenderer.positionCount = points.Count;
                lineRenderer.SetPosition(points.Count - 1, nextPoint);
            }
        }
    }

    void FinishDrawing()
    {
        isDrawing = false;

        for (int i = 1; i < points.Count; i++)
        {
            Vector2 pos = (points[i] + points[i - 1]) / 2f;
            Vector2 dir = points[i] - points[i - 1];
            float length = dir.magnitude;
            float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;

            GameObject wallSegment = Instantiate(wallSegmentPrefab, pos, Quaternion.Euler(0, 0, angle));
            wallSegment.transform.localScale = new Vector3(length, wallSegment.transform.localScale.y, 1);
        }
        lineRenderer.positionCount = 0;

        // Start cooldown here
        nextReadyTime = Time.time + cooldownTime;

        // Force ability off until player re-selects it
        isActive = false;
    }
}
