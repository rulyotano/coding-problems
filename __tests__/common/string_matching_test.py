from common.string_matching import kmp_initialize_suffix_prefix_array, kmp


def test_should_initialize_suffix_prefix_array_correctly():
    result = kmp_initialize_suffix_prefix_array("AAAA")
    assert result == [0, 1, 2, 3]

    result = kmp_initialize_suffix_prefix_array("ABCDE")
    assert result == [0, 0, 0, 0, 0]

    result = kmp_initialize_suffix_prefix_array("AABAACAABAA")
    assert result == [0, 1, 0, 1, 2, 0, 1, 2, 3, 4, 5]

    result = kmp_initialize_suffix_prefix_array("AAACAAAAAC")
    assert result == [0, 1, 2, 0, 1, 2, 3, 3, 3, 4]

    result = kmp_initialize_suffix_prefix_array("AAABAAA")
    assert result == [0, 1, 2, 0, 1, 2, 3]


def test_kmp():
    test_text = 'AABAACAADAABAABA'
    pattern = 'AABA'
    suffix_prefix_array = kmp_initialize_suffix_prefix_array(pattern)

    result = kmp(pattern, test_text, suffix_prefix_array)
    assert result == [0, 9, 12]


def test_kmp_aaaa():
    test_text = 'AAAAABAAABA'
    pattern = 'AAAA'
    suffix_prefix_array = kmp_initialize_suffix_prefix_array(pattern)

    result = kmp(pattern, test_text, suffix_prefix_array)
    assert result == [0, 1]


def test_kmp_empty_text():
    test_text = ''
    pattern = 'AAAA'
    suffix_prefix_array = kmp_initialize_suffix_prefix_array(pattern)

    result = kmp(pattern, test_text, suffix_prefix_array)
    assert result == []


def test_kmp_empty_pattern():
    test_text = 'AAAAABAAABA'
    pattern = ''
    suffix_prefix_array = kmp_initialize_suffix_prefix_array(pattern)

    result = kmp(pattern, test_text, suffix_prefix_array)
    assert result == []


def test_kmp_found_at_end():
    test_text = 'AAAAABAAABAC'
    pattern = 'C'
    suffix_prefix_array = kmp_initialize_suffix_prefix_array(pattern)

    result = kmp(pattern, test_text, suffix_prefix_array)
    assert result == [11]


def test_kmp_one_result():
    test_text = 'AABAACAADAABAABA'
    pattern = 'AABA'
    suffix_prefix_array = kmp_initialize_suffix_prefix_array(pattern)

    result = kmp(pattern, test_text, suffix_prefix_array, one_result=True)
    assert result == [0]
