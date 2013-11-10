from evaluation.cost_evaluator import CostEvaluator
from memory.memory import Memory
from permutation.color_permutator import ColorPermutator


class GraphColoringSearchPerformer:
    def __init__(self, stop_criteria, memory_size):
        self.stop_criteria = stop_criteria
        self.color_permutator = ColorPermutator()
        self.memory = Memory(memory_size)
        self.cost_evaluator = CostEvaluator()
        self.best_score = None

    def search(self, root_node, color_set):
        self.memory.clear_memory()
        self.best_score = (root_node, self.cost_evaluator.evaluate(root_node, color_set))

        return self.recursive_search(root_node, color_set)

    def recursive_search(self, node, color_set):
        permutations = self.color_permutator.permutate(node, color_set, self.memory.get_short_term_memory())
        permutations_to_scores = {permutation: self.cost_evaluator.evaluate(node, color_set) for permutation in
                                  permutations}

        if len(permutations_to_scores) == 0:
            return self.return_score()

        iteration_best_score = sorted(permutations_to_scores.items(), key=lambda t: t[1])[0]

        self.memory.add_to_memory(iteration_best_score[0], iteration_best_score[0].previous_color)

        if self.best_score[1] > iteration_best_score[1]:
            self.best_score = iteration_best_score

        self.stop_criteria.next_iteration(self.best_score)

        if self.stop_criteria.should_stop():
            return self.return_score()

        return self.recursive_search(iteration_best_score[0], color_set)

    def return_score(self):
        return self.best_score[0]
