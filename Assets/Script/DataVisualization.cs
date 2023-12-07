using System.Collections.Generic;
using UnityEngine;

public class AxisDrawer : MonoBehaviour
{
    public CSVReader csvReader;
    public LineRenderer xAxisRenderer;
    public LineRenderer yAxisRenderer;
    public GameObject cubePrefab;  // Prefab for the cube

    void Start()
    {
        DrawAxes();
        SpawnCube();
    }

    void DrawAxes()
    {
        List<Vector3> dataPoints = csvReader.ReadCSVData();

        if (dataPoints.Count >= 2)
        {
            // Set the first data point as (0, 0)
            Vector3 origin = Vector3.zero;

            // Find the maximum Y value from the data points
            float maxY = float.MinValue;
            foreach (Vector3 point in dataPoints)
            {
                if (point.y > maxY)
                {
                    maxY = point.y;
                }
            }

            // Set the X-axis endpoints relative to the origin
            Vector3 xAxisEnd = new Vector3(maxY * 10, 0, 0);

            // Set the Y-axis endpoints relative to the origin
            Vector3 yAxisEnd = new Vector3(0, maxY, 0);

            // Draw X-axis
            Vector3[] xAxisPoints = { origin, xAxisEnd };
            DrawLine(xAxisRenderer, xAxisPoints);

            // Draw Y-axis
            Vector3[] yAxisPoints = { origin, yAxisEnd };
            DrawLine(yAxisRenderer, yAxisPoints);
        }
        else
        {
            Debug.LogError("Insufficient data points to draw axes.");
        }
    }

    void DrawLine(LineRenderer lineRenderer, Vector3[] points)
    {
        lineRenderer.positionCount = points.Length;
        lineRenderer.SetPositions(points);
    }

    void SpawnCube()
    {
        // Find the maximum Y value from the data points again
        List<Vector3> dataPoints = csvReader.ReadCSVData();
        float maxY = float.MinValue;
        foreach (Vector3 point in dataPoints)
        {
            if (point.y > maxY)
            {
                maxY = point.y;
            }
        }

        // Calculate half of maxY
        float halfMaxY = maxY / 2f;

        // Spawn the cube at the position (halfMaxY, halfMaxY, 0) and scale of (maxY, maxY, maxY)
        GameObject cube = Instantiate(cubePrefab, new Vector3(halfMaxY * 10, halfMaxY, 0), Quaternion.identity);
        cube.transform.localScale = new Vector3(maxY * 10, maxY, maxY);
    }
}
