using System.Collections.Generic;
using Microsoft.Xna.Framework.Graphics;
using tinr;

public static class TextureManager
{
    public static Dictionary<string, Texture2D> textures = new Dictionary<string, Texture2D>();

    public static Texture2D GetTexture(string name)
    {
        return textures[name];
    }

    public static void AddTexture(string name, Texture2D texture)
    {
        textures.Add(name, texture);
    }

    public static void AddTexture (string name, string path)
    {
        textures.Add(name, Game1.game.Content.Load<Texture2D>(path));
    }
}