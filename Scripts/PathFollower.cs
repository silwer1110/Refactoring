using System.Collections;
using UnityEngine;

public class PathFollower : MonoBehaviour
{
    [SerializeField] private float _speed = 3f;
    [SerializeField] private Transform _waypointTemplate;

    private Transform[] _waypoints;
    private int _currentIndex;

    private void Awake()
    {
        InitWaypoints();
    }

    private void Start()
    {
        StartMovind();
    }

    private void InitWaypoints()
    {
        int count = _waypointTemplate.childCount;

        _waypoints = new Transform[count];

        for (int i = 0; i < count; i++)
            _waypoints[i] = _waypointTemplate.GetChild(i);
    }

    private void StartMovind()
    {
        StartCoroutine(MoveAlongPath());
    }

    private IEnumerator MoveAlongPath()
    {
        Transform target;

        float distanceBetweenPoints = 0.01f;

        while (_waypoints.Length > 0)
        {
            target = _waypoints[_currentIndex];

            while (Vector3.Distance(transform.position, target.position) > distanceBetweenPoints)
            {
                transform.position = Vector3.MoveTowards(transform.position, target.position, _speed * Time.deltaTime);
                yield return null;
            }

            transform.position = target.position;

            _currentIndex = (_currentIndex + 1) % _waypoints.Length;
        }
    }
}