using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class PlayerWindow : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private GameObject _playerWindow;
    [SerializeField] private PlayerInventoryUI _playerInventoryUI;
    [SerializeField] private PlayerEquipmentUI _playerEquipmentUI;

    public bool PointerOverWindow { get; private set; }
    public PlayerInventoryUI PlayerInventoryUI => _playerInventoryUI;
    public PlayerEquipmentUI PlayerEquipmentUI => _playerEquipmentUI;
    void Start()
    {
        _playerWindow.gameObject.SetActive(false);
        Service_Manager.instance.InputController.CharacterWindowClicked += OnWindowClicked;
    }

    public void InitComponents()
    {
        _playerEquipmentUI.InitComponents();
        _playerInventoryUI.InitComponents();
        
    }

    public void OnWindowClicked()
    {
        _playerWindow.SetActive(!_playerWindow.activeInHierarchy);
    }
    

    private void OnDestroy()
    {
        Service_Manager.instance.InputController.CharacterWindowClicked -= OnWindowClicked;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        PointerOverWindow = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        PointerOverWindow = false;
    }
}
