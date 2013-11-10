import unittest
from graph.node import Node
from permutation.color_permutator import ColorPermutator


class ColorPermutatorTests(unittest.TestCase):
    color_permutator = ColorPermutator()

    def test_permutate_method(self):
        n1 = Node(1)
        n2 = Node(2)
        n3 = Node(3)
        n1.add_edges([n2])
        n2.add_edges([n3])
        n3.add_edges([n1])

        permutations = self.color_permutator.permutate(n1, [1, 2, 3])

        self.assertEqual(6, len(permutations))

        self.assertEqual(2, permutations[0].color)
        self.assertEqual(1, permutations[0].previous_color)
        self.assertEqual(2, permutations[0][0].color)
        self.assertEqual(2, permutations[0][0].previous_color)
        self.assertEqual(3, permutations[0][0][0].color)
        self.assertEqual(3, permutations[0][0][0].previous_color)

        self.assertEqual(3, permutations[1].color)
        self.assertEqual(1, permutations[1].previous_color)
        self.assertEqual(2, permutations[1][0].color)
        self.assertEqual(2, permutations[1][0].previous_color)
        self.assertEqual(3, permutations[1][0][0].color)
        self.assertEqual(3, permutations[1][0][0].previous_color)

        self.assertEqual(1, permutations[2].color)
        self.assertEqual(2, permutations[2].previous_color)
        self.assertEqual(3, permutations[2][0].color)
        self.assertEqual(3, permutations[2][0].previous_color)
        self.assertEqual(1, permutations[2][0][0].color)
        self.assertEqual(1, permutations[2][0][0].previous_color)

        self.assertEqual(3, permutations[3].color)
        self.assertEqual(2, permutations[3].previous_color)
        self.assertEqual(3, permutations[3][0].color)
        self.assertEqual(3, permutations[3][0].previous_color)
        self.assertEqual(1, permutations[3][0][0].color)
        self.assertEqual(1, permutations[3][0][0].previous_color)

        self.assertEqual(1, permutations[4].color)
        self.assertEqual(3, permutations[4].previous_color)
        self.assertEqual(1, permutations[4][0].color)
        self.assertEqual(1, permutations[4][0].previous_color)
        self.assertEqual(2, permutations[4][0][0].color)
        self.assertEqual(2, permutations[4][0][0].previous_color)

        self.assertEqual(2, permutations[5].color)
        self.assertEqual(3, permutations[5].previous_color)
        self.assertEqual(1, permutations[5][0].color)
        self.assertEqual(1, permutations[5][0].previous_color)
        self.assertEqual(2, permutations[5][0][0].color)
        self.assertEqual(2, permutations[5][0][0].previous_color)

    def test_permutate_method_with_banned_transitions(self):
        n1 = Node(1, 18)
        n2 = Node(2, 23)
        n3 = Node(3, 51)
        n1.add_edges([n2])
        n2.add_edges([n3])
        n3.add_edges([n1])

        permutations = self.color_permutator.permutate(n1, [1, 2, 3], [(23, 1), (51, 1)])

        self.assertEqual(4, len(permutations))

        self.assertEqual(2, permutations[0].color)
        self.assertEqual(1, permutations[0].previous_color)
        self.assertEqual(2, permutations[0][0].color)
        self.assertEqual(2, permutations[0][0].previous_color)
        self.assertEqual(3, permutations[0][0][0].color)
        self.assertEqual(3, permutations[0][0][0].previous_color)

        self.assertEqual(3, permutations[1].color)
        self.assertEqual(1, permutations[1].previous_color)
        self.assertEqual(2, permutations[1][0].color)
        self.assertEqual(2, permutations[1][0].previous_color)
        self.assertEqual(3, permutations[1][0][0].color)
        self.assertEqual(3, permutations[1][0][0].previous_color)

        self.assertEqual(3, permutations[2].color)
        self.assertEqual(2, permutations[2].previous_color)
        self.assertEqual(3, permutations[2][0].color)
        self.assertEqual(3, permutations[2][0].previous_color)
        self.assertEqual(1, permutations[2][0][0].color)
        self.assertEqual(1, permutations[2][0][0].previous_color)

        self.assertEqual(2, permutations[3].color)
        self.assertEqual(3, permutations[3].previous_color)
        self.assertEqual(1, permutations[3][0].color)
        self.assertEqual(1, permutations[3][0].previous_color)
        self.assertEqual(2, permutations[3][0][0].color)
        self.assertEqual(2, permutations[3][0][0].previous_color)
