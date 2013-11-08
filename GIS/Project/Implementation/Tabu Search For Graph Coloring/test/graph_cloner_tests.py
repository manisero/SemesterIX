import unittest
from graph.graph_cloner import GraphCloner
from graph.node import Node


class GraphClonerTests(unittest.TestCase):
    graph_cloner = GraphCloner()

    def test_clone_method(self):
        n1 = Node(1)
        n2 = Node(2)
        n3 = Node(3)
        n4 = Node(4)
        n1.add_edges([n2, n3])
        n3.add_edges([n4])
        n4.add_edges([n1, n2])

        cloned_n3 = self.graph_cloner.clone(n3)
        cloned_n4 = cloned_n3.edges[0]
        cloned_n1 = cloned_n4.edges[0]
        cloned_n2 = cloned_n4.edges[1]

        self.assertNotEqual(n3, cloned_n3)
        self.assertEqual(3, cloned_n3.color)
        self.assertEqual(1, len(cloned_n3.edges))
        self.assertNotEqual(n4, cloned_n4)
        self.assertEqual(4, cloned_n4.color)
        self.assertEqual(2, len(cloned_n4.edges))
        self.assertNotEqual(n1, cloned_n1)
        self.assertEqual(1, cloned_n1.color)
        self.assertEqual(2, len(cloned_n1.edges))
        self.assertNotEqual(n2, cloned_n2)
        self.assertEqual(2, cloned_n2.color)
        self.assertEqual(0, len(cloned_n2.edges))
