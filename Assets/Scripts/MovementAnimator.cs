using Photon.Pun;
using StarterAssets;
using UnityEngine;


namespace Heyipomoea
{
    /// <summary>
    /// 移動動畫
    /// </summary>
    public class MovementAnimator : MonoBehaviourPun
    {
        [SerializeField, Header("角色控制器")]
        private CharacterController characterController;
        [SerializeField, Header("第一人稱控制器")]
        private FirstPersonController firstPersonController;

        private Animator ani;
        private string parMove = "移動";

        private void Awake()
        {
            if (!photonView.IsMine) enabled = false;

            ani = GetComponent<Animator>();
        }

        private void Update()
        {
            UpdateAnimator();
        }

        /// <summary>
        /// 更新動畫
        /// </summary>
        private void UpdateAnimator()
        {
            ani.SetFloat(parMove, characterController.velocity.magnitude / firstPersonController.SprintSpeed);
        }
    }
}

