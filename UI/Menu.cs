using Microsoft.Xna.Framework.Graphics;
using secondgame.Drawing;
using System;
using System.Numerics;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace secondgame.UI
{
    public class Menu
    {
        public Vector2 Position;
        public Vector2 Size;
        public Texture2D Texture; // background texture
        public Microsoft.Xna.Framework.Color BackgroundColour;
        public List<UIElement> UIElements;
        public System.Drawing.RectangleF ClickBox;

        public bool Important; // whether or not stuff in game can happen while this menu is up

        public bool Draggable;
        public int DefaultButtonCooldown => 25;
        public int ButtonCooldown;

        public int TimeSinceLastDrag;

        public Menu(Vector2 position, Vector2 size, Texture2D texture)
        {
            Position = position;
            Size = size;
            Texture = texture;

            PrepareMenu();
        }

        public Menu(Vector2 position, Vector2 size, string texture)
        {
            Position = position;
            Size = size;
            Texture = Assets.GetTexture(texture);

            PrepareMenu();
        }
        public Menu(Vector2 position, float width, float height, Texture2D texture)
        {
            Position = position;
            Size = new Vector2(width, height);
            Texture = texture;

            PrepareMenu();
        }

        public void PrepareMenu()
        {
            UIElements = new List<UIElement>();
            ButtonCooldown = 0;

            if (Texture == Assets.GetTexture("box"))
                BackgroundColour = Microsoft.Xna.Framework.Color.Black; // dont colour if none is specified
            else BackgroundColour = Microsoft.Xna.Framework.Color.White;

            Important = false;
            Draggable = false;

            TimeSinceLastDrag = 0;

            UpdateClickBox();
        }
        public void SetImportant(bool important)
        {
            Important = important;
        }

        public void SetDraggable(bool draggable)
        {
            Draggable = draggable;
        }

        public void SetBackgroundColour(Microsoft.Xna.Framework.Color colour)
        {
            BackgroundColour = colour;
        }

        public void Display()
        {
            MenuManager.MenusToAdd.Add(this);
        }

        public void Hide()
        {
            MenuManager.MenusToRemove.Add(this);
        }

        public void Update()
        {
            if (ButtonCooldown > 0)
            {
                ButtonCooldown--;
            }

            if (Draggable)
            {
                TimeSinceLastDrag++;

                if ((ClickBox.Contains(MousePosition) || TimeSinceLastDrag < 10) && IsLeftClickDown())
                {
                    TimeSinceLastDrag = 0;

                    // calculate the mouses relative position within the menu

                    Vector2 mouseRelativePosition = MousePosition - TopLeft();

                    Position = TopLeft() + mouseRelativePosition;
                }
            }

            UpdateClickBox();

            foreach (UIElement uIElement in UIElements)
            {
                if (uIElement.IsClicked() && ButtonCooldown == 0 && !WasMouseDownLastFrame)
                {
                    ButtonCooldown = DefaultButtonCooldown;

                    uIElement.HandleClick();
                }

                uIElement.Update();
            }
        }

        public void UpdateClickBox()
        {
            ClickBox = new System.Drawing.RectangleF(TopLeft().X, TopLeft().Y, Width(), Height());
        }

        public void AddUIElements(UIElement[] uIElements)
        {
            foreach (UIElement uIElement in uIElements)
            {
                AddUIElement(uIElement);
            }
        }
        public void AddUIElement(UIElement uiElement)
        {
            uiElement.Owner = this;
            UIElements.Add(uiElement);
        }

        public float Width() => Size.X;

        public float Height() => Size.Y;

        public Vector2 RelativeCentreOfMenu() => new Vector2(Width(), Height()) / 2f;

        public Vector2 TopLeft() => Position - RelativeCentreOfMenu();
        public virtual void Draw(SpriteBatch s)
        {
            Vector2 origin = CalculateOffset(Texture.Width, Texture.Height);
            SpriteBatchWrapper.Draw(Texture, Position, null, BackgroundColour, 0, origin, new Vector2(Size.X / Texture.Width, Size.Y / Texture.Height), SpriteEffects.None, 1); // scale texture up to required size

            foreach (UIElement uiElement in UIElements)
            {
                uiElement.Draw(s);
            }
        }
    }
}
