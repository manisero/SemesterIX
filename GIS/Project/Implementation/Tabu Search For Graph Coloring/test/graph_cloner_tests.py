import unittest
from graph.graph_cloner import GraphCloner
from graph.node import Node


class GraphClonerTests(unittest.TestCase):
    graph_cloner = GraphCloner()

    def test_clone_method(self):
        n1 = Node(1, 123)
        n2 = Node(2, 456)
        n3 = Node(3, 789)
        n4 = Node(4, 333)
        n1.add_edges([n2, n3, n4])
        n2.add_edges([n1, n4])
        n3.add_edges([n1, n4])
        n4.add_edges([n1, n2, n3])

        cloned_n3 = self.graph_cloner.clone(n3)
        cloned_n1 = cloned_n3.get_node_of_id(123)
        cloned_n2 = cloned_n3.get_node_of_id(456)
        cloned_n3 = cloned_n3.get_node_of_id(789)
        cloned_n4 = cloned_n3.get_node_of_id(333)

        self.assertNotEqual(n3, cloned_n3)
        self.assertEqual(789, cloned_n3.node_id)
        self.assertEqual(3, cloned_n3.color)
        self.assertEqual(2, len(cloned_n3.edges))
        self.assertNotEqual(n4, cloned_n4)
        self.assertEqual(333, cloned_n4.node_id)
        self.assertEqual(4, cloned_n4.color)
        self.assertEqual(3, len(cloned_n4.edges))
        self.assertNotEqual(n1, cloned_n1)
        self.assertEqual(123, cloned_n1.node_id)
        self.assertEqual(1, cloned_n1.color)
        self.assertEqual(3, len(cloned_n1.edges))
        self.assertNotEqual(n2, cloned_n2)
        self.assertEqual(789, cloned_n3.node_id)
        self.assertEqual(2, cloned_n2.color)
        self.assertEqual(2, len(cloned_n2.edges))
