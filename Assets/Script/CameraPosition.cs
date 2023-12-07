using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraPosition : MonoBehaviour
{
    // Reference to CSVReader script
    public CSVReader csvReader;

    // Reference to the camera prefab
    public GameObject camPrefab;

    // Start is called before the first frame update
    void Start()
    {
        SetCamera();
    }

    // Update is called once per frame
    void SetCamera()
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

        float halfMaxY = maxY / 2f;

        // Spawn the camera prefab at the position (halfMaxY * 10, halfMaxY, 0) and rotation of Quaternion.identity
        GameObject instantiatedCam = Instantiate(camPrefab, new Vector3(halfMaxY * 10, halfMaxY, halfMaxY * -5), Quaternion.identity);
    }
}
