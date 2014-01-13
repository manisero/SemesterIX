import unittest
from graph.node import Node
from search.search_performer import GraphColoringSearchPerformer
from stop_criteria.stop_criteria import StopCriteria


class SearchPerformerTests(unittest.TestCase):
    def test_search_method_trivially(self):
        search_performer = GraphColoringSearchPerformer(StopCriteria(10, 10), 4)
        n1 = Node('red')
        n2 = Node('green')
        n3 = Node('green')
        n4 = Node('red')
        n5 = Node('green')
        n1.add_edges([n2, n3])
        n2.add_edges([n1, n4])
        n3.add_edges([n1, n4])
        n4.add_edges([n2, n3, n5])
        n5.add_edges([n4])

        search_result = search_performer.search(n1, ['red', 'green'])
        n1_in_search_result = search_result.get_node_of_id(n1.node_id)
        n2_in_search_result = search_result.get_node_of_id(n2.node_id)
        n3_in_search_result = search_result.get_node_of_id(n3.node_id)
        n4_in_search_result = search_result.get_node_of_id(n4.node_id)
        n5_in_search_result = search_result.get_node_of_id(n5.node_id)

        self.assertIsNotNone(n1_in_search_result)
        self.assertIsNotNone(n2_in_search_result)
        self.assertIsNotNone(n3_in_search_result)
        self.assertIsNotNone(n4_in_search_result)
        self.assertIsNotNone(n5_in_search_result)
        self.assertEqual('red', n1_in_search_result.color)
        self.assertEqual('green', n2_in_search_result.color)
        self.assertEqual('green', n3_in_search_result.color)
        self.assertEqual('red', n4_in_search_result.color)
        self.assertEqual('green', n5_in_search_result.color)

    def test_search_method_with_easy_substitution(self):
        search_performer = GraphColoringSearchPerformer(StopCriteria(10, 10), 3)
        n1 = Node('red')
        n2 = Node('green')
        n3 = Node('green')
        n4 = Node('red')
        n5 = Node('red')
        n1.add_edges([n2, n3])
        n2.add_edges([n1, n4])
        n3.add_edges([n1, n4])
        n4.add_edges([n2, n3, n5])
        n5.add_edges([n4])

        search_result = search_performer.search(n1, ['red', 'green'])
        n1_in_search_result = search_result.get_node_of_id(n1.node_id)
        n2_in_search_result = search_result.get_node_of_id(n2.node_id)
        n3_in_search_result = search_result.get_node_of_id(n3.node_id)
        n4_in_search_result = search_result.get_node_of_id(n4.node_id)
        n5_in_search_result = search_result.get_node_of_id(n5.node_id)

        self.assertIsNotNone(n1_in_search_result)
        self.assertIsNotNone(n2_in_search_result)
        self.assertIsNotNone(n3_in_search_result)
        self.assertIsNotNone(n4_in_search_result)
        self.assertIsNotNone(n5_in_search_result)
        self.assertEqual('red', n1_in_search_result.color)
        self.assertEqual('green', n2_in_search_result.color)
        self.assertEqual('green', n3_in_search_result.color)
        self.assertEqual('red', n4_in_search_result.color)
        self.assertEqual('green', n5_in_search_result.color)

    def test_search_method_with_easy_substitutions(self):
        search_performer = GraphColoringSearchPerformer(StopCriteria(10, 10), 3)
        n1 = Node('red')
        n2 = Node('red')
        n3 = Node('red')
        n4 = Node('red')
        n5 = Node('red')
        n1.add_edges([n2, n3])
        n2.add_edges([n1, n4])
        n3.add_edges([n1, n4])
        n4.add_edges([n2, n3, n5])
        n5.add_edges([n4])

        search_result = search_performer.search(n1, ['red', 'green'])
        n1_in_search_result = search_result.get_node_of_id(n1.node_id)
        n2_in_search_result = search_result.get_node_of_id(n2.node_id)
        n3_in_search_result = search_result.get_node_of_id(n3.node_id)
        n4_in_search_result = search_result.get_node_of_id(n4.node_id)
        n5_in_search_result = search_result.get_node_of_id(n5.node_id)

        self.assertIsNotNone(n1_in_search_result)
        self.assertIsNotNone(n2_in_search_result)
        self.assertIsNotNone(n3_in_search_result)
        self.assertIsNotNone(n4_in_search_result)
        self.assertIsNotNone(n5_in_search_result)
        self.assertEqual('green', n1_in_search_result.color)
        self.assertEqual('red', n2_in_search_result.color)
        self.assertEqual('red', n3_in_search_result.color)
        self.assertEqual('green', n4_in_search_result.color)
        self.assertEqual('red', n5_in_search_result.color)

    def test_find_permutations_method_emergency_procedure(self):
        search_performer = GraphColoringSearchPerformer(StopCriteria(10, 10), 5)
        n1 = Node('red', 12)
        n2 = Node('red', 21)
        n3 = Node('red', 33)
        n4 = Node('red', 34)
        n5 = Node('red', 72)
        search_performer.memory.add_to_memory(n1, 'black')
        search_performer.memory.add_to_memory(n2, 'black')
        search_performer.memory.add_to_memory(n3, 'black')
        search_performer.memory.add_to_memory(n4, 'black')
        search_performer.memory.add_to_memory(n5, 'black')
        n1.add_edges([n2])
        n2.add_edges([n3])
        n3.add_edges([n4])
        n4.add_edges([n5])

        first_permutations = search_performer.find_permutations(n1, ['black', 'red'])[0]
        first_node, first_node_color = first_permutations[0]
        first_node.set_color(first_node_color)
        search_performer.memory.add_to_memory(first_node, first_node.previous_color)
        second_permutations = search_performer.find_permutations(first_node, ['black', 'red'])[0]
        second_node, second_node_color = second_permutations[0]
        second_node.set_color(second_node_color)
        search_performer.memory.add_to_memory(second_node, second_node.previous_color)
        third_permutations = search_performer.find_permutations(second_node, ['black', 'red'])[0]
        third_node, third_node_color = third_permutations[0]
        third_node.set_color(third_node_color)
        search_performer.memory.add_to_memory(third_node, third_node.previous_color)
        fourth_permutations = search_performer.find_permutations(third_node, ['black', 'red'])[0]
        fourth_node, fourth_node_color = fourth_permutations[0]
        fourth_node.set_color(fourth_node_color)
        search_performer.memory.add_to_memory(fourth_node, fourth_node.previous_color)
        fifth_permutations = search_performer.find_permutations(fourth_node, ['black', 'red'])[0]
        fifth_node, fifth_node_color = fifth_permutations[0]
        fifth_node.set_color(fifth_node_color)

        self.assertEqual(1, len(first_permutations))
        self.assertEqual(12, first_node.node_id)
        self.assertEqual('black', first_node.color)
        self.assertEqual(1, len(second_permutations))
        self.assertEqual(21, second_node.node_id)
        self.assertEqual('black', second_node.color)
        self.assertEqual(1, len(third_permutations))
        self.assertEqual(33, third_node.node_id)
        self.assertEqual('black', third_node.color)
        self.assertEqual(1, len(fourth_permutations))
        self.assertEqual(34, fourth_node.node_id)
        self.assertEqual('black', fourth_node.color)
        self.assertEqual(1, len(fifth_permutations))
        self.assertEqual(72, fifth_node.node_id)
        self.assertEqual('black', fifth_node.color)

    def test_get_best_score_for_iteration_method_with_long_term_memory_usage(self):
        search_performer = GraphColoringSearchPerformer(StopCriteria(10, 10), 2)
        n1 = Node(1, 2)
        n2 = Node(1, 5)
        n3 = Node(1, 8)
        search_performer.memory.add_to_memory(n1, 2)
        search_performer.memory.add_to_memory(n2, 3)
        search_performer.memory.add_to_memory(n3, 4)
        search_performer.memory.add_to_memory(n1, 5)
        search_performer.memory.add_to_memory(n3, 6)
        search_performer.memory.add_to_memory(n1, 7)

        best_score_for_iteration = search_performer.get_best_permutation_for_iteration(
            [(n1, 3), (n2, 3), (n3, 3)])

        self.assertEqual(3, best_score_for_iteration[1])
        self.assertEqual(n2, best_score_for_iteration[0])
