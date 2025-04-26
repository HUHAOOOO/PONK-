
public enum PlayerIndexType
{
    None = 0,

    P0 = 1,
    P1 = 2,
    P2 = 3,
    P3 = 4,
}
public static class PlayerIndexTypeExtensions
{
    public static int ToIndex(this PlayerIndexType player)
    {
        // C1 neu enum P0 = 1, P1 = 2, ...
        //return (int)player - 1;
        // C2 string -> lay ky tu tu vi tri 1 tro di
         return int.Parse(player.ToString().Substring(1));
    }
    public static PlayerIndexType IndexToPlayerIndexType(int index)
    {
        if (index == 0) return PlayerIndexType.P0;
        if (index == 1) return PlayerIndexType.P1;
        if (index == 2) return PlayerIndexType.P2;
        if (index == 3) return PlayerIndexType.P3;
        return PlayerIndexType.None;
    }
}