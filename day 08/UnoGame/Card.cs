namespace UnoGame;

public class Card
{
    public CardColor Color {get;}
    public CardValue Value {get;}

    public Card(CardColor color, CardValue value)
    {
        Color = color;
        Value = value;
    }
    
    public bool CanPlayOn(Card top)
    {
        return Color == top.Color ||
            Value == top.Value ||
            Color == CardColor.Wild;
    }

    public override string ToString()
    {
        return $"{Color} {Value}";
    }

    public bool Matches(Card top)
    {
        return Color == top.Color ||
                Value == top.Value ||
                Color == CardColor.Wild;
    }
}