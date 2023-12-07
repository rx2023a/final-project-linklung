using System;
using System.Collections.Generic;
using UnityEngine;

public class CSVReader : MonoBehaviour
{
    public TextAsset csvFile; // Reference to the CSV file as a TextAsset

    public List<Vector3> ReadCSVData()
    {
        List<Vector3> dataPoints = new List<Vector3>();

        try
        {
            string[] lines = csvFile.text.Split('\n');

            for (int i = 0; i < lines.Length; i++)
            {
                string line = lines[i];
                string[] values = line.Split(',');

                // Assuming your CSV has two values per line (x, y)
                if (values.Length == 2)
                {
                    float x, y;

                    if (float.TryParse(values[0], out x) && float.TryParse(values[1], out y))
                    {
                        // Set the z value to 0 for each data point
                        float z = 0f;

                        // Multiply X by 10 for each data point after the first one
                        if (i > 0)
                        {
                            x *= 10f;
                        }

                        dataPoints.Add(new Vector3(x, y, z));
                    }
                    else
                    {
                        Debug.LogError("Invalid numeric values in CSV line: " + line);
                    }
                }
                else
                {
                    Debug.LogError("Invalid CSV format. Make sure each line has two values.");
                }
            }
        }
        catch (Exception e)
        {
            Debug.LogError("Error reading CSV file: " + e.Message);
        }

        return dataPoints;
    }
}
