using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

public class TextField : Component {
    public string text { get; set; }
    public Vector2 position { get; set; }
    public Color color { get; set; }
    public SpriteFont font { get; set; }
    public TextField(string text, Vector2 position, Color color, SpriteFont font) {
        this.text = text;
        this.position = position;
        this.color = color;
        this.font = font;
    }
    public void Draw(SpriteBatch spriteBatch) {
        spriteBatch.DrawString(font, text, position, color);
    }
}