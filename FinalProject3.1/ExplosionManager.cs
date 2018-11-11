using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Storage;

namespace FinalProject31
{
    class ExplosionManager
    {
        public Explosion[] explosionArray;

        private int ExplosionCounter;
        public ExplosionManager()
        {
            explosionArray = new Explosion[GameConstants.NumExplosions];
            for (int i = 0; i < GameConstants.NumExplosions; i++)
            {
                explosionArray[i] = new Explosion();
            }
            ExplosionCounter = 0;
        }
        public void Initialize(Texture2D explosionTexture)
        {
            for (int i = 0; i < GameConstants.NumExplosions; i++)
            {
                explosionArray[i].Initialize(explosionTexture);
            }
        }
        // the function updates and kills off explosions
        // An explosions transparency increases over time (tends towards 0)
        // It is killed off when its transparency <0
        // It scales and rotates over time
        public void Update(GameTime gameTime)
        {
            for (int i = 0; i < GameConstants.NumExplosions; i++)
            {
                if (explosionArray[i].alive)
                {
                    explosionArray[i].transparency -= GameConstants.TransparencyDec;
                    explosionArray[i].scale += explosionArray[i].scaleInc;
                    explosionArray[i].rotation += GameConstants.rotationInc;
                    explosionArray[i].color = new Color(255, 255, 255, (byte)explosionArray[i].transparency);
                    if (explosionArray[i].transparency <= 0f) explosionArray[i].alive = false;
                }
            }
        }

        // This function create explosions
        // The explosion counter is incremented through the array and reset to the beginning when it reaches the end
        // Meaning it is possible a current explosion may be suddenly terminated in order to create a new one
        public void CreateExplosion(Vector2 position, float scaleInc)
        {
            explosionArray[ExplosionCounter].alive = true;
            explosionArray[ExplosionCounter].position = position;
            explosionArray[ExplosionCounter].scale = 1.0f;
            explosionArray[ExplosionCounter].scaleInc = scaleInc;
            explosionArray[ExplosionCounter].transparency = 255f;
            explosionArray[ExplosionCounter].color = new
                Color(255, 255, 255, (byte)explosionArray[ExplosionCounter].transparency);
            explosionArray[ExplosionCounter].origin.X = 16;
            explosionArray[ExplosionCounter].origin.Y = 16;
            ExplosionCounter++;
            // If the explosion counter goes beyond the end of the array the following line resets to the beginning
            ExplosionCounter = ExplosionCounter % GameConstants.NumExplosions;
        }
        public void Draw(SpriteBatch spriteBatch)
        {

            for (int i = 0; i < GameConstants.NumExplosions; i++)
            {
                if (explosionArray[i].alive)
                    spriteBatch.Draw(explosionArray[i].texture, explosionArray[i].position, null, explosionArray[i].color,
                     explosionArray[i].rotation, explosionArray[i].origin, explosionArray[i].scale, SpriteEffects.None, 0f);
            }
        }

    }
}