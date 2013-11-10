class Node:
    Id = 0

    def __init__(self, color=None, node_id=None):
        self.edges = []
        self.color = color

        if node_id is not None:
            self.node_id = node_id
        else:
            self.node_id = Node.Id
            Node.Id += 1

    def add_edges(self, nodes):
        for node in nodes:
            self.edges.append(node)

    def __getitem__(self, item):
        return self.edges[item]
