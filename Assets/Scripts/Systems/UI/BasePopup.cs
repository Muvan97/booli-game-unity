using System;
using UnityEngine;
using UnityEngine.UI;

namespace Systems.UI
{
    public abstract class BasePopup : MonoBehaviour
    {
        public Action Opened, Closed;

        [Header("Open/Close Settings")]
        
        [SerializeField] protected Button closeButton;

        [SerializeField] private Button _openButton;
        [SerializeField] private bool _isOpen;

        public bool IsOpen => _isOpen;
        
        protected void Awake()
        {
            _openButton?.onClick.AddListener(OpenPopup);
            closeButton?.onClick.AddListener(ClosePopup);

            OnInitialization();
        
            gameObject.SetActive(_isOpen);
        }

        public void SetOpenState()
        {
            if (IsOpen)
                ClosePopup();
            else
                OpenPopup();
        }

        public void OpenPopup()
        {
            if (_isOpen) return;
            
            _isOpen = true; 
            gameObject.SetActive(true);
            
            Opened?.Invoke();
            OnOpenPopup();
        }

        public void ClosePopup()
        {
            if (!_isOpen) return;
            
            _isOpen = false;
            gameObject.SetActive(false);

            Closed?.Invoke();
            OnClosePopup();
        }

        #region Callbacks

        protected virtual void OnInitialization()
        {
        }

        protected virtual void OnOpenPopup()
        {
        }

        protected virtual void OnClosePopup()
        {
        }

        #endregion Callbacks
    }
}