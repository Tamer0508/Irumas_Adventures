using NavMeshPlus.Components;
using UnityEngine;

public class NavMeshSurfaceManagment : MonoBehaviour
{
    public static NavMeshSurfaceManagment Instance { get; private set; }

    private NavMeshSurface _navmeshSurface;

    private void Awake()
    {
        Instance = this;
        _navmeshSurface = GetComponent<NavMeshSurface>();
        _navmeshSurface.hideEditorLogs = true;
    }

    public void RebakeNavmeshSurface()
    {
        _navmeshSurface.BuildNavMesh();
    }
}
