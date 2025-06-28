﻿
    using System;
    using UnityEngine;
    using UnityEngine.UI;

    public class MainMenuUI : MonoBehaviour
    {
        [SerializeField] private Button playButton;
        [SerializeField] private Button quitButton;

        private void Awake()
        {
            playButton.onClick.AddListener(() =>
            {
                Loader.LoadScene(Loader.Scene.Game);
            });
            
            quitButton.onClick.AddListener(() =>
            {
                Application.Quit();
            });
        }
    }
