from common.trie import Trie
from problems.determining_dna_health import DataItem, add_health_to_gene_trie, build_trie, count_health_in_node_value


def test_creation_data_item():
    test_item = DataItem(0, 10)
    assert test_item.index == 0
    assert test_item.sum == 10


def test_add_health_to_gene_trie_when_empty_should_add_new_data_item():
    trie = Trie('a')
    index = 2
    health = 13

    add_health_to_gene_trie(trie, health, index)
    result = trie.value

    assert len(result) == 1
    assert result[0].index == index
    assert result[0].sum == health


def test_add_health_to_gene_trie_when_inserting_in_middle_should_insert_in_correct_position_and_update_forward_sum():
    trie = Trie('a')
    trie.value = [DataItem(2, 8), DataItem(5, 14), DataItem(6, 19)]
    index = 3
    health = 4

    add_health_to_gene_trie(trie, health, index)
    result = trie.value

    assert len(result) == 4
    assert result[0].index == 2
    assert result[0].sum == 8

    assert result[1].index == 3
    assert result[1].sum == 12

    assert result[2].index == 5
    assert result[2].sum == 18

    assert result[3].index == 6
    assert result[3].sum == 23


def test_add_health_to_gene_trie_when_add_to_end():
    trie = Trie('a')
    trie.value = [DataItem(2, 8), DataItem(5, 14)]
    index = 7
    health = 4

    add_health_to_gene_trie(trie, health, index)
    result = trie.value

    assert len(result) == 3
    assert result[0].index == 2
    assert result[0].sum == 8

    assert result[1].index == 5
    assert result[1].sum == 14

    assert result[2].index == 7
    assert result[2].sum == 18


def test_add_health_to_gene_trie_when_add_to_beginning():
    trie = Trie('a')
    trie.value = [DataItem(2, 8)]
    index = 1
    health = 4

    add_health_to_gene_trie(trie, health, index)
    result = trie.value

    assert len(result) == 2
    assert result[0].index == 1
    assert result[0].sum == 4

    assert result[1].index == 2
    assert result[1].sum == 12


def test_build_trie_should_build_correctly():
    trie = build_trie(6, ['a', 'b', 'c', 'aa', 'd', 'b'], [1, 2, 3, 4, 5, 6])

    b_node = trie.find_node('b')
    assert len(b_node.value) == 2
    assert b_node.value[1].sum == 8

    aa_node = trie.find_node('aa')
    assert len(aa_node.value) == 1

    aab_node = trie.find_node('aab')
    assert aab_node == None


def test_count_health_in_node_value_both_index_inside():
    valueItems = [DataItem(4, 10), DataItem(
        8, 20), DataItem(15, 30), DataItem(20, 40)]

    result = count_health_in_node_value(valueItems, 6, 17)

    assert result == 20


def test_count_health_in_node_value_start_index_at_the_beggining():
    valueItems = [DataItem(4, 10), DataItem(
        8, 20), DataItem(15, 30), DataItem(20, 40)]

    result = count_health_in_node_value(valueItems, 3, 17)

    assert result == 30


def test_count_health_in_node_value_end_index_at_the_ending():
    valueItems = [DataItem(4, 10), DataItem(
        8, 20), DataItem(15, 30), DataItem(20, 40)]

    result = count_health_in_node_value(valueItems, 3, 21)

    assert result == 40


def test_count_health_in_node_value_both_index_before_start():
    valueItems = [DataItem(4, 10), DataItem(
        8, 20), DataItem(15, 30), DataItem(20, 40)]

    result = count_health_in_node_value(valueItems, 1, 2)

    assert result == 0


def test_count_health_in_node_value_both_index_after_end():
    valueItems = [DataItem(4, 10), DataItem(
        8, 20), DataItem(15, 30), DataItem(20, 40)]

    result = count_health_in_node_value(valueItems, 23, 30)

    assert result == 0


def test_count_health_in_node_value_one_item_array():
    valueItems = [DataItem(8, 20)]

    result = count_health_in_node_value(valueItems, 1, 10)

    assert result == 20


def test_count_health_in_node_value_matching_exactly_last_index():
    valueItems = [DataItem(4, 10), DataItem(
        8, 20), DataItem(15, 30), DataItem(20, 40)]

    result = count_health_in_node_value(valueItems, 4, 20)

    assert result == 40
