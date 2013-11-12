import unittest
from graph.node import Node
from validation.connection_validator import ConnectionValidator


class ConnectionValidatorTests(unittest.TestCase):
    def test_is_graph_connected_method_positive(self):
        n1 = Node()
        n2 = Node()
        n3 = Node()
        n1.add_edges([n2, n3])
        n2.add_edges([n1])
        n3.add_edges([n1])

        n1_graph_connected = ConnectionValidator.is_graph_connected(n1, 3)
        n2_graph_connected = ConnectionValidator.is_graph_connected(n2, 3)
        n3_graph_connected = ConnectionValidator.is_graph_connected(n3, 3)

        self.assertTrue(n1_graph_connected)
        self.assertTrue(n2_graph_connected)
        self.assertTrue(n3_graph_connected)

    def test_is_graph_connected_method_negative(self):
        n1 = Node()
        n2 = Node()
        n3 = Node()
        n4 = Node()
        n5 = Node()
        n1.add_edges([n2, n3])
        n2.add_edges([n1])
        n3.add_edges([n1])
        n4.add_edges([n5])
        n5.add_edges([n4])

        n1_graph_connected = ConnectionValidator.is_graph_connected(n1, 5)
        n2_graph_connected = ConnectionValidator.is_graph_connected(n2, 5)
        n3_graph_connected = ConnectionValidator.is_graph_connected(n3, 5)
        n4_graph_connected = ConnectionValidator.is_graph_connected(n4, 5)
        n5_graph_connected = ConnectionValidator.is_graph_connected(n5, 5)

        self.assertFalse(n1_graph_connected)
        self.assertFalse(n2_graph_connected)
        self.assertFalse(n3_graph_connected)
        self.assertFalse(n4_graph_connected)
        self.assertFalse(n5_graph_connected)
