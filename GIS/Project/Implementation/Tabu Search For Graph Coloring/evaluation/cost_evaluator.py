

class CostEvaluator:
    """
    Evaluates cost of the given graph coloring using the function from 'Optimization by simulated annealing: an
    experimental evaluation; part II, graph coloring and number partitioning' paper.

    Let C_i be the count of i color in the graph and E_i be the count of edges which lead from i-colored to another
    i-colored node. Additionally, let k be the length of the set of possible colors. Than the cost function is evaluated
    as follows:

    cost(pi) = - sum[1 to k] C_i^2 + sum[1 to k] 2 * C_i * E_i

    """
    def __init__(self):
        self.inspected_nodes = []
        self.inspected_edges = []

    def evaluate(self, root_node, color_set):
        cost = 0

        for color in color_set:
            self.inspected_nodes = []
            self.inspected_edges = []
            c_i, e_i = self.evaluate_score_for_color(root_node, color)

            cost += -1 * c_i ** 2 + 2 * c_i * e_i

        return cost

    def evaluate_score_for_color(self, node, color):
        c_i = 0
        e_i = 0

        self.inspected_nodes.append(node)

        if node.color == color:
            c_i += 1

        for child_node in node.edges:
            if node.color == color and child_node.color == color:
                if {node, child_node} not in self.inspected_edges:
                    self.inspected_edges.append({node, child_node})
                    e_i += 1

            if child_node not in self.inspected_nodes:
                c_i_prim, e_i_prim = self.evaluate_score_for_color(child_node, color)

                c_i += c_i_prim
                e_i += e_i_prim

        return c_i, e_i
