﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Input;
using System.Numerics;

namespace secondgame.CoreSystems
{
    public static class InputSystem
    {
        public static KeyboardState KeyboardState;
        public static MouseState MouseState;
        public static Vector2 MousePosition;
        public static bool WasMouseDownLastFrame;
        public static void UpdateInputSystem()
        {
            WasMouseDownLastFrame = IsLeftClickDown();

            KeyboardState = Keyboard.GetState();
            MouseState = Mouse.GetState();

            MousePosition = new Vector2(MouseState.X, MouseState.Y);
        }

        public static bool IsLeftClickDown()
        {
            return MouseState.LeftButton == ButtonState.Pressed;
        }

        public static bool IsRightClickDown()
        {
            return MouseState.RightButton == ButtonState.Pressed;
        }
        public static bool IsKeyPressed(Keys key)
        {
            return KeyboardState.IsKeyDown(key);
        }


    }
}
