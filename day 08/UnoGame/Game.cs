namespace UnoGame;
public class Game
{
    private List<Player> _players = new List<Player>();
    private Deck _deck = new Deck();
    private Stack<Card> _discard = new Stack<Card>();

    private int _currentPlayer = 0;
    private int _direction = 1;
    public Game()
    {
        _players.Add(new Player("Player 1"));
        _players.Add(new Player("Player 2"));
    }


    public void StartGame()
    {
        foreach (var p in _players)
            p.DrawCard(_deck, 7);

        // Start discard
        var first = _deck.Draw();
        if (first.Color == CardColor.Wild) first = new Card(CardColor.Red, CardValue.Zero);
        _discard.Push(first);

        Console.WriteLine($"Starting card: {first}\n");

        Run();
    }

    private void Run()
    {
        while (true)
        {
            var current = _players[_currentPlayer];
            var top = _discard.Peek();

            Console.WriteLine($"--- {current.Name}'s TURN ---");
            Console.WriteLine($"Top Card: {top}");
            Console.WriteLine("Your Cards:");
            for (int i = 0; i < current.Hand.Count; i++)
                Console.WriteLine($"{i}. {current.Hand[i]}");

            var playable = current.GetPlayableCard(top);

            if (playable == null)
            {
                Console.WriteLine("No playable card. Drawing...");
                current.DrawCard(_deck);
            }
            else
            {
                Console.WriteLine($"Playing: {playable}");
                current.RemoveCard(playable);
                _discard.Push(playable);

                ApplyEffect(playable);
            }

            if (current.Hand.Count == 0)
            {
                Console.WriteLine($"{current.Name} WINS!");
                return;
            }

            MoveToNextPlayer();
            Console.WriteLine();
        }
    }

    private void ApplyEffect(Card card)
    {
        switch (card.Value)
        {
            case CardValue.Skip:
                Console.WriteLine("Next player skipped!");
                MoveToNextPlayer();
                break;

            case CardValue.Reverse:
                Console.WriteLine("Direction reversed!");
                _direction *= -1;
                break;

            case CardValue.DrawTwo:
                Console.WriteLine("Next player draws 2!");
                var next = GetNextPlayer();
                next.DrawCard(_deck, 2);
                MoveToNextPlayer();
                break;

            case CardValue.Wild:
                ChangeColor();
                break;

            case CardValue.WildDrawFour:
                ChangeColor();
                var p = GetNextPlayer();
                Console.WriteLine("Next player draws 4!");
                p.DrawCard(_deck, 4);
                MoveToNextPlayer();
                break;
            }
        }

    private void ChangeColor()
    {
        Console.WriteLine("Choose a color:");
        Console.WriteLine("0. Red");
        Console.WriteLine("1. Blue");
        Console.WriteLine("2. Green");
        Console.WriteLine("3. Yellow");
        int opt = int.Parse(Console.ReadLine());
        var chosenColor = (CardColor)opt;

        // Replace top card color
        var top = _discard.Pop();
        var newCard = new Card(chosenColor, top.Value);
        _discard.Push(newCard);

        Console.WriteLine($"Color changed to {chosenColor}");
    }

    private Player GetNextPlayer()
    {
        int next = _currentPlayer + _direction;
        if (next >= _players.Count) next = 0;
        if (next < 0) next = _players.Count - 1;
        return _players[next];
    }

    private void MoveToNextPlayer()
    {
        _currentPlayer += _direction;
        if (_currentPlayer >= _players.Count) _currentPlayer = 0;
        if (_currentPlayer < 0) _currentPlayer = _players.Count - 1;
    }
}