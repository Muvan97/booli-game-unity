using System;

namespace Logic.Other
{
    [Serializable]
    public struct Rectangle
    {
        public float LeftBorder;
        public float RightBorder;
        public float TopBorder;
        public float BottomBorder;

        public Rectangle(float leftBorder, float rightBorder, float topBorder, float bottomBorder)
        {
            LeftBorder = leftBorder;
            RightBorder = rightBorder;
            TopBorder = topBorder;
            BottomBorder = bottomBorder;
        }
    }
}