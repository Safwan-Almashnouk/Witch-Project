using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class IceBuilder : MonoBehaviour
{
    public LineRenderer lineRenderer;
    public GameObject wallSegmentPrefab;
    public float segmentSpacing = 0.2f;
    public float energyPerUnit = 1f;
    public float maxWallLength = 100f;
    public LayerMask groundLayer;

    private List<Vector2> points = new List<Vector2>();
    private Vector2 lastPoint;
    private bool isDrawing = false;
    private float totalLength = 0f;

    private WeaponManager weaponManager;
    private IceBallStrategy IceBallStrategy;
    internal bool isActive = false;

    public LayerMask enemyLayer;
    public float checkRadius = 0.3f;


    private void Start()
    {
        weaponManager = GetComponentInParent<WeaponManager>();
        IceBallStrategy = GetComponentInParent<IceBallStrategy>();
    }
    void Update()
    {
        if (!isActive)
        {
            return;
        }

        if (Input.GetMouseButtonDown(0))
        {
            StartDrawing();
            weaponManager.canSwitch = false;
            IceBallStrategy.canfire = false;
        }

        if (isDrawing && Input.GetMouseButton(0))
        {
            ContinueDrawing();
        }

        if (Input.GetMouseButtonUp(0))
        {
            FinishDrawing();
            isActive = false;
            Debug.Log("Hiiii");
            weaponManager.canSwitch = true;
            IceBallStrategy.canfire = true;
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
            float energyCost = dist * energyPerUnit;

            if (IceBallStrategy.currentEnergy <= 0)
            {
                FinishDrawing();
                return;
            }
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
        IceBallStrategy.currentEnergy = 0;
        IceBallStrategy.recharging = true;
    }
}
