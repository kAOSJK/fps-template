using UnityEngine;

namespace FPS
{
    [RequireComponent(typeof(IBulletProvider))]
    public class PlayerShoot : MonoBehaviour
    {
        private IBulletProvider _bulletProvider;

        private void Awake()
        {
            _bulletProvider = GetComponent<IBulletProvider>();
        }

        private void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                _bulletProvider.CreateBullet();
            }
        }
    }
}
