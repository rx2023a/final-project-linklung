using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SplineMesh;

public class SplinePositionSetter : MonoBehaviour
{
    public CSVReader csvReader;
    public Spline spline;
    public SplineMeshTiling splineMeshTiling; // Reference to the SplineMeshTiling script
    public SplineSmoother splineSmoother; // Reference to the SplineSmoother script

    void Start()
    {
        // Read CSV data from the CSVReader
        List<Vector3> csvData = csvReader.ReadCSVData();

        // Set the spline nodes based on CSV data
        SetSplineNodes(csvData);

        // Refresh the spline curves
        spline.RefreshCurves();

        // Generate the mesh using SplineMeshTiling
        splineMeshTiling.CreateMeshes();

        // Smooth the spline using SplineSmoother
        splineSmoother.SmoothAll();
    }

    void SetSplineNodes(List<Vector3> csvData)
    {
        // Clear existing nodes in the spline
        spline.nodes.Clear();

        // Calculate the max value of y
        float maxY = float.MinValue;
        foreach (Vector3 dataPoint in csvData)
        {
            maxY = Mathf.Max(maxY, dataPoint.y);
        }

        // Add nodes to the spline based on CSV data
        for (int i = 0; i < csvData.Count; i++)
        {
            Vector3 dataPoint = csvData[i];

            // Adjust the direction based on the X value plus the calculated offset
            float offset = maxY / csvData.Count;

            Vector3 direction = new Vector3(dataPoint.x * 10 + i * offset, dataPoint.y, dataPoint.z);

            SplineNode node = new SplineNode(direction, direction);
            spline.AddNode(node);
        }
    }
}
