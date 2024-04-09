using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace secondgame.UI
{
    public static class MenuManager
    {
        public static int MenuCooldown;
        public static int MenuCooldownTimer;

        public static bool PauseMenuDisplayed;

        public static Menu PauseMenu;
        public static List<Menu> MenusToAdd;
        public static List<Menu> MenusToRemove;
        public static List<Menu> ActiveMenus;

        public static void Initialise()
        {
            MenuCooldown = 20;
            MenuCooldownTimer = MenuCooldown;
            PauseMenuDisplayed = false;
            MenusToAdd = new List<Menu>();
            MenusToRemove = new List<Menu>();
        }
        public static void ManageMenus()
        {
            //if (MenuCooldownTimer > 0)
            //{
            //    MenuCooldownTimer--;
            //}

            //if (IsKeyPressed(Keys.Escape) && GameState.State == GameState.GameStates.InGame && MenuCooldownTimer == 0)
            //{
            //    if (!PauseMenuDisplayed)
            //    {
            //        PauseMenu = new Menu(Utilities.CentreOfScreen(), new Vector2(ScreenWidth / 6, ScreenHeight / 6), "MenuBG");

            //        PauseMenu.SetDraggable(true);
            //        PauseMenu.SetImportant(true);

            //        ExitButton exitGame = new ExitButton("ExitButton", Vector2.One * 3f, PauseMenu);

            //        exitGame.SetPositionInMenu(PauseMenu.RelativeCentreOfMenu());

            //        PauseMenu.AddUIElement(exitGame);
            //        PauseMenu.Display();

            //        PauseMenuDisplayed = true;
            //    }

            //    else
            //    {
            //        PauseMenu.Hide();
            //        PauseMenuDisplayed = false;
            //    }

            //    MenuCooldownTimer = MenuCooldown;
            //}

            foreach (Menu menu in MenusToAdd)
            {
                ActiveMenus.Add(menu);
            }

            foreach (Menu menu in MenusToRemove)
            {
                ActiveMenus.Remove(menu);
            }
        }
    }
}
