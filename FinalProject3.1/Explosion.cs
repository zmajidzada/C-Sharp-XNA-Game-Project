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
    struct Explosion
    {
        public Texture2D texture;
        public Vector2 position;
        public Vector2 origin;
        public float width;
        public float height;
        public bool alive;
        public float scale;
        public float scaleInc;
        public float transparency;
        public float rotation;
        public Color color;

        public void Initialize(Texture2D explosionTexture)
        {
            texture = explosionTexture;
            alive = false;
            scale = 1.0f;
            transparency = 255f;
            rotation = 0f;
            color = new Color(255, 255, 255, (byte)transparency);
            width = texture.Width;
            height = texture.Height;
            origin.X = width / 2f;
            origin.Y = height / 2f;
        }
    }

}