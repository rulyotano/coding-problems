using System.Text;

public class IntToRomanSolution {
    public string IntToRoman(int num) {
        const int maxIndex = 3;
        var romans = new DecimalPair[] { new DecimalPair('I', 'V'), new DecimalPair('X', 'L'), new DecimalPair('C', 'D'), new DecimalPair('M', '-') };
        var i = 0;
        var builder = new StringBuilder();
        while (num > 0)
        {
            var currentDigit = num % 10;
            var romanIndex = i > maxIndex ? maxIndex : i;
            var currentDecimalPair = romans[romanIndex];
            var nextDecimalPair = romanIndex == maxIndex ? null : romans[romanIndex + 1];
            builder.Insert(0, currentDecimalPair.GetValue(currentDigit, nextDecimalPair));

            i++;
            num /= 10;            
        }
        return builder.ToString();
    }
}

public record DecimalPair(char Units, char HalfUnit)
{
    public string GetValue(int decimalValue, DecimalPair nextPair)
    {
        if (decimalValue == 9 && nextPair is not null) return $"{Units}{nextPair.Units}";
        if (decimalValue == 4 && HalfUnit != '-') return $"{Units}{HalfUnit}";
        if (decimalValue < 5 || HalfUnit == '-') return new String(Units, decimalValue);
        return $"{HalfUnit}{new String(Units, decimalValue - 5)}";
    }
}