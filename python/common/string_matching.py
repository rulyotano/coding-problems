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
