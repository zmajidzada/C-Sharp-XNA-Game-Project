﻿using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Net;
using Microsoft.Xna.Framework.Storage;

namespace FinalProject31
{
    class GameObject
    {
        public Texture2D sprite;
        public Vector2 position;
        public float rotation;
        public Vector2 centre;
        public Vector2 velocity;
    //    public Vector2 velocitx;
        public bool alive;

        public GameObject(Texture2D loadedTexture)
        {
          
            rotation = 0.0f;
            position = Vector2.Zero;
            sprite = loadedTexture;
            centre = new Vector2(sprite.Width / 2, sprite.Height / 2);
            velocity = Vector2.Zero;
        //    velocitx = Vector2.One;
            alive = false;
        }

    }
}
