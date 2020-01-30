using UnityEngine;

public class PathElementBezier : MonoBehaviour, IEnemyPath
{    
    [SerializeField] private Transform _startingPoint;
    [SerializeField] private Transform _targetPoint;
    [SerializeField] private Transform _bezierPoint;
    [SerializeField] private int _pathWaypointsAmount;

    public Vector3[] GetPath()
    {
        Vector3[] path = new Vector3[_pathWaypointsAmount];

        Vector3 nextPoint = new Vector3();

        for (int i = 0; i < path.Length; i++)
        {
            nextPoint = GetNextPathPoint(i, path.Length);
            path[i] = nextPoint;
        }

        return path;
    }

    private Vector3 GetNextPathPoint(int pointIndex, int totalWaypointsAmount)
    {
        float lerpValue = (float)pointIndex / (totalWaypointsAmount - 1);

        Vector3 firstLerpedPosition = Vector3.Lerp(_startingPoint.position, _bezierPoint.position, lerpValue);
        Vector3 secondLerpedPosition = Vector3.Lerp(_bezierPoint.position, _targetPoint.position, lerpValue);
        return Vector3.Lerp(firstLerpedPosition, secondLerpedPosition, lerpValue);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;

        for (int i = 0; i < _pathWaypointsAmount; i++)
        {
            Gizmos.DrawCube(GetNextPathPoint(i, _pathWaypointsAmount), new Vector3(0.1f, 0.1f, 0.1f));
        }
    }
}
