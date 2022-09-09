import sys
import re
import random
import os
import math
from common.string_matching import kmp, kmp_initialize_suffix_prefix_array
from common.trie import Trie


def count_health(genes_data, firstIndex, lastIndex, stream):
    total_health = 0
    genes_count_cache = Trie()

    for i in range(firstIndex, lastIndex + 1):
        gene = genes_data.genes[i]
        health = genes_data.health[i]

        gene_count_cache_node = genes_count_cache.find_node(gene)

        if gene_count_cache_node == None:
            prefix_array = genes_data.prefix_arrays[i]
            appear_positions = kmp(gene, stream, prefix_array)
            gene_count_cache_node = genes_count_cache.add(gene)
            gene_count_cache_node.value = len(appear_positions)

        total_health += gene_count_cache_node.value * health

    return total_health


class GenesData():
    def __init__(self, genes, health, prefix_arrays):
        self.genes = genes
        self.health = health
        self.prefix_arrays = prefix_arrays


#!/bin/python
if __name__ == '__main__':
    n = int(raw_input())

    genes = raw_input().rstrip().split()

    health = map(int, raw_input().rstrip().split())

    s = int(raw_input())

    genes_prefix_cache_trie = Trie()
    prefix_arrays = [None for g in genes]

    # calculate the prefix arrays by using a trie as cache
    for i in range(len(genes)):
        gene = genes[i]
        cache_trie_node = genes_prefix_cache_trie.find_node(gene)
        if cache_trie_node != None:
            prefix_arrays[i] = cache_trie_node.value
        else:
            prefix_arrays[i] = kmp_initialize_suffix_prefix_array(gene)
            cache_trie_node = genes_prefix_cache_trie.add(gene)
            cache_trie_node.value = prefix_arrays[i]

    genes_data = GenesData(genes, health, prefix_arrays)

    min = 10000000000000000
    max = 0

    for s_itr in xrange(s):
        firstLastd = raw_input().split()

        first = int(firstLastd[0])

        last = int(firstLastd[1])

        d = firstLastd[2]

        print('analizing string '+d)

        current_count = count_health(genes_data, first, last, d)

        if current_count > max:
            max = current_count

        if current_count < min:
            min = current_count

    print(str(min) + ' ' + str(max))
