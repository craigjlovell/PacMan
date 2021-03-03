using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;
using Raylib_cs;

namespace PacMan
{
    class Player
    {
        GameLevelScreen level;
        Vector2 pos;
        Vector2 dir = new Vector2(1, 0);

        float lerpTime = 0;
        Vector2 startTilePos;
        Vector2 endTilePos;

        public Player(GameLevelScreen level, Vector2 pos)
        {
            this.pos = pos;
            this.level = level;

            startTilePos = GetCurrentTilePos();
            endTilePos = GetNextTilePos();
        }

        Vector2 GetCurrentTilePos()
        {
            int row = level.GetYPosToRow(pos.Y);
            int col = level.GetXPosToCol(pos.X);
            var rect = level.GetTileRect(row, col);
            return new Vector2(rect.x + rect.width / 2, rect.y + rect.height / 2);
        }

        Vector2 GetNextTilePos()
        {
            int row = level.GetYPosToRow(pos.Y) + (int)dir.Y;
            int col = level.GetXPosToCol(pos.X) + (int)dir.X;
            var rect = level.GetTileRect(row, col);
            return new Vector2(rect.x + rect.width / 2, rect.y + rect.height / 2);
        }

        public void Update()
        {
            if (Raylib.IsKeyPressed(KeyboardKey.KEY_LEFT))  dir = new Vector2(-1, 0);
            if (Raylib.IsKeyPressed(KeyboardKey.KEY_RIGHT)) dir = new Vector2( 1, 0);
            if (Raylib.IsKeyPressed(KeyboardKey.KEY_UP))    dir = new Vector2( 0,-1);
            if (Raylib.IsKeyPressed(KeyboardKey.KEY_DOWN))  dir = new Vector2( 0, 1);

            lerpTime += Raylib.GetFrameTime();
            if(lerpTime > 1)
            {
                lerpTime = 0;
                startTilePos = GetCurrentTilePos();
                endTilePos = GetNextTilePos();
            }

            pos = Vector2.Lerp(startTilePos, endTilePos, lerpTime);            
        }

        public void Draw()
        {
            Raylib.DrawCircle((int)pos.X, (int)pos.Y, 12, Color.YELLOW);
            DebugDraw();
        }

        public void DebugDraw()
        {
            int row = level.GetYPosToRow(pos.Y);
            int col = level.GetXPosToCol(pos.X);

            var rect = level.GetTileRect(row, col);
            Raylib.DrawRectangleLinesEx(rect, 1, Color.YELLOW);            

            Raylib.DrawCircle((int)startTilePos.X, (int)startTilePos.Y, 3, Color.RED);
            Raylib.DrawCircle((int)endTilePos.X, (int)endTilePos.Y, 3, Color.RED);
            Raylib.DrawLineEx(startTilePos, endTilePos, 1, Color.RED);
            Raylib.DrawCircle((int)pos.X, (int)pos.Y, 2, Color.BLACK);
        }
    }
}
