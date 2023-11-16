using UnityEngine;
using PackMan_Item;

namespace PackMan_Player
{
    public class TargetSerch : MonoBehaviour
    {
        private PlayerController _playerController;
        private ItemController _itemController;

        private void Awake()
        {
            _playerController = GetComponent<PlayerController>();
            _itemController = GameObject.FindGameObjectWithTag(TagName.ItemController).GetComponent<ItemController>();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag(TagName.Enemy))
            {
                _playerController.HitDamage();
            }
            if (other.gameObject.CompareTag(TagName.Item))
            {
                _itemController.GetItem();
                Destroy(other.gameObject);
            }
        }
    }
}
