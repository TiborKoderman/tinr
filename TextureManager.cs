using System.Collections.Generic;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using tinr;

public static class TextureManager
{
    //textures
    public static Dictionary<string, Texture2D> textures = new Dictionary<string, Texture2D>();
    //fonts
    public static Dictionary<string, SpriteFont> fonts = new Dictionary<string, SpriteFont>();
    //sounds
    public static Dictionary<string, SoundEffect> sounds = new Dictionary<string, SoundEffect>();

    public static Texture2D GetTexture(string name)
    {
        return textures[name];
    }

    public static SpriteFont GetFont(string name)
    {
        return fonts[name];
    }

    public static SoundEffect GetSound(string name)
    {
        return sounds[name];
    }


    public static void AddTexture(string name, Texture2D texture)
    {
        textures.Add(name, texture);
    }

    public static void AddFont(string name, SpriteFont font)
    {
        fonts.Add(name, font);
    }

    public static void AddSound(string name, SoundEffect sound)
    {
        sounds.Add(name, sound);
    }

    public static void AddTexture (string name, string path)
    {
        textures.Add(name, Game1.game.Content.Load<Texture2D>(path));
    }

    public static void AddFont (string name, string path)
    {
        fonts.Add(name, Game1.game.Content.Load<SpriteFont>(path));
    }

    public static void AddSound (string name, string path)
    {
        sounds.Add(name, Game1.game.Content.Load<SoundEffect>(path));
    }
}