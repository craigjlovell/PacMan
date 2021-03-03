using System;
using Raylib_cs;

namespace PacMan
{
    class Program
    {

        int windowW = 800;
        int windowH = 450;
        string windowT = "PacMan";

        IGameState gameState;

        static void Main(string[] args)
        {
            Program program = new Program();            
            program.RunGame();
        }

        void RunGame()
        {
            Raylib.InitWindow(windowW, windowH, windowT);
            Raylib.SetTargetFPS(60);

            LoadGame();

            while (!Raylib.WindowShouldClose())
            {
                Update();
                Draw();
            }

            Raylib.CloseWindow();
        }

        void LoadGame()
        {
            gameState = new GameLevelScreen(this);
        }

        void Update()
        {
            if (gameState != null)
            {
                gameState.Update();
            }
                
        }

        void Draw()
        {
            Raylib.BeginDrawing();
            Raylib.ClearBackground(Color.WHITE);

            if (gameState != null)
            {
                gameState.Draw();
            }
                

            Raylib.DrawFPS(10, windowH - 20);
            Raylib.EndDrawing();
        }
    }
}
