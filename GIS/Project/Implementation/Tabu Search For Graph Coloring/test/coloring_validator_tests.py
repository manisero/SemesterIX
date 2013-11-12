import unittest
from graph.node import Node
from validation.coloring_validator import ColoringValidator


class ColoringValidatorTests(unittest.TestCase):
    def test_is_coloring_valid_method_positive(self):
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

        coloring_valid = ColoringValidator.is_coloring_valid(n1)

        self.assertTrue(coloring_valid)

    def test_is_coloring_valid_method_negative(self):
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

        coloring_valid = ColoringValidator.is_coloring_valid(n1)

        self.assertFalse(coloring_valid)
