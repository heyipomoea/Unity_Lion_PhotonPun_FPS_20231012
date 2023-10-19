using Photon.Pun;
using UnityEngine;

namespace Heyipomoea
{
    /// <summary>
    /// 生成玩家系統
    /// </summary>
    public class SpawnPlayerSystem : MonoBehaviourPunCallbacks
    {
        [SerializeField, Header("玩家控制物件")]
        private GameObject prefabPlayer;

        private void Awake()
        {
            PhotonNetwork.Instantiate(prefabPlayer.name, Vector3.zero, Quaternion.identity);
        }
    }
}

