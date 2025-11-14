namespace UnoGame;
public class Deck
{
    Stack<Card> _cards = new Stack<Card>();

    public Deck()
    {
        var temp = new List<Card>();

        foreach(CardColor color in Enum.GetValues(typeof(CardColor)))
        {
            if(color == CardColor.Wild)
            {
                temp.Add(new Card(color, CardValue.Wild));
                temp.Add(new Card(color, CardValue.Wild));
                temp.Add(new Card(color, CardValue.WildDrawFour));
                temp.Add(new Card(color, CardValue.WildDrawFour));
                continue;
            }

            foreach(CardValue value in Enum.GetValues(typeof(CardValue)))
            {
                if (value == CardValue.Wild || value == CardValue.WildDrawFour)
                    continue;
                temp.Add(new Card(color, value));
            }

            var rnd = new Random();
            foreach (var card in temp.OrderBy(c => rnd.Next()))
                _cards.Push(card);
        }
    }

    public Card Draw()
    {
        if (_cards.Count == 0) throw new Exception("Deck empty!");
        return _cards.Pop();
    }
}