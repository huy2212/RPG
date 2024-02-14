using UnityEditor;
using UnityEngine;
using UnityEngine.AI;

public class ClearNavMesh : MonoBehaviour
{
    [MenuItem("Light Brigade/Debug/Force Cleanup NavMesh")]
    public static void ForceCleanupNavMesh()
    {
        if (Application.isPlaying)
            return;

        NavMesh.RemoveAllNavMeshData();
    }
}