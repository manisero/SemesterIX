class AspirationCriteria:
    def __init__(self, banned_transitions, best_score):
        self.banned_transitions = banned_transitions
        self.best_score = best_score

    def is_permutation_allowed(self, node, color, cost):
        if (node.node_id, color) not in self.banned_transitions:
            return True

        return self.best_score is not None and cost < self.best_score
