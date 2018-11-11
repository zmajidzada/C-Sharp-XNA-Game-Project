using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FinalProject31
{
    class GameConstants
    {
        public const int ScreenWidth = 640;//1280;
        public const int ScreenHeight = 480;//960;

        public const int NumEnemies = 30;
        public const int NumPossibleEnemies = 90;
        public const float EnemySpeedAdjustment = 40;
        public const int NumBullets = 10;

        public const int BulletSpeed = 6;
        public const float BulletSpeedAdjustment = 100;

        public const int NumExplosions = NumBullets;
        public const int NumLives = 20;
        public const float SmallScaleExplosionInc = 0.1f;

        public const float TransparencyDec = 12f;
        public const float PowerUpTransparencyDec = 0.8f;
        public const float rotationInc = 0.5f;
        public const int LivesInc = 5;

        public const float LargeScaleExplosionInc = 1.2f;
        public const float vibrationInc = .3f;


    }
}