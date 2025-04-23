
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
}