using System.Windows.Media;

public class ConsoleMessageType
{
    public Brush Color { get; }

    private ConsoleMessageType(Brush color)
    {
        Color = color;
    }

    public static readonly ConsoleMessageType Menu =
        new ConsoleMessageType(Brushes.Cyan);

    public static readonly ConsoleMessageType Info =
        new ConsoleMessageType(Brushes.Goldenrod);

    public static readonly ConsoleMessageType Error =
        new ConsoleMessageType(Brushes.Red);

    public static readonly ConsoleMessageType Warning =
        new ConsoleMessageType(Brushes.Yellow);

    public static readonly ConsoleMessageType Debug =
        new ConsoleMessageType(Brushes.Gray);
}
