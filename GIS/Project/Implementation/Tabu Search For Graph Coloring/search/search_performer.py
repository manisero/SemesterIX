from aspiration_criteria.aspiration_criteria import AspirationCriteria
from evaluation.cost_evaluator import CostEvaluator
from graph.graph_cloner import GraphCloner
from memory.memory import Memory
from permutation.fast_color_permutator import FastColorPermutator


class GraphColoringSearchPerformer:
    def __init__(self, stop_criteria, memory_size, progress_writer=None):
        self.stop_criteria = stop_criteria
        self.color_permutator = FastColorPermutator()
        self.memory = Memory(memory_size)
        self.progress_writer = progress_writer
        self.root_node = None
        self.best_score = None
        self.best_score_graph = None
        self.color_set = []

    def search(self, root_node, color_set):
        self.memory.clear_memory()
        self.root_node = root_node
        self.best_score = CostEvaluator.evaluate(root_node, color_set)
        self.best_score_graph = self.root_node
        self.color_set = color_set

        return self.recursive_search()

    def recursive_search(self):
        iteration_task = self.tabu_search_task_name()

        if self.progress_writer:
            self.progress_writer.start_task(iteration_task, True)
            self.progress_writer.start_subtask('finding permutations', True)

        permutations, score = self.find_permutations(self.root_node, self.color_set)

        if len(permutations) == 0:
            return self.return_score()

        if self.progress_writer:
            self.progress_writer.stop_subtask('finding permutations', '', True)

        node, color = self.get_best_permutation_for_iteration(permutations)
        node.set_color(color)

        self.memory.add_to_memory(node, node.previous_color)

        if self.best_score > score:
            self.best_score = score
            self.best_score_graph = GraphCloner.clone(self.root_node)

        self.stop_criteria.next_iteration(self.best_score)

        if self.progress_writer:
            self.progress_writer.stop_task(iteration_task, self.get_iteration_summary(score), True)

        if self.stop_criteria.should_stop():
            return self.return_score()

        return self.recursive_search()

    def tabu_search_task_name(self):
        return 'tabu search {0} iteration'.format(self.stop_criteria.current_iterations + 1)

    def find_permutations(self, node, color_set):
        aspiration_criteria = AspirationCriteria(self.memory.get_short_term_memory(), self.best_score)
        permutations, score = self.color_permutator.permutate(node, color_set, aspiration_criteria)

        for index in range(1, len(self.memory.get_short_term_memory())):
            if len(permutations) > 0:
                break

            aspiration_criteria = AspirationCriteria(self.memory.get_short_term_memory()[index:], self.best_score)
            permutations, score = self.color_permutator.permutate(node, color_set, aspiration_criteria)

        return permutations, score

    def return_score(self):
        return self.best_score_graph

    def get_best_permutation_for_iteration(self, permutations):
        if len(permutations) == 1:
            return permutations[0]

        permutation_to_return, permutation_to_return_usages = permutations[0], None

        for permutation in permutations:
            node_usages = sum(1 for _ in filter(lambda t: t[0] == permutation[0].node_id,
                                                self.memory.get_long_term_memory()))

            if permutation_to_return_usages is None or node_usages < permutation_to_return_usages:
                permutation_to_return, permutation_to_return_usages = permutation, node_usages

        return permutation_to_return

    def get_iteration_summary(self, iteration_best_score):
        summary = 'iterations without score change: {0}\n'.format(
            self.stop_criteria.current_iterations_without_score_change)
        summary += 'iteration\'s best score: {0}\n'.format(iteration_best_score)
        summary += 'overall best score: {0}\n'.format(self.best_score)
        summary += 'memory: {0}'.format(self.memory)
        summary += 'permutation: {0}'.format(self.root_node)

        return summary
