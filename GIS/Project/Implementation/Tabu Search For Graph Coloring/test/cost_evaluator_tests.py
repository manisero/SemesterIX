import unittest
from evaluation.cost_evaluator import CostEvaluator
from graph.node import Node


class CostEvaluatorTests(unittest.TestCase):
    def test_evaluate_method(self):
        r1 = Node('red')
        r2 = Node('red')
        r3 = Node('red')
        b1 = Node('blue')
        b2 = Node('blue')
        g1 = Node('green')
        g2 = Node('green')
        g3 = Node('green')
        r1.add_edges([g1, r2, b2])
        r2.add_edges([r1, r3, b2])
        r3.add_edges([r2, b1, g2])
        b1.add_edges([r3])
        b2.add_edges([r1, r2, g1, g3])
        g1.add_edges([r1, b2])
        g2.add_edges([r3, g3])
        g3.add_edges([g2, b2])

        evaluated_cost_r1 = CostEvaluator.evaluate(r1, ['red', 'blue', 'green'])
        evaluated_cost_g3 = CostEvaluator.evaluate(g3, ['blue', 'red', 'green'])

        self.assertEqual(evaluated_cost_r1, evaluated_cost_g3)
        self.assertEqual(-4, evaluated_cost_r1)

    def test_evaluate_method_second(self):
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

        evaluated_cost = CostEvaluator.evaluate(n1, ['red', 'green'])

        self.assertEqual(-7, evaluated_cost)
