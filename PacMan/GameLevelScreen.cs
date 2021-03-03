using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;
using Raylib_cs;

namespace PacMan
{
    enum TileType
    {
        EMPTY, //0
        WALL,  //1
        DOT,   //2
        PLAYER_START //3
    }

    class GameLevelScreen : IGameState
    {

        float tileW = 32;
        float tileH = 32;
        float mapXPos = 10;
        float mapYPos = 40;

        TileType[,] map;

        int lives = 3;
        int score = 1000;

        Player player;



        public GameLevelScreen(Program program) : base(program)
        {
            LoadLevel();
        }

        void LoadLevel()
        {
            int[,] tilemap = new int[,]
            {
                {1,1,1,1,1,1,1,1,1,1},
                {1,0,0,0,0,0,0,0,0,1},
                {1,0,1,1,1,0,1,1,0,1},
                {1,0,1,0,0,0,0,1,0,1},
                {1,0,0,0,1,1,0,1,0,0},
                {0,0,1,0,1,1,0,0,0,1},
                {1,0,1,0,0,0,0,1,0,1},
                {1,0,1,1,0,1,1,1,0,1},
                {1,0,0,0,3,0,0,0,0,1},
                {1,1,1,1,1,1,1,1,1,1}
            };

            //Copy the above int array to a stronly typed tile array
            map = new TileType[tilemap.GetLength(0), tilemap.GetLength(1)];
            for (int row = 0; row < tilemap.GetLength(0); row++)
            {
                for (int col = 0; col < tilemap.GetLength(1); col++)
                    map[row, col] = (TileType)tilemap[row, col];
            }
            

            for (int row = 0; row < map.GetLength(0); row++)
            {
                for (int col = 0; col < map.GetLength(1); col++)
                {
                    var tileValue = GetTileValue(row, col);
                    if (tileValue == TileType.EMPTY)        SetTileValue(row, col, TileType.DOT);                                           
                    if (tileValue == TileType.PLAYER_START) CreatePlayer(row, col);                                            
                }
            }
        }
        public void CreatePlayer(int row, int col)
        {
            var rect = GetTileRect(row, col);
            var pos = new Vector2(rect.x + rect.width / 2, rect.y + rect.height / 2);
            player = new Player(this, pos);
            
            SetTileValue(row, col, TileType.EMPTY);
        }

        public int GetTileID(int row, int col)
        {
            return row * map.GetLength(1) + col;
        }

        public TileType GetTileValue(int row, int col)
        {
            return map[row, col];
        }

        public void SetTileValue(int row, int col, TileType value)
        {
            map[row, col] = value;
        }

        public Color GetTileColor(int row, int col)
        {
            var tileValue = GetTileValue(row, col);
            if (tileValue == TileType.EMPTY) return Color.WHITE;
            if (tileValue == TileType.WALL) return Color.BLACK;
            if (tileValue == TileType.DOT) return Color.WHITE;

            return Color.WHITE;
        }

        public Rectangle GetTileRect(int row, int col)
        {
            float xPos = mapXPos + (col * tileW);
            float yPos = mapYPos + (row * tileH);
            return new Rectangle(xPos, yPos, tileW, tileH);
        }

        public int GetYPosToRow(float yPos)
        {
            return (int)((yPos - mapYPos) / tileH);
        }

        public int GetXPosToCol(float xPos)
        {
            return (int)((xPos - mapXPos) / tileW);
        }

        public override void Update()
        {
            player.Update();
        }

        public override void Draw()
        {
            DrawMap();
            DrawUI();
            player.Draw();

            if (Raylib.IsKeyDown(KeyboardKey.KEY_LEFT_CONTROL))
            {
                DebugDraw();
            }
            

        }

        void DrawMap()
        {
            for (int row = 0; row < map.GetLength(0); row++)
            {
                for (int col = 0; col < map.GetLength(1); col++)
                {
                    var tileValue = GetTileValue(row, col);
                    var color = GetTileColor(row, col);
                    var rect = GetTileRect(row, col); ;
                    Raylib.DrawRectangleRec(rect, color);

                    if(tileValue == TileType.DOT)
                    {
                        int pacDotSize = 4;
                        Raylib.DrawCircle((int)(rect.x + rect.width / 2), (int)(rect.y + rect.height / 2), pacDotSize, Color.PINK);
                    }
                }
            }
        }

        void DrawUI()
        {
            Raylib.DrawText($"Score: {score}", 30, 10, 20, Color.LIME);
            Raylib.DrawText($"Lives: {lives}", 210, 10, 20, Color.LIME);
        }

        void DebugDraw()
        {
            for (int row = 0; row < map.GetLength(0); row++)
            {
                for (int col = 0; col < map.GetLength(1); col++)
                {
                    var rect = GetTileRect(row, col);
                    var color = new Color(255, 255, 255, 128);

                    Raylib.DrawRectangleRec(rect, color);
                    Raylib.DrawRectangleLinesEx(rect, 1, Color.WHITE);

                    var tileId = GetTileID(row, col);
                    var tileVal = (int)GetTileValue(row, col);

                    Raylib.DrawText(tileId.ToString(), (int)(rect.x + 2), (int)(rect.y + 2), 10, Color.SKYBLUE);
                    Raylib.DrawText(tileVal.ToString(), (int)(rect.x + 2), (int)(rect.y + rect.height - 14), 10, Color.SKYBLUE);

                }
            }
        }
    }
}
