import sys
import re
import random
import os
import math
# from common.string_matching import kmp, kmp_initialize_suffix_prefix_array

# import


def kmp(pattern, text, suffix_preffix_array, one_result=False):
    if pattern == '':
        return []

    m = len(pattern)
    n = len(text)

    i = 0
    j = 0
    result = []
    while i < n:
        if pattern[j] == text[i]:
            i += 1
            j += 1

        if j == m:
            result.append(i - j)
            if one_result:
                return result
            j = suffix_preffix_array[j - 1]
        elif i < n and pattern[j] != text[i]:
            if j != 0:
                j = suffix_preffix_array[j - 1]
            else:
                i += 1

    return result


def kmp_initialize_suffix_prefix_array(pattern):
    n = len(pattern)
    suffix_prefix_array = [0 for _ in range(n)]

    l = 0
    i = 1
    while i < n:
        c = pattern[i]
        if pattern[l] == c:
            l += 1
            suffix_prefix_array[i] = l
            i += 1
        else:
            if l > 0:
                l = suffix_prefix_array[l - 1]
            else:
                suffix_prefix_array[i] = 0
                i += 1

    return suffix_prefix_array
# end import


def count_health(genes_data, firstIndex, lastIndex, stream):
    total_health = 0
    genes_count_cache = {}

    for i in range(firstIndex, lastIndex + 1):
        gene = genes_data.genes[i]
        health = genes_data.health[i]

        if not gene in genes_count_cache:
            prefix_array = genes_data.prefix_arrays[i]
            appear_positions = kmp(gene, stream, prefix_array)
            genes_count_cache[gene] = len(appear_positions)

        total_health += genes_count_cache[gene] * health

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

    prefix_arrays = [kmp_initialize_suffix_prefix_array(g) for g in genes]

    genes_data = GenesData(genes, health, prefix_arrays)

    min = 10000000000000000
    max = 0

    for s_itr in xrange(s):
        firstLastd = raw_input().split()

        first = int(firstLastd[0])

        last = int(firstLastd[1])

        d = firstLastd[2]

        current_count = count_health(genes_data, first, last, d)

        if current_count > max:
            max = current_count

        if current_count < min:
            min = current_count

    print(str(min) + ' ' + str(max))
