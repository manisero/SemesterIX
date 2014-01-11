from evaluation.cost_evaluator import CostEvaluator
from graph.graph_cloner import GraphCloner


class FastColorPermutator:
    def __init__(self):
        self.permutations = []
        self.current_best_score = None
        self.c = {}
        self.e = {}

    def permutate(self, root_node, color_set, aspiration_criteria):
        self.permutations = []
        self.current_best_score = None
        self.c, self.e = CostEvaluator.evaluate_score_for_colors(root_node)
        self.find_permutations(root_node, color_set, aspiration_criteria)

        return self.permutations, self.current_best_score

    def find_permutations(self, root_node, color_set, aspiration_criteria):
        for node in root_node.iterator():
            for color in color_set:
                if node.color == color:
                    continue

                cost = CostEvaluator.evaluate_score_for_permutation(node, color, self.c, self.e, color_set)

                if not aspiration_criteria.is_permutation_allowed(node, color, cost):
                    continue

                if self.current_best_score is None or cost <= self.current_best_score:
                    cloned_node = GraphCloner.clone(node)
                    cloned_node.color = color

                    if cost == self.current_best_score:
                        self.permutations.append(cloned_node)
                    else:
                        self.permutations = [cloned_node]

                    self.current_best_score = cost
