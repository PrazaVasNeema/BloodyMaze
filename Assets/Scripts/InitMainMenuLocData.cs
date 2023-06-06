using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace BloodyMaze
{
    public class InitMainMenuLocData : MonoBehaviour
    {
        [SerializeField] private TMP_Text mainButtonPlay;
        [SerializeField] private TMP_Text mainOptions;
        [SerializeField] private TMP_Text mainExit;
        [SerializeField] private TMP_Text optionsLanguage;
        [SerializeField] private TMP_Text optionsMusic;
        [SerializeField] private TMP_Text optionsSFX;
        [SerializeField] private TMP_Text optionsBack;
        [SerializeField] private TMP_Text optionsApply;
        [SerializeField] private TMP_Text levelSelectBack;
        [SerializeField] private TMP_Text levelSelectSelectLevel;
        [SerializeField] private TMP_Text levelSelectLoad;
        [SerializeField] private TMP_Text levelSelectNew;
        [SerializeField] private TMP_Text saveSlot1_1;
        [SerializeField] private TMP_Text saveSlot1_2;
        [SerializeField] private TMP_Text saveSlot2_1;
        [SerializeField] private TMP_Text saveSlot2_2;
        [SerializeField] private TMP_Text saveSlot3_1;
        [SerializeField] private TMP_Text saveSlot3_2;
        [SerializeField] private TMP_Text[] backButtons;
        [SerializeField] private TMP_Text IntroText;

        [SerializeField] private string mainButtonPlayKey;
        [SerializeField] private string mainOptionsKey;
        [SerializeField] private string mainExitKey;
        [SerializeField] private string optionsLanguageKey;
        [SerializeField] private string optionsMusicKey;
        [SerializeField] private string optionsSFXKey;
        [SerializeField] private string optionsBackKey;
        [SerializeField] private string optionsApplyKey;
        [SerializeField] private string levelSelectBackKey;
        [SerializeField] private string levelSelectLoadKey;
        [SerializeField] private string levelSelectNewKey;
        [SerializeField] private string saveSlot1_1Key;
        [SerializeField] private string saveSlot1_2Key;
        [SerializeField] private string saveSlot2_1Key;
        [SerializeField] private string saveSlot2_2Key;
        [SerializeField] private string saveSlot3_1Key;
        [SerializeField] private string saveSlot3_2Key;
        [SerializeField] private string levelSelectSelectLevelKey;
        [SerializeField] private string IntroTextKey;

        public void SetLocDataMainMenu()
        {
            mainButtonPlay.text = GameController.instance.locData.GetInterfaceText(mainButtonPlayKey);
            mainOptions.text = GameController.instance.locData.GetInterfaceText(mainOptionsKey);
            mainExit.text = GameController.instance.locData.GetInterfaceText(mainExitKey);

            optionsLanguage.text = GameController.instance.locData.GetInterfaceText(optionsLanguageKey);
            optionsMusic.text = GameController.instance.locData.GetInterfaceText(optionsMusicKey);
            optionsSFX.text = GameController.instance.locData.GetInterfaceText(optionsSFXKey);
            optionsBack.text = GameController.instance.locData.GetInterfaceText(optionsBackKey);
            optionsApply.text = GameController.instance.locData.GetInterfaceText(optionsApplyKey);

            levelSelectBack.text = GameController.instance.locData.GetInterfaceText(levelSelectBackKey);
            levelSelectLoad.text = GameController.instance.locData.GetInterfaceText(levelSelectLoadKey);
            levelSelectNew.text = GameController.instance.locData.GetInterfaceText(levelSelectNewKey);
            levelSelectSelectLevel.text = GameController.instance.locData.GetInterfaceText(levelSelectSelectLevelKey);

            foreach (TMP_Text backButton in backButtons)
            {
                backButton.text = GameController.instance.locData.GetInterfaceText(levelSelectBackKey);
            }

            saveSlot1_1.text = $"{GameController.instance.locData.GetInterfaceText(saveSlot1_1Key)} 1";
            saveSlot2_1.text = $"{GameController.instance.locData.GetInterfaceText(saveSlot1_1Key)} 2";
            saveSlot3_1.text = $"{GameController.instance.locData.GetInterfaceText(saveSlot1_1Key)} 3";
            IntroText.text = GameController.instance.locData.GetInterfaceText(IntroTextKey);
        }
    }
}
