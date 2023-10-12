using Photon.Pun;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Heyipomoea
{
    /// <summary>
    /// 大廳管理器:連線、連線房間資訊更新
    /// </summary>
    public class LobbyManager : MonoBehaviourPunCallbacks
    {
        #region 資料
        /// <summary>
        /// 畫布連線大廳
        /// </summary>
        private CanvasGroup groupLobby;
        /// <summary>
        /// 房間資訊
        /// </summary>
        private CanvasGroup groupRoom;
        /// <summary>
        /// 輸入欄位玩家名稱
        /// </summary>
        private TMP_InputField inputFieldName;
        /// <summary>
        /// 輸入欄位創建房間名稱 
        /// </summary>
        private TMP_InputField inputFieldCreateRoomName;
        /// <summary>
        /// 輸入欄位加入房間名稱 
        /// </summary>
        private TMP_InputField inputFieldJoinRoomName;
        /// <summary>
        /// 按鈕創建房間
        /// </summary>
        private Button btnCreateRoom;
        /// <summary>
        /// 按紐加入房間 
        /// </summary>
        private Button btnJoinRoom;
        /// <summary>
        /// 按紐加入隨機房間
        /// </summary>
        private Button btnJoinRandomRoom;
        #endregion


        private void Awake()
        {
            GetUIObject();
            PhotonNetwork.ConnectUsingSettings();
        }

        /// <summary>
        /// 取得介面物件
        /// </summary>
        private void GetUIObject()
        {
            groupLobby = GameObject.Find("畫布連線大廳").GetComponent<CanvasGroup>();
            groupRoom = GameObject.Find("房間資訊").GetComponent<CanvasGroup>();
            inputFieldName = GameObject.Find("輸入欄位玩家名稱").GetComponent<TMP_InputField>();
            inputFieldCreateRoomName = GameObject.Find("輸入欄位創建房間名稱").GetComponent<TMP_InputField>();
            inputFieldJoinRoomName = GameObject.Find("輸入欄位加入房間名稱").GetComponent<TMP_InputField>();
            btnCreateRoom = GameObject.Find("按鈕創建房間").GetComponent<Button>();
            btnJoinRoom = GameObject.Find("按紐加入房間").GetComponent<Button>();
            btnJoinRandomRoom = GameObject.Find("按紐加入隨機房間").GetComponent<Button>();
        }

        public override void OnConnectedToMaster()
        {
            base.OnConnectedToMaster();
            print("連線伺服器成功!");
            groupLobby.interactable = true;
        }
    }


}

