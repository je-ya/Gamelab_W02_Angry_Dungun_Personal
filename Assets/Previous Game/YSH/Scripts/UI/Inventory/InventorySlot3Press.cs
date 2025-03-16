using UnityEngine;
using UnityEngine.UI;

public class InventorySlot3Press : MonoBehaviour
{
    [SerializeField] Inventory_IsEmpty _itemSlotItemID; // ������ ���̵� �������� ����
    Aazz0200_Act _bulletShootUp;
    Aazz0200_Player _playerSpeed;
    UI_PlayerHp _playerHpSlider;
    Aazz0200_Life _playerHp;
    ItemCount _itemCount;
    int _itemID;

    private void Awake()
    {
        _playerHp = GameObject.Find("Player").GetComponent<Aazz0200_Life>();
        _bulletShootUp = GameObject.Find("Act_��").GetComponent<Aazz0200_Act>();
        _playerSpeed = GameObject.FindAnyObjectByType<Aazz0200_Player>();
        _playerHpSlider = GameObject.FindAnyObjectByType<UI_PlayerHp>();
        _itemCount = GetComponent<ItemCount>();
        _itemID = _itemSlotItemID.ItemID;
    }

    private void Update()
    {
        _itemID = _itemSlotItemID.ItemID;
        //Debug.Log(_itemID);

        if (Input.GetButtonDown("ItemUse3"))
        {
            if (_itemCount.ItemCounts == 0)
            {
                return;
            }
            else if (_itemCount.ItemCounts != 1)
            {
                _itemCount.ItemCounts--;
            }
            else if (_itemCount.ItemCounts == 1)
            {
                _itemSlotItemID.GetComponent<Image>().sprite = default;
                _itemCount.ItemCounts--;
                _itemSlotItemID.IsEmpty = true;

            }
            // ������ ��� ȿ�� ������ �ɵ�?

            switch (_itemID)
            {
                case 11: // ���ǵ� ��
                    _playerSpeed.PlayerSpeed = ItemDataBase.ItemData[11]._speedUp;
                    break;
                case 12: // ���� �ӵ� ��
                    _bulletShootUp.Col_Max = ItemDataBase.ItemData[12]._shootSpeedUp;
                    break;
                case 13: // ���� 
                    _playerHpSlider.GetComponent<Slider>().value += ItemDataBase.ItemData[13].healing;
                    _playerHp.now += ItemDataBase.ItemData[13].healing * 100;
                    Debug.Log("���� ��");
                    break;

                default:
                    break;
            }
        }

    }
}
