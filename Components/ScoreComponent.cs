using Microsoft.Xna.Framework;

public class ScoreComponent : Component
{
    private int _score;
    public int level { get; set; }  = 0;
    public int floor { get; set; } = 0;

    public int addScore(int score)
    {
        _score += score;
        return _score;
    }

    public int getScore()
    {
        return _score;
    }
    
    public ScoreComponent()
    {
        _score = 0;
    }

    public override void Update(GameTime gameTime)
    {
        //throw new NotImplementedException();
    }
}