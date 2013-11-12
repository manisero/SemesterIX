from graph.graph_cloner import GraphCloner


class ColorPermutator:
    def __init__(self):
        self.permutations = []

    def permutate(self, root_node, color_set, banned_transitions=None):
        self.permutations = []

        if banned_transitions is None:
            banned_transitions = []

        self.find_permutations(root_node, color_set, banned_transitions)

        return self.permutations

    def find_permutations(self, root_node, color_set, banned_transitions):
        for node in root_node.iterator():
            for color in color_set:
                if node.color == color or (node.node_id, color) in banned_transitions:
                    continue

                cloned_node = GraphCloner.clone(node)
                cloned_node.color = color

                self.permutations.append(cloned_node)
