class Node:
    Id = 0

    def __init__(self, color=None):
        self.edges = []
        self.color = color
        Node.Id += 1
        self.id = Node.Id

    def add_edges(self, nodes):
        for node in nodes:
            self.edges.append(node)

    def __str__(self):
        return '{0}[{1}]'.format(self.color, self.id)
