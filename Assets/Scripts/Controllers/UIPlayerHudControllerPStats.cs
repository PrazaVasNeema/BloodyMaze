using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BloodyMaze.Components;
using UnityEngine.UI;
using TMPro;

namespace BloodyMaze.Controllers
{
    public class UIPlayerHUDControllerPStats : MonoBehaviour
    {
        [SerializeField] private Image m_healthImage;
        [SerializeField] private Image m_manaImage;
        [SerializeField] private TMP_Text m_ammoHoly;
        [SerializeField] private TMP_Text m_ammoSilver;
        [SerializeField] private GameObject m_showcase;

        private GameObject m_UIPanel;
        private CharacterComponent m_characterComponent;
        private GameObject m_refImage;
        private List<GameObject> m_inventoryItemsInShowcase = new List<GameObject>();
        private float m_itemImageOffset = 30f;

        private void Awake()
        {
            m_refImage = m_showcase.GetComponentInChildren<Image>().gameObject;
            m_UIPanel = m_manaImage.transform.parent.transform.parent.gameObject;
        }

        public void Init(CharacterComponent characterComponent)
        {
            m_characterComponent = characterComponent;
            m_characterComponent.ammunitionComponent.onAmmoCountChange += RefreshAmmoCount;
            m_characterComponent.abilitiesManagerSlot1.onAbilityChange += ChangeRevolverStatsFocus;
            GameEvents.OnSetInteractState += ChangeHUDVisibilityState;
            GameInventory.current.onInventoryChange += ReorganizeShowcase;
            m_characterComponent.ammunitionComponent.Reload("holy");
            m_characterComponent.ammunitionComponent.Reload("silver");
        }

        private void OnDestroy()
        {
            m_characterComponent.ammunitionComponent.onAmmoCountChange -= RefreshAmmoCount;
            m_characterComponent.abilitiesManagerSlot1.onAbilityChange -= ChangeRevolverStatsFocus;
            GameEvents.OnSetInteractState -= ChangeHUDVisibilityState;
            GameInventory.current.onInventoryChange -= ReorganizeShowcase;
        }

        private void Update()
        {
            if (Time.frameCount % 10 == 0)
            {
                RefreshHPAndMana(m_characterComponent.healthComponent.percent,
                                    m_characterComponent.manaComponent.percent);
            }
        }

        private void ReorganizeShowcase(string name, PickableItem item)
        {
            if (item != null)
            {
                GameObject tempItem = Instantiate(m_refImage, m_showcase.transform);
                PickableItemComponent tempComponent = tempItem.AddComponent<PickableItemComponent>();
                tempComponent.SetItem(item);
                tempItem.transform.position = m_inventoryItemsInShowcase[m_inventoryItemsInShowcase.Count - 1].transform.position;
                tempItem.transform.position = new Vector2(tempItem.transform.position.x, tempItem.transform.position.x + m_itemImageOffset);

            }
        }

        public void RefreshHPAndMana(float healthPr, float manaPr)
        {
            m_healthImage.fillAmount = healthPr;
            m_manaImage.fillAmount = manaPr;
        }

        private void RefreshAmmoCount(string ammoTypeName, AmmoType ammoType)
        {
            switch (ammoTypeName)
            {
                case "holy":
                    m_ammoHoly.SetText($"{ammoType.currentRoundAmmo}/{ammoType.roundSize}   {ammoType.currentAmmo}");
                    break;
                case "silver":
                    m_ammoSilver.SetText($"{ammoType.currentRoundAmmo}/{ammoType.roundSize}   {ammoType.currentAmmo}");
                    break;
            }
        }

        public void ChangeRevolverStatsFocus(int currentRevolverIndex)
        {
            Transform ammoHolyTransform = m_ammoHoly.transform.parent.transform.parent.GetComponent<Transform>();
            Transform ammoSilverTransform = m_ammoSilver.transform.parent.transform.parent.GetComponent<Transform>();
            Vector3 scaleOne = new Vector3(0.8f, 0.8f, 0.8f);
            Vector3 scaleTwo = new Vector3(1f, 1f, 1f);
            switch (currentRevolverIndex)
            {
                case 0:
                    ammoHolyTransform.localScale = scaleTwo;
                    ammoSilverTransform.localScale = scaleOne;
                    break;
                case 1:
                    ammoHolyTransform.localScale = scaleOne;
                    ammoSilverTransform.localScale = scaleTwo;
                    break;
            }
        }

        private void ChangeHUDVisibilityState()
        {
            m_UIPanel.SetActive(!m_UIPanel.activeSelf);
        }
    }
}
