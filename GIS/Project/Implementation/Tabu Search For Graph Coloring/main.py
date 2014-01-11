import argparse
import sys
from evaluation.cost_evaluator import CostEvaluator
from input.input_reader_factory import InputReaderFactory
from progress.progress_writer import ProgressWriter
from search.search_performer import GraphColoringSearchPerformer
from stop_criteria.stop_criteria import StopCriteria


def main():
    parser = argparse.ArgumentParser()
    parser.add_argument('input_file', nargs='+', type=str, help='input file(s) containing graph definition')
    parser.add_argument('-o', required=False, metavar='output_file', help='name of the file to write progress log',
                        default=sys.stdout)
    parser.add_argument('-v', '--verbose', required=False, action='store_true', help='extended logging')
    parser.add_argument('-i', type=int, required=True, help='maximum iterations', metavar='maximum_iterations')
    parser.add_argument('-s', type=int, required=True, help='maximum iterations without changed score',
                        metavar='maximum_iterations_without_score_change')
    parser.add_argument('-m', type=int, required=True, help='memory size', metavar='memory_size')
    parser.add_argument('--dimacs-compat', required=False, action='store_true', help='DIMACS input compatibility')
    arguments = parser.parse_args()

    progress_writer = ProgressWriter(arguments.o, arguments.verbose)

    for file_name in arguments.input_file:
        progress_writer.start_execution(file_name, False)
        input_reader = InputReaderFactory.create_input_reader(arguments.dimacs_compat)
        progress_writer.start_task('reading input file', True)
        graph, color_set = input_reader.read_input_graph_and_color_set(file_name)
        progress_writer.stop_task('reading input file', get_input_summary(graph, color_set), True)
        stop_criteria = StopCriteria(arguments.i, arguments.s)
        best_score = GraphColoringSearchPerformer(stop_criteria, arguments.m, progress_writer).search(graph, color_set)
        progress_writer.stop_execution(file_name, 'best score for colouring: \n{0}'.format(best_score), False)


def get_input_summary(graph, color_set):
    return 'read graph:\n{0}\nread color set:\n{1}\n\ninitial cost: {2}\n'.format(graph, color_set,
                                                                                  CostEvaluator.evaluate(graph,
                                                                                                         color_set))

if __name__ == '__main__':
    main()
