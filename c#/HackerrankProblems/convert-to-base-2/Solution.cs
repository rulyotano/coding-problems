using System.Text;

namespace convert.to.base2;

public class Solution {

    public string BaseNeg2(int n) {
        if (n == 0) return "0";
        var builder = new StringBuilder();
        while (n != 0)
        {
            builder.Insert(0, BitToChar((n & 1) == 1));
            n = -(n >> 1);
        }

        return builder.ToString();
    }

    private string BaseNeg2V1(int n) {
        if (n == 0) return "0";
        var base2 = IntToBase2(n);
        var builder = new StringBuilder();

        var carry = false;
        for (var i = 0; i < base2.Count; i++)
        {
            var even = i % 2 == 0;
            var bit = base2[i];
            builder.Insert(0, BitToChar(carry ^ bit));

            if (even)
            {
                carry = carry && bit;
            }
            else 
            {
                carry = carry || bit;
            }
        }
        if (carry) 
        {
            builder.Insert(0, BitToChar(true));
            if (builder.Length % 2 == 0) builder.Insert(0, BitToChar(true));
        }

        return builder.ToString();
    }

    private List<bool> IntToBase2(int n)
    {
        var result = new List<bool>();

        while (n > 0)
        {
            result.Add(n % 2 != 0);
            n = n / 2;
        }

        return result;
    }

    private char BitToChar(bool bit) => bit ? '1' : '0';
}