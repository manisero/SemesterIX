import unittest
from mock import Mock
from aspiration_criteria.aspiration_criteria import AspirationCriteria
from graph.node import Node
from permutation.fast_color_permutator import FastColorPermutator


class FastColorPermutatorTests(unittest.TestCase):
    def test_permutate_method_with_single_result(self):
        permutator = FastColorPermutator()
        aspiration_criteria = AspirationCriteria(None, None)
        aspiration_criteria.is_permutation_allowed = Mock(return_value=True)
        n1 = Node(color=0, node_id=10)
        n2 = Node(color=0, node_id=20)
        n3 = Node(color=0, node_id=30)
        n1.add_edges([n2])
        n2.add_edges([n1, n3])
        n3.add_edges([n2])

        permutations, best_score = permutator.permutate(n1, [0, 7], aspiration_criteria)

        self.assertEqual(-5, best_score)
        self.assertEqual(1, len(permutations))
        self.assertEqual(n2, permutations[0][0])
        self.assertEqual(7, permutations[0][1])

    def test_permutate_method_with_multiple_results(self):
        permutator = FastColorPermutator()
        aspiration_criteria = AspirationCriteria(None, None)
        aspiration_criteria.is_permutation_allowed = Mock(return_value=True)
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

        permutations, best_score = permutator.permutate(n1, [3, 4], aspiration_criteria)

        self.assertEqual(-1, best_score)
        self.assertEqual(3, len(permutations))
        self.assertEqual(n2, permutations[0][0])
        self.assertEqual(4, permutations[0][1])
        self.assertEqual(n3, permutations[1][0])
        self.assertEqual(4, permutations[1][1])
        self.assertEqual(n4, permutations[2][0])
        self.assertEqual(4, permutations[2][1])

    def test_permutate_method_with_banned_results(self):
        permutator = FastColorPermutator()
        n1 = Node(color=1, node_id=3)
        n2 = Node(color=1, node_id=5)
        n3 = Node(color=1, node_id=7)
        n4 = Node(color=1, node_id=9)
        n5 = Node(color=1, node_id=10)
        n1.add_edges([n2])
        n2.add_edges([n1, n3])
        n3.add_edges([n2, n4])
        n4.add_edges([n3, n5])
        n5.add_edges([n4])
        aspiration_criteria = AspirationCriteria([(n2.node_id, 2), (n3.node_id, 2)], -1)

        permutations, best_score = permutator.permutate(n1, [1, 2], aspiration_criteria)

        self.assertEqual(-1, best_score)
        self.assertEqual(1, len(permutations))
        self.assertEqual(n4, permutations[0][0])
        self.assertEqual(2, permutations[0][1])

