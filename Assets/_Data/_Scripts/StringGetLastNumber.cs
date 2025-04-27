using System.Text.RegularExpressions;

public static class StringGetLastNumber
{
    /// <summary>
    /// Lay so cuoi cung trong chuoi . ko co so thi tra ve -1
    /// </summary>
    public static int ExtractLastNumber(string input)
    {
        // tim tat ca so trong chuoi
        MatchCollection matches = Regex.Matches(input, @"\d+");

        if (matches.Count > 0)
        {
            // Lay so cuoi cung
            string lastNumber = matches[matches.Count - 1].Value;
            return int.Parse(lastNumber);
        }

        return -1; // Khong co so
    }
}
