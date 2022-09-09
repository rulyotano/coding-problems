import sys
import re
import random
import os
import math
from common.binary_search import binary_search, is_insert_index
from common.trie import Trie

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
