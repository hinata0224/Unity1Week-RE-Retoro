using UnityEngine;
using PackMan_Item;

namespace PackMan_Player
{
    public class TargetSerch : MonoBehaviour
    {
        [SerializeField]
        PlayerController controller;

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag("Enemy"))
            {
                controller.HitDamage();
            }
            if (other.gameObject.CompareTag("Item"))
            {
                ItemController.GetItem();
                Destroy(other.gameObject);
            }
        }
    }
}
