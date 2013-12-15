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
        to_write += '\nRead graph: \n'

        self.write_to_output(to_write, verbose)
        self.write_graph(root_node, verbose)

    def write_result(self, root_node, verbose=False):
        self.write_to_output('Resulting graph: \n', verbose)
        self.write_graph(root_node)

    def write_iteration_info(self, current_iterations, current_iterations_without_score_change, iteration_best_score,
                             best_score, memory, permutation, verbose=False):
        to_write = ('~' * 40)
        to_write += '\nIteration no: ' + str(current_iterations)
        to_write += '\nIterations without score change: ' + str(current_iterations_without_score_change)
        to_write += '\nIteration\'s best score: ' + str(iteration_best_score)
        to_write += '\nOverall best score: ' + str(best_score)
        to_write += '\nMemory: \n'

        self.write_to_output(to_write, verbose)
        self.write_memory(memory, verbose)
        self.write_to_output('Permutation: \n', verbose)
        self.write_graph(permutation, verbose)
        self.write_to_output(('~' * 40) + '\n', verbose)

    def write_memory(self, memory, verbose=False):
        to_write = ''

        for memory_entry in memory.get_short_term_memory():
            to_write += str(memory_entry[0]) + ' - ' + str(memory_entry[1]) + '\n'

        self.write_to_output(to_write, verbose)
