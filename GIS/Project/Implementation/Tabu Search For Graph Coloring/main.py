import argparse
import sys
from evaluation.cost_evaluator import CostEvaluator
from input.input_reader_factory import InputReaderFactory
from progress.progress_writer import ProgressWriter
from search.search_performer import GraphColoringSearchPerformer
from stop_criteria.stop_criteria import StopCriteria
from validation.coloring_validator import ColoringValidator


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
        progress_writer.stop_execution(file_name, get_result_summary(best_score), False)


def get_input_summary(graph, color_set):
    input_summary = 'read graph:\n{0}\n'.format(graph)
    input_summary += 'read color set:\n{0}\n\n'.format(color_set)
    input_summary += 'initial cost: {0}\n'.format(CostEvaluator.evaluate(graph, color_set))

    return input_summary


def get_result_summary(best_graph):
    result_summary = 'best score for coloring: \n{0}'.format(best_graph)
    result_summary += 'colors used: {0}\n'.format(best_graph.get_colors_count())
    result_summary += 'is coloring valid?: {0}\n'.format(ColoringValidator.is_coloring_valid(best_graph))

    return result_summary

if __name__ == '__main__':
    main()
