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
        [Header("PC Stats")]
        [SerializeField] private Image m_healthImage;
        [SerializeField] private Image m_manaImage;
        [SerializeField] private TMP_Text m_ammoHoly;
        [Header("Mini message")]
        [SerializeField] private TMP_Text m_miniMessage;
        [SerializeField] private float m_miniMessageDisplayTime = 3f;
        [Header("Other")]
        [SerializeField] private GameObject m_showcase;


        private GameObject m_UIPanel;
        private CharacterComponent m_characterComponent;
        private GameObject m_refImage;
        private List<GameObject> m_inventoryItemsInShowcase = new List<GameObject>();
        private float m_itemImageOffset = 50f;
        private Animator m_animator;

        private void OnEnable()
        {
            m_UIPanel = m_manaImage.transform.parent.transform.parent.gameObject;
            m_inventoryItemsInShowcase.Add(m_refImage);
            GameEvents.OnShowMiniMessage += ShowMiniMessage;
        }

        private void OnDisable()
        {
            m_characterComponent.ammunitionComponent.onAmmoCountChange -= RefreshAmmoCount;
            GameEvents.OnShowMiniMessage -= ShowMiniMessage;
        }

        public void Init(CharacterComponent characterComponent)
        {
            m_characterComponent = characterComponent;
            m_characterComponent.ammunitionComponent.onAmmoCountChange += RefreshAmmoCount;
            GameInventory.current.onInventoryChange += ReorganizeShowcase;
            m_characterComponent.ammunitionComponent.Reload("holy");
            m_refImage = m_showcase.GetComponentInChildren<Image>().gameObject;
            m_animator = GetComponent<Animator>();
            m_refImage.SetActive(false);
        }

        private void OnDestroy()
        {
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
                Debug.Log("ReorganizeShowcase");
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
            m_ammoHoly.SetText($"{ammoType.currentRoundAmmo}/{ammoType.roundSize}   {ammoType.currentAmmo}");
        }

        private void ShowMiniMessage(string miniMessageKey)
        {
            m_miniMessage.text = GameController.locData.GetMiniMessage(miniMessageKey);
            m_animator.SetBool("MiniMessageShouldBeShown", true);
            StopCoroutine(HideMiniMessage());
            StartCoroutine(HideMiniMessage());
        }

        IEnumerator HideMiniMessage()
        {
            bool doOnce = true;
            while (doOnce)
            {
                doOnce = false;
                yield return new WaitForSecondsRealtime(m_miniMessageDisplayTime);
            }
            m_animator.SetBool("MiniMessageShouldBeShown", false);
        }
    }
}
