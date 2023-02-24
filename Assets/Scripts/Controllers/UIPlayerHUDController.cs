using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace BloodyMaze.Controllers
{
    public class UIPlayerHUDController : MonoBehaviour
    {
        [SerializeField] private Image m_healthImage;
        [SerializeField] private Image m_manaImage;
        [SerializeField] private TMP_Text m_ammoHoly;
        [SerializeField] private TMP_Text m_ammoSilver;


        public void RefreshHPAndMana(float healthPr, float manaPr)
        {
            m_healthImage.fillAmount = healthPr;
            m_manaImage.fillAmount = manaPr;
        }

        public void RefreshAmmoHoly(in AmmoType ammoType)
        {
            m_ammoHoly.SetText($"{ammoType.currentRoundAmmo}/{ammoType.roundSize}   {ammoType.currentAmmo}");
        }

        public void RefreshAmmoSilver(in AmmoType ammoType)
        {
            m_ammoSilver.SetText($"{ammoType.currentRoundAmmo}/{ammoType.roundSize}   {ammoType.currentAmmo}");
        }
    }
}
