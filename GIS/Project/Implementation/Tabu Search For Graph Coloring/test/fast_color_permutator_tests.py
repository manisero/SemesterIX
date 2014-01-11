import unittest
from graph.node import Node
from permutation.fast_color_permutator import FastColorPermutator


class FastColorPermutatorTests(unittest.TestCase):
    def test_permutate_method_with_single_result(self):
        permutator = FastColorPermutator()
        n1 = Node(color=0, node_id=10)
        n2 = Node(color=0, node_id=20)
        n3 = Node(color=0, node_id=30)
        n1.add_edges([n2])
        n2.add_edges([n1, n3])
        n3.add_edges([n2])

        permutations, best_score = permutator.permutate(n1, [0, 7])

        self.assertEqual(-5, best_score)
        self.assertEqual(1, len(permutations))
        self.assertEqual(0, permutations[0].get_node_of_id(n1.node_id).color)
        self.assertEqual(7, permutations[0].get_node_of_id(n2.node_id).color)
        self.assertEqual(0, permutations[0].get_node_of_id(n3.node_id).color)

    def test_permutate_method_with_multiple_results(self):
        permutator = FastColorPermutator()
        n1 = Node(color=3, node_id=3)
        n2 = Node(color=3, node_id=5)
        n3 = Node(color=3, node_id=7)
        n4 = Node(color=3, node_id=9)
        n5 = Node(color=3, node_id=10)
        n1.add_edges([n2])
        n2.add_edges([n1, n3])
        n3.add_edges([n2, n4])
        n4.add_edges([n3, n5])
        n5.add_edges([n4])

        permutations, best_score = permutator.permutate(n1, [3, 4])

        self.assertEqual(-1, best_score)
        self.assertEqual(3, len(permutations))
        self.assertEqual(3, permutations[0].get_node_of_id(n1.node_id).color)
        self.assertEqual(4, permutations[0].get_node_of_id(n2.node_id).color)
        self.assertEqual(3, permutations[0].get_node_of_id(n3.node_id).color)
        self.assertEqual(3, permutations[0].get_node_of_id(n4.node_id).color)
        self.assertEqual(3, permutations[0].get_node_of_id(n5.node_id).color)
        self.assertEqual(3, permutations[1].get_node_of_id(n1.node_id).color)
        self.assertEqual(3, permutations[1].get_node_of_id(n2.node_id).color)
        self.assertEqual(4, permutations[1].get_node_of_id(n3.node_id).color)
        self.assertEqual(3, permutations[1].get_node_of_id(n4.node_id).color)
        self.assertEqual(3, permutations[1].get_node_of_id(n5.node_id).color)
        self.assertEqual(3, permutations[2].get_node_of_id(n1.node_id).color)
        self.assertEqual(3, permutations[2].get_node_of_id(n2.node_id).color)
        self.assertEqual(3, permutations[2].get_node_of_id(n3.node_id).color)
        self.assertEqual(4, permutations[2].get_node_of_id(n4.node_id).color)
        self.assertEqual(3, permutations[2].get_node_of_id(n5.node_id).color)
