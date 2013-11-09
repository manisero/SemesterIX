class StopCriteria:
    def __init__(self, maximum_iterations, maximum_iterations_without_score_change):
        self.maximum_iterations = maximum_iterations
        self.maximum_iterations_without_score_change = maximum_iterations_without_score_change
        self.current_iterations = 0
        self.current_iterations_without_score_change = 0
        self.previous_score = None

    def reset(self):
        self.current_iterations = 0
        self.current_iterations_without_score_change = 0
        self.previous_score = None

    def next_iteration(self, score):
        self.current_iterations += 1

        if self.previous_score is None or self.previous_score != score:
            self.previous_score = score
            self.current_iterations_without_score_change = 1
        else:
            self.current_iterations_without_score_change += 1

    def should_stop(self):
        return self.current_iterations >= self.maximum_iterations or \
               self.current_iterations_without_score_change >= self.maximum_iterations_without_score_change
