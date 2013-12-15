from evaluation.cost_evaluator import CostEvaluator
from memory.memory import Memory
from permutation.color_permutator import ColorPermutator


class GraphColoringSearchPerformer:
    def __init__(self, stop_criteria, memory_size, output_writer=None):
        self.stop_criteria = stop_criteria
        self.color_permutator = ColorPermutator()
        self.memory = Memory(memory_size)
        self.cost_evaluator = CostEvaluator()
        self.best_score = None
        self.output_writer = output_writer

    def search(self, root_node, color_set):
        self.memory.clear_memory()
        self.best_score = (root_node, self.cost_evaluator.evaluate(root_node, color_set))

        return self.recursive_search(root_node, color_set)

    def recursive_search(self, node, color_set):
        permutations = self.find_permutations(node, color_set)

        if len(permutations) == 0:
            return self.return_score()

        permutations_to_scores = {permutation: self.cost_evaluator.evaluate(permutation, color_set) for permutation in
                                  permutations}

        iteration_best_score = self.get_best_score_for_iteration(permutations_to_scores.items())

        self.memory.add_to_memory(iteration_best_score[0], iteration_best_score[0].previous_color)

        if self.best_score[1] > iteration_best_score[1]:
            self.best_score = iteration_best_score

        self.stop_criteria.next_iteration(self.best_score)

        if self.output_writer is not None:
            self.output_writer.write_iteration_info(self.stop_criteria.current_iterations,
                                                    self.stop_criteria.current_iterations_without_score_change,
                                                    iteration_best_score[1],
                                                    self.best_score[1],
                                                    self.memory,
                                                    iteration_best_score[0],
                                                    True)

        if self.stop_criteria.should_stop():
            return self.return_score()

        return self.recursive_search(iteration_best_score[0], color_set)

    def find_permutations(self, node, color_set):
        permutations = self.color_permutator.permutate(node, color_set, self.memory.get_short_term_memory())

        for index in range(1, len(self.memory.get_short_term_memory())):
            permutations = self.color_permutator.permutate(node, color_set,
                                                           self.memory.get_short_term_memory()[1:])

            if len(permutations) > 0:
                break

        return permutations

    def return_score(self):
        return self.best_score[0]

    def get_best_score_for_iteration(self, permutations_to_scores_list):
        permutations_to_scores_sorted = sorted(permutations_to_scores_list, key=lambda t: t[1])
        best_score = permutations_to_scores_sorted[0]
        best_scores = filter(lambda t: t[1] == best_score[1], permutations_to_scores_sorted)

        if len(best_scores) == 0:
            return best_score

        permutation_to_return = best_score, None

        for score in best_scores:
            node_usages = sum(1 for _ in filter(lambda t: t[0] == score[0].node_id, self.memory.get_long_term_memory()))

            if permutation_to_return[1] is None or node_usages < permutation_to_return[1]:
                permutation_to_return = score, node_usages

        return permutation_to_return[0]
