namespace AOC2015._04;

public class Day04A : Day
{
    protected override string Run()
    {
        var input = "ckczppom";

        var i = 0;
        using var md5 = System.Security.Cryptography.MD5.Create();
        while (true)
        {
            i++;
            var key = input + i;
            
            var inputBytes = System.Text.Encoding.ASCII.GetBytes(key);
            var hashBytes = md5.ComputeHash(inputBytes);
            
            var hash = Convert.ToHexString(hashBytes);

            if (hash[..5] == "00000")
            {
                return "" + i;
            }

        }
    }
}