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
            return compare(orderList[startIndex], startIndex)
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


def is_insert_index(insertItem, collection, index):
    testItem = collection[index]
    if insertItem == testItem:
        return 0
    if insertItem < testItem and (index == 0 or insertItem > collection[index - 1]):
        return 0

    return insertItem - testItem