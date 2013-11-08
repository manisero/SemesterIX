class Node:
    def __init__(self, color=None):
        self.edges = []
        self.color = color

    def add_edges(self, nodes):
        for node in nodes:
            self.edges.append(node)

    def clone(self):
        node = Node(self.color)

        for child_node in self.edges:
            node.edges.append(child_node.clone())
