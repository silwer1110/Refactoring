using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Shooter : MonoBehaviour
{
    [SerializeField] private Bullet _bullet;
    [SerializeField] private Transform _target;
    [SerializeField] private float _bulletSpeed = 10f;
    [SerializeField] private float _shootInterval = 1f;

    private void Start()
    {
        StartCoroutine(Shoot());
    }

    private IEnumerator Shoot()
    {
        WaitForSeconds wait = new(_shootInterval);

        Vector3 direction;

        while (enabled)
        {
            Bullet bullet;

            direction = (_target.position - transform.position).normalized;

            bullet = CreateBullet(direction);

            bullet.Rigidbody.velocity = direction * _bulletSpeed;

            yield return wait;
        }
    }

    private Bullet CreateBullet(Vector3 direction)
    {
        Vector3 spawnPosition = transform.position + direction;

        return Instantiate(_bullet, spawnPosition, Quaternion.LookRotation(direction));
    }
}