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
        [SerializeField] private TMP_Text m_ammoAllRemainingAmmo;
        [SerializeField] private TMP_Text m_drumAmmo;
        [SerializeField] private TMP_Text m_medsCommon;
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
        private float m_reloadingValue = 0f;
        [SerializeField] private Image m_reloadingButtonImage;
        [SerializeField] private Button m_medsButton;
        [SerializeField] private Button m_reloadButton;

        private void OnEnable()
        {
            m_UIPanel = m_manaImage.transform.parent.transform.parent.gameObject;
            GameEvents.OnShowMiniMessage += ShowMiniMessage;
            if (m_characterComponent)
            {
                m_characterComponent.medsComponent.OnMedsCountChange += RefreshMedsCount;
                m_characterComponent.ammunitionComponent.onAmmoCountChange += RefreshAmmoCount;
            }
            GameEvents.OnReload += OnReload;
        }

        private void OnDisable()
        {
            GameEvents.OnShowMiniMessage -= ShowMiniMessage;
            m_characterComponent.medsComponent.OnMedsCountChange -= RefreshMedsCount;
            m_characterComponent.ammunitionComponent.onAmmoCountChange -= RefreshAmmoCount;
            GameEvents.OnReload -= OnReload;
        }

        public void Init(CharacterComponent characterComponent)
        {
            m_characterComponent = characterComponent;
            GameInventory.current.onInventoryChange += ReorganizeShowcase;
            m_refImage = m_showcase.GetComponentInChildren<Image>().gameObject;
            m_inventoryItemsInShowcase.Add(m_refImage);
            m_animator = GetComponent<Animator>();
            m_refImage.SetActive(false);
            m_characterComponent.medsComponent.OnMedsCountChange += RefreshMedsCount;
            m_characterComponent.ammunitionComponent.onAmmoCountChange += RefreshAmmoCount;
            m_characterComponent.ammunitionComponent.Reload("holy");
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
                m_reloadingButtonImage.fillAmount = m_reloadingValue;
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
                Debug.Log(tempComponent.name);
                tempComponent.SetItem(item);
                Debug.Log(tempComponent.item.name);
                tempItem.GetComponent<Image>().sprite = tempComponent.item.displaySprite;
                if (m_inventoryItemsInShowcase[m_inventoryItemsInShowcase.Count - 1] == m_refImage)
                {
                    Debug.Log("ghfgh");
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
            m_manaImage.fillAmount = 1 - manaPr;
        }

        private void RefreshAmmoCount(string ammoTypeName, AmmoType ammoType)
        {
            m_ammoAllRemainingAmmo.SetText(ammoType.currentAmmo.ToString());
            m_drumAmmo.SetText($"{ammoType.currentRoundAmmo}/{ammoType.roundSize}");
            if (ammoType.currentRoundAmmo != ammoType.roundSize)
            {
                m_reloadButton.gameObject.SetActive(true);
                if (ammoType.currentAmmo == 0)
                    m_reloadButton.interactable = false;
                else
                    m_reloadButton.interactable = true;
            }
            else
                m_reloadButton.gameObject.SetActive(false);
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

        private void RefreshMedsCount(MedsType medsCommon)
        {
            m_medsCommon.text = medsCommon.currentAmount + "/" + medsCommon.maxAmount;
            if (medsCommon.currentAmount == 0)
                m_medsButton.interactable = false;
            else
                m_medsButton.interactable = true;
        }

        private void OnReload()
        {
            StartCoroutine(IncreaseValue());
        }

        IEnumerator IncreaseValue()
        {
            m_reloadingValue = 1f;
            while (m_reloadingValue != 0f)
            {
                m_reloadingValue -= 0.5f * Time.deltaTime;
                yield return new();
            }
        }
    }
}
