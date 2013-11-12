import argparse
import sys
from input.input_reader import InputReader
from output.output_writer import OutputWriter
from search.search_performer import GraphColoringSearchPerformer
from stop_criteria.stop_criteria import StopCriteria


def main():
    parser = argparse.ArgumentParser()
    parser.add_argument('input_file', nargs='+', type=str, help='input file(s) containing graph definition')
    parser.add_argument('-o', required=False, metavar='output_file', help='name of the file to write output log',
                        default=sys.stdout)
    parser.add_argument('-v', '--verbose', required=False, action='store_true', help='extended logging')
    parser.add_argument('-i', type=int, required=True, help='maximum iterations', metavar='maximum_iterations')
    parser.add_argument('-s', type=int, required=True, help='maximum iterations without changed score',
                        metavar='maximum_iterations_without_score_change')
    parser.add_argument('-m', type=int, required=True, help='memory size', metavar='memory_size')
    arguments = parser.parse_args()

    if arguments.verbose is True:
        raise Exception('verbose mode currently not supported!')

    for file_name in arguments.input_file:
        graph, color_set = InputReader().read_input_graph_and_color_set(file_name)
        stop_criteria = StopCriteria(arguments.i, arguments.s)
        best_score = GraphColoringSearchPerformer(stop_criteria, arguments.m).search(graph, color_set)
        OutputWriter.write_graph(best_score, arguments.o)

if __name__ == '__main__':
    main()
