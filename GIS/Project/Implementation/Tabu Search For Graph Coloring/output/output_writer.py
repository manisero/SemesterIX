class OutputWriter:
    def __init__(self, output, verbose=False):
        self.output = output
        self.verbose = verbose

    def write_graph(self, root_node, verbose=False):
        to_write = ''

        for node in root_node.iterator():
            to_write += str(node.node_id) + ' - ' + str(node.color) + '\n'

        self.write_to_output(to_write, verbose)

    def write_to_output(self, to_write, verbose=False):
        if verbose and not self.verbose:
            return

        if isinstance(self.output, str):
            with open(self.output, 'w') as output_file:
                output_file.write(to_write)
        else:
            self.output.write(to_write)

    def write_analyzed_file_name(self, file_name, verbose=False):
        to_write = ('-' * 80)
        to_write += '\n Analyzing graph: ' + file_name + '\n'
        to_write += ('-' * 80)

        self.write_to_output(to_write, verbose)

    def write_input(self, root_node, color_set, verbose=False):
        to_write = 'Read colorset: \n' + ', '.join([str(color) for color in color_set])
        self.write_to_output(to_write, verbose)

        to_write = '\nRead graph: \n'
        self.write_to_output(to_write, verbose)
        self.write_graph(root_node, verbose)

    def write_result(self, root_node, verbose=False):
        self.write_to_output('Resulting graph: \n', verbose)
        self.write_graph(root_node)