using UnityEngine;

namespace Project.Runtime.Scripts
{
    public class MapScroller : MonoBehaviour
    {
        public float Speed = 5f;
        public float DestroyZ = -20f;

        private void Update()
        {
            transform.Translate(Vector3.back * Speed * Time.deltaTime);

            if (transform.position.z < DestroyZ)
                Destroy(gameObject);
        }
    }
}
