using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BloodyMaze.Components;
using UnityEngine.UI;
using TMPro;

namespace BloodyMaze.UI
{
    public class UIPlayerHud : MonoBehaviour
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
        private float m_itemImageOffset = 50f;

        private void Awake()
        {
            m_refImage = m_showcase.GetComponentInChildren<Image>().gameObject;
            m_UIPanel = m_manaImage.transform.parent.transform.parent.gameObject;
            m_inventoryItemsInShowcase.Add(m_refImage);
            m_refImage.SetActive(false);
        }

        public void Init(CharacterComponent characterComponent)
        {
            m_characterComponent = characterComponent;
            m_characterComponent.ammunitionComponent.onAmmoCountChange += RefreshAmmoCount;
            m_characterComponent.abilitiesManagerSlot1.onAbilityChange += ChangeRevolverStatsFocus;
            GameInventory.current.onInventoryChange += ReorganizeShowcase;
            m_characterComponent.ammunitionComponent.Reload("holy");
            m_characterComponent.ammunitionComponent.Reload("silver");
        }

        private void OnDestroy()
        {
            m_characterComponent.ammunitionComponent.onAmmoCountChange -= RefreshAmmoCount;
            m_characterComponent.abilitiesManagerSlot1.onAbilityChange -= ChangeRevolverStatsFocus;
            // GameInventory.current.onInventoryChange -= ReorganizeShowcase;
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
                tempItem.SetActive(true);
                PickableItemComponent tempComponent = tempItem.AddComponent<PickableItemComponent>();
                tempComponent.SetItem(item);
                tempItem.GetComponent<Image>().sprite = tempComponent.item.displaySprite;
                if (m_inventoryItemsInShowcase[m_inventoryItemsInShowcase.Count - 1] == m_refImage)
                {
                    tempItem.transform.position = m_inventoryItemsInShowcase[m_inventoryItemsInShowcase.Count - 1].transform.position;
                }
                else
                {
                    tempItem.transform.position = m_inventoryItemsInShowcase[m_inventoryItemsInShowcase.Count - 1].transform.position;
                    tempItem.transform.position = new Vector2(tempItem.transform.position.x, tempItem.transform.position.y - m_itemImageOffset);
                }
                m_inventoryItemsInShowcase.Add(tempItem);
            }
            else
            {
                for (int i = 1; i < m_inventoryItemsInShowcase.Count; i++)
                {
                    if (m_inventoryItemsInShowcase[i].GetComponent<PickableItemComponent>().item.name == name)
                    {
                        var temp = m_inventoryItemsInShowcase[i];
                        m_inventoryItemsInShowcase.Remove(m_inventoryItemsInShowcase[i]);
                        Destroy(temp);
                    }
                    if (i < m_inventoryItemsInShowcase.Count)
                        m_inventoryItemsInShowcase[i].transform.position = new Vector2(m_inventoryItemsInShowcase[i - 1].transform.position.x,
                         m_inventoryItemsInShowcase[i - 1].transform.position.y - (i == 1 ? 0 : m_itemImageOffset));
                }
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
    }
}
