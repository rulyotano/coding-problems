Problem URL: [Partition to K Equal Sum Subsets](https://leetcode.com/problems/partition-to-k-equal-sum-subsets/)

> tags: `#array`, `#backtracking`, `#bitmanipulation`

1st try took: >3hrs

2 backtracking solutions, 1 too expensive, adding items to each subset, 2nd solution get all the subsets and check how can combine them, needed to optimize the solution by cutting a lot of recursive branches.

some test cases:
//[2,9,4,7,3,2,10,5,3,6,6,2,7,5,2,4]
//7

//[3, 2, 1, 3, 6, 1, 4, 8, 10, 8, 9, 1, 7, 9, 8, 1]
//9

//[9,6,1,8,4,3,4,1,7,3,7,4,5,3,2,3]
//10