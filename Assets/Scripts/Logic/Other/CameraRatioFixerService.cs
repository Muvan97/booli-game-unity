using Systems.Level;
using UnityEngine;

namespace Logic.Other
{
    public class CameraRatioFixer
    {
        private readonly Camera _camera;
        private readonly float _backgroundSpriteWidth;
        private readonly float _backgroundSpriteHeight;

        public CameraRatioFixer(MapPrefab mapPrefab)
        {
            _camera = Camera.main;

            var bounds = mapPrefab.GetComponent<SpriteRenderer>().bounds;
            _backgroundSpriteWidth = Mathf.Abs(bounds.max.x - bounds.min.x);
            _backgroundSpriteHeight = Mathf.Abs(bounds.max.y - bounds.min.y);
        
            UpdateCameraRatio();
        }

        private void UpdateCameraRatio()
        {
            var min = GetCameraBottomLeftCornerPosition();
            var max = GetCameraTopRightCornerPosition();
        
            var cameraWidth = Mathf.Abs(min.x - max.x);
            var cameraHeight = Mathf.Abs(min.y - max.y);
        
            var heightDifferent = cameraHeight / _backgroundSpriteHeight;
            var widthDifferent = cameraWidth / _backgroundSpriteWidth;
        
            _camera.orthographicSize /= Mathf.Max(heightDifferent, widthDifferent);
        }

        private Vector2 GetCameraTopRightCornerPosition() => _camera.ViewportToWorldPoint (new Vector2 (1,1));

        private Vector2 GetCameraBottomLeftCornerPosition() => _camera.ViewportToWorldPoint (new Vector2 (0,0));
    }
}