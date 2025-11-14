namespace UnoGame;

public class Player
{
    public string Name { get; }
    public List<Card> Hand { get; } = new List<Card>();

    public Player(string name)
    {
        Name = name;
    }

    public void DrawCard(Deck deck, int count = 1)
    {
        for (int i = 0; i < count; i++)
            Hand.Add(deck.Draw());
    }

    public Card GetPlayableCard(Card topCard)
    {
        return Hand.FirstOrDefault(c => c.Matches(topCard));
    }

    public void RemoveCard(Card card)
    {
        Hand.Remove(card);
    }
}