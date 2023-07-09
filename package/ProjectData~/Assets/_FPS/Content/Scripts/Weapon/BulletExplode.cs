using UnityEngine;

namespace FPS
{
    public class BulletExplode : MonoBehaviour
    {
        private void OnCollisionEnter(Collision collision)
        {
            Destroy(gameObject);
        }
    }
}
