using UnityEngine;

namespace Heyipomoea
{
    /// <summary>
    /// 面相攝影機
    /// </summary>
    public class LookAtCamera : MonoBehaviour
    {
        private Transform pointCamera;

        private void Awake()
        {
            pointCamera = Camera.main.transform;
        }

        private void Update()
        {
            LookAt();
        }

        private void LookAt()
        {
            transform.LookAt(pointCamera);
        }
    }
}

