import tempfile
import unittest
from input.input_reader import InputReader


class InputReaderTests(unittest.TestCase):
    def test_input_graph_method(self):
        with tempfile.NamedTemporaryFile() as test_file:
            input_reader = InputReader()
            test_file.write('3\n')
            test_file.write('A,B\n')
            test_file.write('C,D\n')
            test_file.write('A,D\n')
            test_file.write('C,E\n')
            test_file.seek(0)

            graph, color_set = input_reader.read_input_graph_and_color_set(test_file.name)
            a_node = graph.get_node_of_id('A')
            b_node = graph.get_node_of_id('B')
            c_node = graph.get_node_of_id('C')
            d_node = graph.get_node_of_id('D')
            e_node = graph.get_node_of_id('E')

            self.assertEqual(5, graph.node_count())
            self.assertEqual(3, len(color_set))
            self.assertIn(0, color_set)
            self.assertIn(1, color_set)
            self.assertIn(2, color_set)
            self.assertEqual(2, len(a_node.edges))
            self.assertIn(b_node, a_node.edges)
            self.assertIn(d_node, a_node.edges)
            self.assertIn(a_node.color, range(0, 3))
            self.assertEqual(1, len(b_node.edges))
            self.assertIn(a_node, b_node.edges)
            self.assertIn(b_node.color, range(0, 3))
            self.assertEqual(2, len(c_node.edges))
            self.assertIn(d_node, c_node.edges)
            self.assertIn(e_node, c_node.edges)
            self.assertIn(c_node.color, range(0, 3))
            self.assertEqual(2, len(d_node.edges))
            self.assertIn(a_node, d_node.edges)
            self.assertIn(c_node, d_node.edges)
            self.assertIn(d_node.color, range(0, 3))
            self.assertEqual(1, len(e_node.edges))
            self.assertIn(c_node, e_node.edges)
            self.assertIn(e_node.color, range(0, 3))
