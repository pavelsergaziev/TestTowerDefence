using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tester : MonoBehaviour
{

    [SerializeField] private Transform _testObjectPrefab;
    [SerializeField] private float _speed;
    [SerializeField] private PathBuilder _pathbuilder;//переложить в MAin и выдавать через интерфейс
    [SerializeField] private float _minDistanceToWayPoint;

    private Transform _testObject;
    private Vector3[] _path;
    private int _nextWaypointIndex;
    private float _distanceToNextWaypoint;
    private float _travelDistance;

    // Start is called before the first frame update
    void Start()
    {
        _path = _pathbuilder.GetPath();
        _testObject = Instantiate(_testObjectPrefab, _path[0], Quaternion.identity);
    }

    // Update is called once per frame
    void Update()
    {
        _travelDistance = _speed * Time.deltaTime;
        Vector3 objectPosition = _testObject.position;

        _distanceToNextWaypoint = Vector3.Distance(_testObject.position, _path[_nextWaypointIndex]);

        while (_distanceToNextWaypoint < _minDistanceToWayPoint && _nextWaypointIndex < _path.Length - 1)
        {
            _distanceToNextWaypoint = Vector3.Distance(_testObject.position, _path[++_nextWaypointIndex]);
        }

        float currentDistance = _travelDistance;

        while (currentDistance >= _distanceToNextWaypoint && _nextWaypointIndex < _path.Length - 2)
        {
            currentDistance -= _distanceToNextWaypoint;
            objectPosition = _path[_nextWaypointIndex];
            _distanceToNextWaypoint = Vector3.Distance(_path[_nextWaypointIndex], _path[++_nextWaypointIndex]);
        }

        _testObject.position = Vector3.MoveTowards(objectPosition, _path[_nextWaypointIndex], currentDistance);

    }
}
