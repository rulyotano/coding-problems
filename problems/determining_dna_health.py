import sys
import re
import random
import os
import math


# imported


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


class Trie:
    def __init__(self, character='', value=None):
        self.children = {}
        self.character = character
        self.value = value

    def add(self, stringValue, currentIndex=0):
        latestIndex = len(stringValue) - 1
        if currentIndex > latestIndex:
            return self

        character = stringValue[currentIndex]

        if not character in self.children:
            self.children[character] = Trie(character)

        return self.children[character].add(stringValue, currentIndex + 1)

    def find_node(self, searchString, currentIndex=0):
        latestIndex = len(searchString) - 1
        if currentIndex > latestIndex:
            return self

        character = searchString[currentIndex]

        if not character in self.children:
            return None

        return self.children[character].find_node(searchString, currentIndex + 1)

# end imported


class DataItem:
    def __init__(self, index, sum):
        self.index = index
        self.sum = sum

    def __sub__(self, other):
        return self.index - other.index

    def __eq__(self, other):
        return self.index == other.index

    def __ne__(self, other):
        return self.index != other.index

    def __lt__(self, other):
        return self.index < other.index

    def __gt__(self, other):
        return self.index > other.index

    def __le__(self, other):
        return self.index <= other.index

    def __ge__(self, other):
        return self.index >= other.index


def count_health(trie, firstIndex, lastIndex, stream):
    totalHealth = 0

    for i in range(len(stream)):
        c = stream[i]
        currentTrie = trie.find_node(c)
        j = i
        while currentTrie != None:
            if (currentTrie.value != None):
                totalHealth += count_health_in_node_value(
                    currentTrie.value, firstIndex, lastIndex)

            j += 1
            if (j == len(stream)):
                break
            c = stream[j]
            currentTrie = currentTrie.find_node(c)

    return totalHealth


def count_health_in_node_value(valueItems, firstIndex, lastIndex):
    startIndexForCount = binary_search(
        valueItems, lambda value, i: is_insert_index(DataItem(firstIndex, 0), valueItems, i))

    endIndexForCount = binary_search(
        valueItems, lambda value, i: is_insert_index(DataItem(lastIndex, 0), valueItems, i, insert_after=True)) - 1

    if startIndexForCount == -1 or endIndexForCount == -1:
        return 0

    value_to_substract = 0 if startIndexForCount == 0 else valueItems[
        startIndexForCount - 1].sum

    total_sum = valueItems[-1].sum if endIndexForCount == - \
        2 else valueItems[endIndexForCount].sum

    return total_sum - value_to_substract


def build_trie(n, genes, health):
    trie = Trie()

    for i in range(n):
        gene_trie = trie.add(genes[i])
        add_health_to_gene_trie(gene_trie, health[i], i)

    return trie


def add_health_to_gene_trie(gene_trie, health, index):
    if gene_trie.value == None:
        gene_trie.value = [DataItem(index, health)]
        return

    items = gene_trie.value
    insert_index = binary_search(
        items, lambda value, i: is_insert_index(DataItem(index, 0), items, i))

    insert_item = None
    if insert_index == -1:
        insert_item = DataItem(index, health + items[-1].sum)
        items.append(insert_item)
        return

    previous_sum = 0 if insert_index == 0 else items[insert_index - 1].sum
    insert_item = DataItem(index, health + previous_sum)
    items.insert(insert_index, insert_item)
    for i in range(insert_index + 1, len(items)):
        items[i].sum += health


#!/bin/python

if __name__ == '__main__':
    n = int(raw_input())

    genes = raw_input().rstrip().split()


    health = map(int, raw_input().rstrip().split())


    s = int(raw_input())

    trie = build_trie(n, genes, health)

    min = 100000000000
    max = 0

    for s_itr in xrange(s):
        firstLastd = raw_input().split()

        first = int(firstLastd[0])

        last = int(firstLastd[1])

        d = firstLastd[2]

        current_count = count_health(trie, first, last, d)

        if current_count > max:
            max = current_count

        if current_count < min:
            min = current_count

    print(str(min) + ' ' + str(max))
