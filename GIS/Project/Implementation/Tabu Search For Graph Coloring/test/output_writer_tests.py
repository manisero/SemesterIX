import tempfile
import unittest
from graph.node import Node
from output.output_writer import OutputWriter


class OutputWriterTests(unittest.TestCase):
    def test_write_graph_method(self):
        with tempfile.NamedTemporaryFile() as test_file:
            output_writer = OutputWriter(test_file)
            n1 = Node(color='red', node_id=1)
            n2 = Node(color='blue', node_id=2)
            n3 = Node(color='green', node_id=3)
            n1.add_edges([n2, n3])

            output_writer.write_graph(n1)

            test_file.seek(0)
            first_line = test_file.readline()
            second_line = test_file.readline()
            third_line = test_file.readline()

            self.assertEqual('1 - red\n', first_line)
            self.assertEqual('2 - blue\n', second_line)
            self.assertEqual('3 - green\n', third_line)
