using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using Events;
using Player.ActionHandlers;
using UnityEngine;


namespace Camera
{
    public class CameraMover : MonoBehaviour
    {
        [SerializeField] private Transform followTarget;
        
        private ClickHandler _clickHandler;
        private bool _isDrag = false;
        private bool _isNodeTapped = false;
        private Vector3 InitialPosition = Vector3.zero;

        void Awake()
        {
            _clickHandler = ClickHandler.Instance;
        }

        void OnEnable()
        {
            _clickHandler.PointerDownEvent += OnDragStartEvent;
            _clickHandler.PointerUpEvent += OnDragEndEvent;

            EventsController.Subscribe<EventModels.Game.NodeTapped>(this, OnNodeTapped);
        }

        void OnDisable()
        {
            _clickHandler.PointerDownEvent -= OnDragStartEvent;
            _clickHandler.PointerUpEvent -= OnDragEndEvent;

            EventsController.Unsubscribe<EventModels.Game.NodeTapped>(OnNodeTapped);
        }

        void Update()
        {
            if (!_isDrag)
                return;

            if (_isNodeTapped)
                return;

            var secondPoint = CameraHolder.Instance.MainCamera.ScreenToWorldPoint(Input.mousePosition);
            var difference = InitialPosition - secondPoint;
            followTarget.position += difference;
            InitialPosition = secondPoint;
        }

        private void OnNodeTapped(EventModels.Game.NodeTapped tapped)
        {
            _isNodeTapped = true;
        }

        private void OnDragStartEvent(Vector3 vector)
        {
            _isDrag = true;

            InitialPosition = vector;
        }

        private void OnDragEndEvent(Vector3 vector)
        {
            _isDrag = false;
            _isNodeTapped = false;
            InitialPosition = Vector3.zero;
        }
    }
}