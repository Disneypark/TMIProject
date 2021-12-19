﻿#if UNITY_EDITOR
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace Michsky.UI.ModernUIPack
{
    [ExecuteInEditMode]
    public class UIManagerHSelector : MonoBehaviour
    {
        [Header("SETTINGS")]
        public UIManager UIManagerAsset;

        [Header("RESOURCES")]
        public List<GameObject> images = new List<GameObject>();
        public List<GameObject> imagesHighlighted = new List<GameObject>();
        public List<GameObject> texts = new List<GameObject>();
        HorizontalSelector hSelector;
        public OctaveChangeManager octaveChangeManager;

        private int num;

        void Awake()
        {
            try
            {
                if (hSelector == null)
                    hSelector = gameObject.GetComponent<HorizontalSelector>();

                if (UIManagerAsset == null)
                    UIManagerAsset = Resources.Load<UIManager>("MUIP Manager");

                this.enabled = true;

                if (UIManagerAsset.enableDynamicUpdate == false)
                {
                    UpdateSelector();
                    this.enabled = false;
                }
            }

            catch
            {
                Debug.Log("<b>[Modern UI Pack]</b> No UI Manager found, assign it manually.", this);
            }
            
            num = GetComponent<HorizontalSelector>().index;
        }

        void LateUpdate()
        {
            if (UIManagerAsset == null)
                return;

            if (UIManagerAsset.enableDynamicUpdate == true)
                UpdateSelector();
        }

        void UpdateSelector()
        {
            for (int i = 0; i < images.Count; ++i)
            {
                Image currentImage = images[i].GetComponent<Image>();
                currentImage.color = new Color(UIManagerAsset.selectorColor.r, UIManagerAsset.selectorColor.g, UIManagerAsset.selectorColor.b, currentImage.color.a);
            }

            for (int i = 0; i < imagesHighlighted.Count; ++i)
            {
                Image currentAlphaImage = imagesHighlighted[i].GetComponent<Image>();
                currentAlphaImage.color = new Color(UIManagerAsset.selectorHighlightedColor.r, UIManagerAsset.selectorHighlightedColor.g, UIManagerAsset.selectorHighlightedColor.b, currentAlphaImage.color.a);
            }

            for (int i = 0; i < texts.Count; ++i)
            {
                TextMeshProUGUI currentText = texts[i].GetComponent<TextMeshProUGUI>();
                currentText.color = new Color(UIManagerAsset.selectorColor.r, UIManagerAsset.selectorColor.g, UIManagerAsset.selectorColor.b, currentText.color.a);
                currentText.font = UIManagerAsset.selectorFont;
                currentText.fontSize = UIManagerAsset.hSelectorFontSize;
            }

            if (hSelector != null)
            {
                hSelector.invertAnimation = UIManagerAsset.hSelectorInvertAnimation;
                hSelector.loopSelection = UIManagerAsset.hSelectorLoopSelection;
            }
        }
        
        public void PreviousEvent()
        {
            num--;
            if (num < 0)
            {
                num = 0;
                return;
            }
            octaveChangeManager.OnMinusOctave();
        }
        
        public void ForwardEvent()
        {
            num++;
            if (3 < num)
            {
                num = 3;
                return;
            }
            octaveChangeManager.OnPlusOctave();
        }
    }
}
#endif