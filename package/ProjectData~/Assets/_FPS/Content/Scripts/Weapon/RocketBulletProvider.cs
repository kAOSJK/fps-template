using UnityEngine;

namespace FPS
{
    public class RocketBulletProvider : MonoBehaviour, IBulletProvider
    {
        [SerializeField] private GameObject _rocketPrefab;
        [SerializeField] private Transform _camera;

        public GameObject CreateBullet()
        {
            return Instantiate(_rocketPrefab, _camera.position, _camera.rotation);
        }
    }
}
