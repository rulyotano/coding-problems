
from common.trie import Trie

test_str_1 = 'abc'
test_str_2 = 'abcdef'
test_str_3 = 'bdr'
test_str_4 = 'predefined'
test_str_5 = 'ac'
test_str_6 = 'bder'
test_str_not_found = 'not-found'

test_values = {
    test_str_1: 4,
    test_str_2: 5,
    test_str_3: 3,
    test_str_4: 2,
    test_str_5: 6,
    test_str_6: 8,
}


def test_should_return_same_inserted_values():
    trie = Trie()
    try_test_value(trie, test_str_1)
    try_test_value(trie, test_str_2)
    try_test_value(trie, test_str_3)
    try_test_value(trie, test_str_4)
    try_test_value(trie, test_str_5)
    try_test_value(trie, test_str_6)

def try_test_value(trie, test_value):
    insertedNode = trie.add(test_value)
    insertedNode.value = test_values[test_value]

    testNode = trie.find_node(test_value)
    assert testNode.value == test_values[test_value]


def test_when_not_found_should_return_none():
    trie = Trie()
    trie.add(test_str_1)
    trie.add(test_str_2)
    trie.add(test_str_3)

    testNode = trie.find_node(test_str_not_found)

    assert testNode == None

def test_when_empty_string_should_return_root():
    trie = Trie()

    testNode = trie.find_node('')
    assert not testNode == None

def test_when_inserting_all_nodes_a_node_should_have_len_2():
    trie = Trie()

    trie.add(test_str_1)
    trie.add(test_str_2)
    trie.add(test_str_3)
    trie.add(test_str_4)
    trie.add(test_str_5)
    trie.add(test_str_6)

    a_node = trie.find_node('a')
    assert len(a_node.children) == 2
    
