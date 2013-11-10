from graph.node_iterator import NodeIterator


class Node:
    Id = 0

    def __init__(self, color=None, node_id=None, previous_color=None):
        self.edges = []
        self.color = color

        if node_id is not None:
            self.node_id = node_id
        else:
            self.node_id = Node.Id
            Node.Id += 1

        self.previous_color = self.color

        if previous_color is not None:
            self.previous_color = previous_color

        self.visited_nodes = []

    def add_edges(self, nodes):
        for node in nodes:
            self.edges.append(node)

    def __getitem__(self, item):
        return self.edges[item]

    def iterator(self):
        return NodeIterator(self)

    def get_node_of_id(self, node_id):
        for node in self.iterator():
            if node.node_id == node_id:
                return node
