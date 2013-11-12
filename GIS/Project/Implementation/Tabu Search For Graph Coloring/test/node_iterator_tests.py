import unittest
from graph.node import Node
from graph.node_iterator import NodeIterator


class NodeIteratorTests(unittest.TestCase):
    def test_next_method(self):
        n1 = Node('red', 1)
        n2 = Node('green', 2)
        n3 = Node('green', 3)
        n4 = Node('red', 4)
        n5 = Node('green', 5)
        n6 = Node('red', 6)
        n1.add_edges([n2, n3, n6])
        n2.add_edges([n1, n4])
        n3.add_edges([n1, n4])
        n4.add_edges([n2, n3, n5])
        n5.add_edges([n4])
        n6.add_edges([n1])
        iterator = NodeIterator(n1)

        self.assertEqual(n1, iterator.next())
        self.assertEqual(n2, iterator.next())
        self.assertEqual(n4, iterator.next())
        self.assertEqual(n3, iterator.next())
        self.assertEqual(n5, iterator.next())
        self.assertEqual(n6, iterator.next())
        self.assertRaises(StopIteration, iterator.next)
