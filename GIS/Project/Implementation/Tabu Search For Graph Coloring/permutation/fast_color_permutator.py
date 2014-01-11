from evaluation.cost_evaluator import CostEvaluator
from graph.graph_cloner import GraphCloner


class FastColorPermutator:
    def __init__(self):
        self.permutations = []
        self.current_best_score = None
        self.c = {}
        self.e = {}

    def permutate(self, root_node, color_set, banned_transitions=None):
        self.permutations = []
        self.current_best_score = None
        self.c, self.e = CostEvaluator.evaluate_score_for_colors(root_node)

        if banned_transitions is None:
            banned_transitions = []

        self.find_permutations(root_node, color_set, banned_transitions)

        return self.permutations, self.current_best_score

    def find_permutations(self, root_node, color_set, banned_transitions):
        for node in root_node.iterator():
            for color in color_set:
                if node.color == color or (node.node_id, color) in banned_transitions:
                    continue

                c_permutation, e_permutation = self.c.copy(), self.e.copy()
                c_permutation[node.color] -= 1

                if color not in c_permutation:
                    c_permutation[color] = 0

                c_permutation[color] += 1

                for child_node in node.edges:
                    if child_node.color == node.color:
                        e_permutation[node.color] -= 1
                    elif child_node.color == color:
                        if color not in e_permutation:
                            e_permutation[color] = 0

                        e_permutation[color] += 1

                cost = CostEvaluator.evaluate_cost(color_set, c_permutation, e_permutation)

                if self.current_best_score is None or cost <= self.current_best_score:
                    cloned_node = GraphCloner.clone(node)
                    cloned_node.color = color

                    if cost == self.current_best_score:
                        self.permutations.append(cloned_node)
                    else:
                        self.permutations = [cloned_node]

                    self.current_best_score = cost
