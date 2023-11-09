using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using System.Collections;
using StarterAssets;
using UnityEngine.InputSystem;

namespace Heyipomoea
{
    /// <summary>
    /// 受傷系統
    /// </summary>
    public class DamageSystem : MonoBehaviourPun
    {
        private float hp = 100;
        private float maxHp = 100;

        [SerializeField, Header("血條")]
        private Image imgHp;
        [SerializeField, Header("血量文字")]
        private TextMeshProUGUI textHp;
        [SerializeField, Header("爆炸特效")]
        private GameObject objectExplosion;
        //[SerializeField, Header("PlayerCapsule")]
        //private GameObject playerCapsule;
        [SerializeField, Header("開槍系統")]
        private FireSystem fireSystem;
        [SerializeField, Header("結束畫面")]
        private CanvasGroup groupFinal;
        [SerializeField, Header("第一人稱控制器")]
        private FirstPersonController firstPersonController;
        [SerializeField, Header("玩家輸入")]
        private PlayerInput playerInput;
        [SerializeField, Header("角色控制器")]
        private CharacterController characterController;
        [SerializeField, Header("膠囊碰撞器")]
        private CapsuleCollider capsuleCollider;
        [SerializeField, Header("返回大廳")]
        private Button btnReturnLobby;
        [SerializeField, Header("輸入資源")]
        private StarterAssetsInputs starterAssetsInputs;
        [SerializeField, Header("骷髏")]
        private GameObject objectSkull;


        private string bulletName = "子彈";

        private void Awake()
        {
            btnReturnLobby.onClick.AddListener(ReturnToLobby);
        }

        /// <summary>
        /// 返回大廳
        /// </summary>
        private void ReturnToLobby()
        {
            print("點擊返回大廳");
            PhotonNetwork.LeaveRoom();
            PhotonNetwork.LoadLevel("連線大廳");
        }

        /// <summary>
        /// 受傷
        /// </summary>
        /// <param name="damage">獲得的傷害</param>
        private void Damage(float damage)
        {
            hp -= damage;
            imgHp.fillAmount = hp / maxHp;
            textHp.text = $"{hp} / {maxHp}";

            if (hp <= 0) Dead();
        }

        private void Dead()
        {
            GameObject tempExplosion = Instantiate(objectExplosion, transform.position, Quaternion.Euler(-90, 0, 0));
            Destroy(tempExplosion, 3);
            //playerCapsule.SetActive(false);
            fireSystem.enabled = false;
            firstPersonController.enabled = false;
            playerInput.enabled = false;
            StartCoroutine(FadeInFinal());
            photonView.RPC("RPCDead", RpcTarget.All);
        }

        [PunRPC]
        private void RPCDead()
        {
            objectSkull.SetActive(false);
            characterController.enabled = false;
            capsuleCollider.enabled = false;
        }

        private IEnumerator FadeInFinal()
        {
            for(int i = 0; i < 10; i++)
            {
                groupFinal.alpha += 0.1f;
                yield return new WaitForSeconds(0.02f);
            }

            groupFinal.interactable = true;
            groupFinal.blocksRaycasts = true;
            Cursor.lockState = CursorLockMode.Confined;
            starterAssetsInputs.cursorLocked = false;
        }

        private void OnCollisionEnter(Collision collision)
        {
            if(collision.gameObject.name.Contains(bulletName))
            {
                float bulletDamage = collision.gameObject.GetComponent<Bullet>().bulletDamage;
                Damage(bulletDamage);
            }
        }
    }
}

