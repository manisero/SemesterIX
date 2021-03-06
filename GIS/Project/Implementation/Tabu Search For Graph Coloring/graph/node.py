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

    def add_edges(self, nodes):
        for node in nodes:
            if node not in self.edges:
                self.edges.append(node)

            if self not in node.edges:
                node.edges.append(self)

    def __getitem__(self, item):
        return self.edges[item]

    def iterator(self):
        return NodeIterator(self)

    def get_node_of_id(self, node_id):
        for node in self.iterator():
            if node.node_id == node_id:
                return node

    def node_count(self):
        return sum(1 for _ in self.iterator())

    def __str__(self):
        graph_as_string = ''

        for node in self.iterator():
            graph_as_string += str(node.node_id) + ' - ' + str(node.color) + '\n'

        return graph_as_string

    def get_colors_count(self):
        colors = set()

        for node in self.iterator():
            colors.add(node.color)

        return len(colors)

    def set_color(self, color):
        self.previous_color = self.color
        self.color = color
