using System.Collections.Generic;
using Microsoft.Xna.Framework.Graphics;
using tinr;

public static class TextureManager
{
    public static Dictionary<string, Texture2D> textures = new Dictionary<string, Texture2D>();
    public static Dictionary<string, SpriteFont> fonts = new Dictionary<string, SpriteFont>();

    public static Texture2D GetTexture(string name)
    {
        return textures[name];
    }

    public static SpriteFont GetFont(string name)
    {
        return fonts[name];
    }


    public static void AddTexture(string name, Texture2D texture)
    {
        textures.Add(name, texture);
    }

    public static void AddFont(string name, SpriteFont font)
    {
        fonts.Add(name, font);
    }

    public static void AddTexture (string name, string path)
    {
        textures.Add(name, Game1.game.Content.Load<Texture2D>(path));
    }

    public static void AddFont (string name, string path)
    {
        fonts.Add(name, Game1.game.Content.Load<SpriteFont>(path));
    }
}