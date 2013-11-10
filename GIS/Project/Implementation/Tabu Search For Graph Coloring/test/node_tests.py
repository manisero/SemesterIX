import unittest
from graph.node import Node


class NodeTests(unittest.TestCase):
    def test_get_node_of_id_method(self):
        n1 = Node(node_id=8)
        n2 = Node(node_id=9)
        n3 = Node(node_id=12)
        n4 = Node(node_id=1)
        n5 = Node(node_id=88)
        n1.add_edges([n5, n3])
        n2.add_edges([n4])
        n3.add_edges([n2, n1])
        n4.add_edges([n1, n5])

        self.assertEqual(n1, n1.get_node_of_id(8))
        self.assertEqual(n2, n1.get_node_of_id(9))
        self.assertEqual(n3, n1.get_node_of_id(12))
        self.assertEqual(n4, n1.get_node_of_id(1))
        self.assertEqual(n5, n1.get_node_of_id(88))
