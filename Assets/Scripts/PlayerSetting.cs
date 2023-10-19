using Photon.Pun;
using StarterAssets;
using TMPro;
using UnityEngine;


namespace Heyipomoea
{
    /// <summary>
    /// 玩家設定:攝影機與控制器
    /// </summary>
    public class PlayerSetting : MonoBehaviourPunCallbacks
    {
        [SerializeField, Header("攝影機物件")]
        private GameObject objectCamera;
        [SerializeField, Header("攝影機跟隨物件")]
        private GameObject objectCameraFollow;
        [SerializeField, Header("第一人稱控制器")]
        private FirstPersonController firstPersonController;
        [SerializeField, Header("玩家名稱")]
        private TextMeshProUGUI textPlayerName;
        [SerializeField, Header("骷髏頭模型")]
        private GameObject objectSkullHead;

        private string pointName = "生成點";

        private void Awake()
        {
            SettingPosition();
            if (!photonView.IsMine)
            {
                objectCamera.SetActive(false);
                objectCameraFollow.SetActive(false);
                firstPersonController.enabled = false;
            }
            else if(photonView.IsMine)
            {
                objectSkullHead.layer = 3;
            }
            textPlayerName.text = photonView.Owner.NickName;
        }

        /// <summary>
        /// 設定玩家座標
        /// </summary>
        private void SettingPosition()
        {
            int id = photonView.ViewID / 1000;
            Vector3 point = GameObject.Find(pointName+id).transform.position;
            transform.position = point;
        }
    }
}

