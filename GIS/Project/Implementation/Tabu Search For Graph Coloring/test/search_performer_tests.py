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
        search_performer = GraphColoringSearchPerformer(StopCriteria(10, 10), 1)
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
