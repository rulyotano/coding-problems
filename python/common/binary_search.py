import math


def binary_search(orderList, compare):
    """Compare function return < 0 when should search left 
    when > 0 when search to right
    Should return 0 when found"""

    def get_middle_index(startIndex, endIndex):
        return abs(startIndex + endIndex)//2

    def recursion_search(startIndex, endIndex):
        if startIndex > endIndex:
            return -1
        if startIndex == endIndex:
            return startIndex if compare(orderList[startIndex], startIndex) == 0 else -1
        if endIndex - startIndex == 1:
            return startIndex if compare(orderList[startIndex], startIndex) == 0 else (endIndex if compare(orderList[endIndex], endIndex) == 0 else -1)

        middleIndex = get_middle_index(startIndex, endIndex)
        middleItem = orderList[middleIndex]
        result = compare(middleItem, middleIndex)

        if result == 0:
            return middleIndex

        if result < 0:
            return recursion_search(startIndex, middleIndex)

        return recursion_search(middleIndex, endIndex)

    return recursion_search(0, len(orderList) - 1)


def is_insert_index(insertItem, collection, index, insert_after=False):
    testItem = collection[index]

    def compare_equal():
        return not insert_after and insertItem == testItem

    def is_greater(left, right):
        return left >= right if insert_after else left > right

    if compare_equal():
        return 0

    if insertItem < testItem and (index == 0 or is_greater(insertItem, collection[index - 1])):
        return 0

    comapre_result = insertItem - testItem
    return comapre_result if not insert_after or comapre_result != 0 else 1
