namespace product.of.array.except.self;

// src: leetcode

public class Solution {
    public int[] ProductExceptSelf(int[] nums) {
        // idea behind this is having 2 arrays, one for prefix prods and another for suffix prods.
        // suffix array isn't needed since we can store the values in a var and use the iteration to set the values
        var result = new int[nums.Length];
        var current = 1;

        for (var i = 0; i < nums.Length; i++)
        {
            current *= nums[i];
            result[i] = current;
        }

        current = 1;
        for (var i = nums.Length - 1; i >= 0; i--)
        {
            result[i] = i == 0 ? current : result[i - 1] * current;
            current *= nums[i];
        }
        return result;
    }

    public int[] ProductExceptSelf2(int[] nums) {
        // this solution is the most intuitive, but uses / which isn't allowed. Think to consider amount of 0s
        // 1 zero => only zero index as prod of the rest, >= 2 zeros => all zeros

        var prod = 1;
        var zeroIndex = -1;
        for (var i = 0; i < nums.Length; i++)
        {
            if (nums[i] == 0)
            {
                if (zeroIndex > -1) 
                    return new int[nums.Length];
                zeroIndex = i;
            }
            else 
            {
                prod *= nums[i];
            }
        }
        
        var result = new int[nums.Length];

        for (var i = 0; i < nums.Length; i++)
        {
            if (zeroIndex > -1)
            {
                result[i] = i == zeroIndex ? prod : 0;
            }
            else
            {
                result[i] = prod / nums[i];
            }
        }

        return result;
    }
}