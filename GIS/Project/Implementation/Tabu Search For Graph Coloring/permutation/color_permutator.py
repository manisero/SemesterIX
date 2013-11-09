from graph.graph_cloner import GraphCloner


class ColorPermutator:
    def __init__(self):
        self.graph_cloner = GraphCloner()
        self.inspected_nodes = set()
        self.permutations = []

    def permutate(self, root_node, color_set):
        self.inspected_nodes = set()
        self.permutations = []
        self.find_permutations(root_node, color_set)

        return self.permutations

    def find_permutations(self, node, color_set):
        self.inspected_nodes.add(node)

        for color in color_set:
            if node.color == color:
                continue

            cloned_node = self.graph_cloner.clone(node)
            cloned_node.color = color

            self.permutations.append(cloned_node)

        for child_node in node.edges:
            if child_node not in self.inspected_nodes:
                self.find_permutations(child_node, color_set)
