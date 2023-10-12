using Photon.Pun;
using Photon.Realtime;
using System;
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


        private string namePlayer;
        private string nameCreateRoom;
        private string nameJoinRoom;
        /// <summary>
        /// 文字房間名稱
        /// </summary>
        private TextMeshProUGUI textRoomName;
        /// <summary>
        /// 文字房間人數
        /// </summary>
        private TextMeshProUGUI textRoomPersonCount;
        /// <summary>
        /// 按鈕開始遊戲
        /// </summary>
        private Button btnStartGame;

        private void Awake()
        {
            GetUIObject();
            PhotonNetwork.ConnectUsingSettings();
            GetInputFieldData();
            ButtonClickSetting();
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

            textRoomName = GameObject.Find("文字房間名稱").GetComponent<TextMeshProUGUI>();
            textRoomPersonCount = GameObject.Find("文字房間人數").GetComponent<TextMeshProUGUI>();
            btnStartGame = GameObject.Find("按紐開始遊戲").GetComponent<Button>();
        }


        /// <summary>
        /// 取得輸入欄位資料
        /// </summary>
        private void GetInputFieldData()
        {
            inputFieldName.onEndEdit.AddListener((input) => namePlayer = input);
            inputFieldCreateRoomName.onEndEdit.AddListener((input) => nameCreateRoom = input);
            inputFieldJoinRoomName.onEndEdit.AddListener((input) => nameJoinRoom = input);
        }

        /// <summary>
        /// 設定按鈕點擊
        /// </summary>
        private void ButtonClickSetting()
        {
            btnCreateRoom.onClick.AddListener(BtnCreateRoom);
            btnJoinRoom.onClick.AddListener(BtnJoinRoom);
            btnJoinRandomRoom.onClick.AddListener(BtnJoinRandomRoom);

            btnStartGame.onClick.AddListener(() => photonView.RPC("RPCLoadGameScene", RpcTarget.All));
        }

        private void BtnCreateRoom()
        {
            RoomOptions roomOptions = new RoomOptions();
            roomOptions.MaxPlayers = 20;
            PhotonNetwork.CreateRoom(nameCreateRoom, roomOptions);
            btnStartGame.interactable = true;
        }

        private void BtnJoinRoom()
        {
            PhotonNetwork.JoinRoom(nameJoinRoom);
        }

        private void BtnJoinRandomRoom()
        {
            PhotonNetwork.JoinRandomRoom();
        }

        /// <summary>
        /// 更新房間介面
        /// </summary>
        private void UpdateRoomUI()
        {
            groupRoom.alpha = 1;
            groupRoom.interactable = true;
            groupRoom.blocksRaycasts = true;

            textRoomName.text = $"房間名稱: {PhotonNetwork.CurrentRoom.Name}"; 
            textRoomPersonCount.text = $"遊戲人數: {PhotonNetwork.CurrentRoom.PlayerCount} / {PhotonNetwork.CurrentRoom.MaxPlayers}";
        }

        [PunRPC]
        private void RPCLoadGameScene()
        {
            Debug.LogError("開始遊戲");
            PhotonNetwork.LoadLevel("遊戲場景");
        }

        public override void OnConnectedToMaster()
        {
            base.OnConnectedToMaster();
            print("連線伺服器成功!");
            groupLobby.interactable = true;
        }

        public override void OnCreatedRoom()
        {
            base.OnCreatedRoom();
            print("創建房間成功!");
            UpdateRoomUI();
        }

        public override void OnJoinedRoom()
        {
            base.OnJoinedRoom();
            print("加入房間成功!");
            UpdateRoomUI();
        }

        public override void OnPlayerEnteredRoom(Player newPlayer)
        {
            base.OnPlayerEnteredRoom(newPlayer);
            UpdateRoomUI();
        }
    }
}

