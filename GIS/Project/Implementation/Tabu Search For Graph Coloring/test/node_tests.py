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

    def test_node_count_method(self):
        n1 = Node('a')
        n2 = Node('b')
        n3 = Node('c')
        n4 = Node('d')
        n5 = Node('e')
        n1.add_edges([n2, n5])
        n2.add_edges([n3])
        n3.add_edges([n4])

        node_count = n1.node_count()

        self.assertEqual(node_count, 5)

    def test_color_classes_method(self):
        n1 = Node(color='#')
        n2 = Node(color='*')
        n3 = Node(color='#')
        n4 = Node(color='$')
        n1.add_edges([n2, n3, n4])
        n2.add_edges([n1, n3, n4])
        n3.add_edges([n1, n2, n4])
        n4.add_edges([n1, n2, n3])

        color_classes = n1.get_colors_count()

        self.assertEqual(3, color_classes)

    def test_set_color_method(self):
        n1 = Node(color='#')

        n1.set_color('?')

        self.assertEqual('?', n1.color)
        self.assertEqual('#', n1.previous_color)
