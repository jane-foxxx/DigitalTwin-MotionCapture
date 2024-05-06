using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
public class TransformExporter : MonoBehaviour
{
    public string fileName = "Transforms.txt";
    public Transform[] objectsToExport;

    private void Start()
    {
        ExportTransforms();
    }

    private void ExportTransforms()
    {
        string filePath = Path.Combine(Application.dataPath, fileName);

        using (StreamWriter writer = File.CreateText(filePath))
        {
            foreach (Transform obj in objectsToExport)
            {
                //Vector3 position = obj.position;
                Quaternion rotation = obj.rotation;
                //Vector3 scale = obj.localScale;

                //writer.WriteLine($"Position: {position.x}, {position.y}, {position.z}");
                writer.WriteLine($"Rotation: {rotation.eulerAngles.x}, {rotation.eulerAngles.y}, {rotation.eulerAngles.z}");
                //writer.WriteLine($"Scale: {scale.x}, {scale.y}, {scale.z}");
                writer.WriteLine();
            }
        }

        Debug.Log($"Transforms exported to: {filePath}");
    }
}